using System;
using System.Collections.Generic;
using System.Text;

namespace ServicesCheckerLib.Interfaces
{
    public interface ITimeMaster
    {
        DateTime CurrentTime { get; } 
        event Action<DateTime> NextTimeEvent;
    }
}
