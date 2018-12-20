using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServicesCheckerLib.Def;
using ServicesCheckerLib.Impl;
using ServicesCheckerLib.Interfaces;
using ServicesCheckerLib.Interfaces.Pub;

namespace ServicesCheckerLib
{
    internal class SCRunnerImpl : ISCRunner
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private readonly ITimeMaster _timeMaster;
        private bool _started = false;
        private readonly object _startStopLock;
        private volatile bool _checkInProgress = false;

        private class ServiceCheckerContainer
        {
            internal IServiceChecker Checker;
            internal DateTime NextCheckTime = DateTime.MinValue;
            internal ServiceDef ServiceDef;
        }

        private readonly List<ServiceCheckerContainer> _serviceCheckers;

        internal SCRunnerImpl(ITimeMaster timeMaster, SCConfig config)
        {
            if (timeMaster == null)
                throw new ArgumentNullException(nameof(timeMaster));
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            _timeMaster = timeMaster;
            _startStopLock = new object();

            _serviceCheckers = new List<ServiceCheckerContainer>();

            InitServiceCheckers(config);
        }

        private void InitServiceCheckers(SCConfig config)
        {
            DateTime now = DateTime.Now;

            Array.ForEach(config.Services, x =>
            {
                _serviceCheckers.Add(new ServiceCheckerContainer()
                {
                    NextCheckTime = now,
                    Checker = ComponentsFactory.CreateServiceChecker(x),
                    ServiceDef = x
                });
            });
        }

        private void TimeMaster_OnNextTimeEvent(DateTime currentTime)
        {
            CheckServices(currentTime);
        }

        private void CheckServices(DateTime currentTime)
        {
            if (_checkInProgress)
                return;

            _checkInProgress = true;

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

                                x.NextCheckTime = nextCheckTime;
                            });
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex, "Parallel.ForEach exception: " + ex.Message);
                        }
                        finally
                        {
                            _checkInProgress = false;
                        }
                    });
                }
                else
                {
                    _checkInProgress = false;
                }
            }
            catch (Exception)
            {
                _checkInProgress = false;
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

                _started = true;
            }
        }

        public void Stop()
        {
            lock (_startStopLock)
            {
                if (!_started)
                    return;

                _timeMaster.NextTimeEvent -= TimeMaster_OnNextTimeEvent;

                _started = false;
            }
        }
    }
}
