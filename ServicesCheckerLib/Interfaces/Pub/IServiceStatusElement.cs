using ServicesCheckerLib.Def;
using ServicesCheckerLib.Def.Pub;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServicesCheckerLib.Interfaces
{
    public interface IServiceStatusElement
    {
        string Id { get; set; }

        string ServiceName { get; set; }

        DateTime Timestamp { get; set; }

        ServiceStatus Status { get; set; }

        string LastError { get; set; }
    }
}
