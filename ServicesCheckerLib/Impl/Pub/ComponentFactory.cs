using ServicesCheckerLib.Def.Pub;
using ServicesCheckerLib.Impl.MongoDb;
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
            if (outputConfig != null && outputConfig.StorageType == StorageType.MongoDb)
            {
                if (outputConfig.Port == null)
                    throw new ArgumentNullException("Port cannot be blank");

                return new MongoDbStorage(outputConfig.Address, outputConfig.Port.Value, outputConfig.DbName, outputConfig.TableName, outputConfig.User, outputConfig.Password);
            }

            return new DefaultStorage();
        }

        public static IHistoryLoader CreateHistoryLoader(StorageConfig historyLoaderConfig)
        {
            if (historyLoaderConfig != null && historyLoaderConfig.StorageType == StorageType.MongoDb)
            {
                if (historyLoaderConfig.Port == null)
                    throw new ArgumentNullException("Port cannot be blank");

                return new MongoDbStorage(historyLoaderConfig.Address, historyLoaderConfig.Port.Value, historyLoaderConfig.DbName, historyLoaderConfig.TableName, historyLoaderConfig.User, historyLoaderConfig.Password);
            }

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
