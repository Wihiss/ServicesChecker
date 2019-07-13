using ServicesCheckerLib.Def.Pub;
using ServicesCheckerLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceCheckerWebApp.Models
{
    public class ServiceStatusElementModel : IServiceStatusElement
    {
        public string Id { get; set; }

        public string ServiceName { get; set; }

        public DateTime Timestamp { get; set; }
        
        public ServiceStatus Status { get; set; }

        public string LastError { get; set; }
    }
}
