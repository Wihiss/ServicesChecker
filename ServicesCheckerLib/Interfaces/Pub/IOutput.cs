using ServicesCheckerLib.Def;
using ServicesCheckerLib.Def.Pub;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServicesCheckerLib.Interfaces
{
    public interface IOutput
    {
        Task Write(DateTime timestamp, ServiceDef serviceDef, CheckResult r);
    }
}
