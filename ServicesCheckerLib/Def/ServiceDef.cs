using System;
using System.Collections.Generic;
using System.Text;

namespace ServicesCheckerLib.Def
{
    /// <summary>
    /// Service definition.
    /// </summary>
    public class ServiceDef
    {
        /// <summary>
        /// Service ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Service name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Service host
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// Service port
        /// </summary>
        public int? Port { get; set; }
        /// <summary>
        /// Service poll period (in seconds)
        /// </summary>
        public int Period { get; set; }
        /// <summary>
        /// Disabled flag
        /// </summary>
        public bool Disabled { get; set; }

        public string GetFullName() => $"{Name} ({Id})";
    }
}
