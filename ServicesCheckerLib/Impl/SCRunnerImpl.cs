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
        private readonly ITimeMaster _timeMaster;
        private bool _started = false;
        private readonly object _startStopLock;

        private class ServiceCheckerContainer
        {
            internal ServiceChecker Checker;
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
            DateTime now = DateTime.UtcNow;

            Array.ForEach(config.Services, x =>
            {
                _serviceCheckers.Add(new ServiceCheckerContainer()
                {
                    NextCheckTime = now,
                    Checker = new ServiceChecker(x.Host, x.Port),
                    ServiceDef = x
                });
            });
        }

        private void TimeMaster_OnNextTimeEvent(DateTime currentTime)
        {
            List<ServiceCheckerContainer> targetContainers = _serviceCheckers.
                Where(x => currentTime >= x.NextCheckTime && !x.Checker.IsCheckInProgress).ToList();

            if (targetContainers.Count > 0)
            {
                Task.Factory.StartNew(() =>

                    Parallel.ForEach(targetContainers, x =>
                    {
                        CheckResult r = x.Checker.Check();

                        // TODO: Handle check result

                        x.NextCheckTime = DateTime.UtcNow.AddSeconds(x.ServiceDef.PollPeriod);
                    })
                );
            }
        }

        public void Start()
        {
            lock (_startStopLock)
            {
                if (_started)
                    throw new InvalidOperationException("Already started");

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
