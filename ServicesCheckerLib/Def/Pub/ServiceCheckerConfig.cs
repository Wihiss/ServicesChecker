using System;
using System.Collections.Generic;
using System.Text;

namespace ServicesCheckerLib.Def.Pub
{
    /// <summary>
    /// ServiceChecker config.
    /// </summary>
    public class ServiceCheckerConfig
    {
        /// <summary>
        /// List of services.
        /// </summary>
        public ServiceDef[] Services { get; set; }

        /// <summary>
        /// Output config.
        /// </summary>
        public StorageConfig Output { get; set; }

        /// <summary>
        /// History config.
        /// </summary>
        public StorageConfig HistoryLoader { get; set; }
    }
}