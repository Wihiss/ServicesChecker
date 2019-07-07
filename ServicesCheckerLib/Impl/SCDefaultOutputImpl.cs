using ServicesCheckerLib.Def;
using ServicesCheckerLib.Def.Pub;
using ServicesCheckerLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServicesCheckerLib.Impl
{
    internal class SCDefaultOutputImpl : ISCOutput
    {
        public Task Write(DateTime timestamp, ServiceDef serviceDef, CheckResult r)
        {
            // Default implementation does nothing

            return Task.CompletedTask;
        }
    }
}
