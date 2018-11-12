using System;
using System.Collections.Generic;
using System.Text;

namespace ServicesCheckerLib.Def
{
    /// <summary>
    /// Service report definition. 
    /// </summary>
    public class ServiceReportDef
    {
        /// <summary>
        /// Service status.
        /// </summary>
        public ServiceStatus Status { get; set; }
        /// <summary>
        /// Service status text.
        /// </summary>
        public string StatusText { get; set; }
        /// <summary>
        /// Service last poll time.
        /// </summary>
        public DateTime LastPollTime { get; set; }
    }

    /// <summary>
    /// Service status enum.
    /// </summary>
    public enum ServiceStatus
    {
        Available, Unavailable
    }
}
