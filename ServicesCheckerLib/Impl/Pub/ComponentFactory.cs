using ServicesCheckerLib.Def.Pub;
using ServicesCheckerLib.Impl.FileStorage;
using ServicesCheckerLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServicesCheckerLib.Impl
{
    public static class ComponentFactory
    {
        public static ITimeMaster CreateTimeMaster() => new TimeMasterImpl();

        public static IOutput CreateOutput(StorageConfig outputConfig)
        {
            if (outputConfig != null && outputConfig.StorageType == StorageType.File)
                return new FileStorageWriterImpl(outputConfig.Address);

            return new DefaultStorage();
        }

        public static IHistoryLoader CreateHistoryLoader(StorageConfig historyLoaderConfig)
        {
            if (historyLoaderConfig != null && historyLoaderConfig.StorageType == StorageType.File)
                return new FileStorageHistoryLoaderImpl(historyLoaderConfig.Address);

            return new DefaultStorage();
        }

        public static IServiceChecker CreateServiceChecker(ServiceDef serviceDef)
        {
            if (serviceDef.Port == null)
                return new HttpServiceChecker(serviceDef.Host);

            return new TcpServiceChecker(serviceDef.Host, serviceDef.Port.Value);
        }
    }
}
