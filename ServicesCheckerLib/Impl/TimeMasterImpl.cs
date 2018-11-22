using ServicesCheckerLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServicesCheckerLib.Impl
{
    internal class TimeMasterImpl : ITimeMaster
    {
        private DateTime _currentTime;

        private System.Timers.Timer t;

        public TimeMasterImpl()
        {
            t = new System.Timers.Timer(1000);
            t.Elapsed += (sender, e) =>
            {
                _currentTime = DateTime.Now;
                NextTimeEvent?.Invoke(_currentTime);
            };

            _currentTime = DateTime.Now;

            t.Start();
        }

        public event Action<DateTime> NextTimeEvent;

        public DateTime CurrentTime => _currentTime;
    }
}
