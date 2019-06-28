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
        private SCOutputType outputType = SCOutputType.Default;

        /// <summary>
        /// List of services.
        /// </summary>
        public ServiceDef[] Services { get; set; }

        /// <summary>
        /// Output Type.
        /// </summary>
        public SCOutputType OutputType { get { return outputType; } set { outputType = value; } }
    }
}
