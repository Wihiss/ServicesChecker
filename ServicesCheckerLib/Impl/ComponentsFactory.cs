using ServicesCheckerLib.Def;
using ServicesCheckerLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServicesCheckerLib.Impl
{
    internal static class ComponentsFactory
    {
        internal static ITimeMaster CreateTimeMaster() => new TimeMasterImpl();

        internal static IServiceChecker CreateServiceChecker(ServiceDef serviceDef)
        {
            if (serviceDef.Port == null)
                return new HttpServiceChecker(serviceDef.Host);

            return new TcpServiceChecker(serviceDef.Host, serviceDef.Port.Value);
        }
    }
}
