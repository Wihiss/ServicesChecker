using ServicesCheckerLib.Def;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServicesCheckerLib.Interfaces
{
    interface ISCOutput
    {
        Task Write(DateTime timestamp, ServiceDef serviceDef, CheckResult r);
    }
}
