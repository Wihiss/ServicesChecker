using System;
using System.Collections.Generic;
using System.Text;
using ServicesCheckerLib.Def;
using ServicesCheckerLib.Def.Pub;
using ServicesCheckerLib.Impl.Config;
using ServicesCheckerLib.Interfaces;
using ServicesCheckerLib.Interfaces.Pub;

namespace ServicesCheckerLib.Impl.Pub
{
    public static class ServiceCheckerFactory
    {
        public static IConfigMaster CreateConfigMaster() => new ConfigMasterImpl();

        public static IRunner CreateRunner(ITimeMaster timeMaster, IOutput output, ServiceDef[] services, bool autoStart)
        {
            RunnerImpl i = new RunnerImpl(timeMaster, output, services);

            if (autoStart)
                i.Start();

            return i;
        }
    }
}
