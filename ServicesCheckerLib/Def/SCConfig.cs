using System;
using System.Collections.Generic;
using System.Text;

namespace ServicesCheckerLib.Def
{
    /// <summary>
    /// ServiceChecker config.
    /// </summary>
    public class SCConfig
    {
        /// <summary>
        /// List of services.
        /// </summary>
        public ServiceDef[] Services { get; set; }

        /// <summary>
        /// Output config.
        /// </summary>
        public OutputConfig Output { get; set; }
    }
}