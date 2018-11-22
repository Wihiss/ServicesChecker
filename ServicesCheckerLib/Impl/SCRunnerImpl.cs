using System;
using System.Collections.Generic;
using System.Text;
using ServicesCheckerLib.Def;
using ServicesCheckerLib.Interfaces;
using ServicesCheckerLib.Interfaces.Pub;

namespace ServicesCheckerLib
{
    internal class SCRunnerImpl : ISCRunner
    {
        private readonly ITimeMaster _timeMaster;
        private bool _started = false;
        private readonly object _startStopLock;

        internal SCRunnerImpl(ITimeMaster timeMaster)
        {
            if (timeMaster == null)
                throw new ArgumentNullException(nameof(timeMaster));
            _timeMaster = timeMaster;

            _startStopLock = new object();
        }

        private void TimeMaster_OnNextTimeEvent(DateTime currentTime)
        {
            // TODO:
        }

        public void Start(SCConfig config)
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
