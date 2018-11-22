using System;
using System.Collections.Generic;
using System.Text;
using ServicesCheckerLib.Impl.Config;
using ServicesCheckerLib.Interfaces.Pub;

namespace ServicesCheckerLib.Impl.Pub
{
    public static class SCFactory
    {
        static SCFactory()
        {

        }

        public static IConfigMaster CreateConfigMaster() => new ConfigMasterImpl();
        public static ISCRunner CreateRunner()
        {
            return new SCRunnerImpl(ComponentsFactory.CreateTimeMaster());
        }
    }
}
