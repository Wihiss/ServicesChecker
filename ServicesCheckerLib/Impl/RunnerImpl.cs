using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ServicesCheckerLib.Def;
using ServicesCheckerLib.Def.Pub;
using ServicesCheckerLib.Impl;
using ServicesCheckerLib.Interfaces;
using ServicesCheckerLib.Interfaces.Pub;

namespace ServicesCheckerLib
{
    internal class RunnerImpl : IRunner
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private readonly ITimeMaster _timeMaster;
        private readonly IOutput _outputService;

        private bool _started = false;
        private readonly object _startStopLock;
        private ManualResetEvent _checkComplete = new ManualResetEvent(true);

        private class ServiceCheckerContainer
        {
            internal IServiceChecker Checker;
            internal DateTime NextCheckTime = DateTime.MinValue;
            internal ServiceDef ServiceDef;
        }

        private readonly List<ServiceCheckerContainer> _serviceCheckers;

        internal RunnerImpl(ITimeMaster timeMaster, IOutput outputService, ServiceDef[] services)
        {
            if (timeMaster == null)
                throw new ArgumentNullException(nameof(timeMaster));
            if (outputService == null)
                throw new ArgumentNullException(nameof(outputService));
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            _timeMaster = timeMaster;
            _outputService = outputService;
            _startStopLock = new object();

            _serviceCheckers = new List<ServiceCheckerContainer>();

            InitServiceCheckers(services);
        }

        private void InitServiceCheckers(ServiceDef[] services)
        {
            DateTime now = DateTime.Now;

            Array.ForEach(services, x =>
            {
                _serviceCheckers.Add(new ServiceCheckerContainer()
                {
                    NextCheckTime = now,
                    Checker = ComponentFactory.CreateServiceChecker(x),
                    ServiceDef = x
                });
            });
        }

        private void TimeMaster_OnNextTimeEvent(DateTime currentTime)
        {
            if (!_started)
                return;

            CheckServices(currentTime);
        }

        private bool IsCheckInProgress()
        {
            lock (_checkComplete)
            {
                bool flag = _checkComplete.WaitOne(0);
                if (flag)
                    _checkComplete.Reset();

                return !flag;
            }
        }

        private void CheckServices(DateTime currentTime)
        {
            if (IsCheckInProgress())
                return;

            try
            {
                List<ServiceCheckerContainer> targetContainers = _serviceCheckers.
                    Where(x => currentTime >= x.NextCheckTime).ToList();

                if (targetContainers.Count > 0)
                {
                    Task.Factory.StartNew(() => {

                        try
                        {
                            Parallel.ForEach(targetContainers, x =>
                            {
                                CheckResult r = x.Checker.Check();

                                DateTime nextCheckTime =  DateTime.Now.AddSeconds(x.ServiceDef.Period);

                                logger.Info("Service " + x.ServiceDef.GetFullName() + ": " + r.GetText() + System.Environment.NewLine + "Next check time: " + nextCheckTime);

                                try
                                {
                                    _outputService.Write(DateTime.UtcNow, x.ServiceDef, r).ContinueWith(t =>
                                    {
                                        if (t.Exception != null)
                                        {
                                            logger.Error(t.Exception, "Output task error for service " + x.ServiceDef.GetFullName() + ": " + t.Exception.ToString());
                                        }
                                        else
                                        {
                                            logger.Error("Output task error for service " + x.ServiceDef.GetFullName() + " (detailed info is missed)");
                                        }
                                    }, TaskContinuationOptions.OnlyOnFaulted);
                                }
                                catch (Exception ex)
                                {
                                    logger.Error(ex, "Output error for service " + x.ServiceDef.GetFullName() + ": " + ex.Message);
                                }

                                x.NextCheckTime = nextCheckTime;
                            });
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex, "Parallel.ForEach exception: " + ex.Message);
                        }
                        finally
                        {
                            _checkComplete.Set();
                        }
                    });
                }
                else
                {
                    _checkComplete.Set();
                }
            }
            catch (Exception)
            {
                _checkComplete.Set();
                throw;
            }
        }

        public void Start()
        {
            lock (_startStopLock)
            {
                if (_started)
                    throw new InvalidOperationException("Already started");

                CheckServices(DateTime.Now);

                _timeMaster.NextTimeEvent += TimeMaster_OnNextTimeEvent;

                logger.Info("Runner started.");

                _started = true;
            }
        }

        public void Stop()
        {
            lock (_startStopLock)
            {
                if (!_started)
                    return;

                _started = false;

                _timeMaster.NextTimeEvent -= TimeMaster_OnNextTimeEvent;

                _checkComplete.WaitOne(5000);

                logger.Info("Runner stopped.");
            }
        }
    }
}
