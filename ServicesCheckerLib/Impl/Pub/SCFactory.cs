using System;
using System.Collections.Generic;
using System.Text;
using ServicesCheckerLib.Def;
using ServicesCheckerLib.Impl.Config;
using ServicesCheckerLib.Interfaces.Pub;

namespace ServicesCheckerLib.Impl.Pub
{
    public static class SCFactory
    {
        public static IConfigMaster CreateConfigMaster() => new ConfigMasterImpl();

        public static ISCRunner CreateRunner(SCConfig config, bool autoStart)
        {
            SCRunnerImpl i = new SCRunnerImpl(ComponentsFactory.CreateTimeMaster(), ComponentsFactory.CreateOutputService(config.Output), config.Services);

            if (autoStart)
                i.Start();

            return i;
        }
    }
}
