using ServicesCheckerLib.Def;
using ServicesCheckerLib.Impl.MongoDb;
using ServicesCheckerLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServicesCheckerLib.Impl
{
    internal static class ComponentsFactory
    {
        internal static ITimeMaster CreateTimeMaster() => new TimeMasterImpl();

        internal static ISCOutput CreateOutputService(OutputConfig outputConfig)
        {
            if (outputConfig != null && outputConfig.OutputType == SCOutputType.MongoDb)
            {
                if (outputConfig.Port == null)
                    throw new ArgumentNullException("Port cannot be blank");

                return new SCMongoDbOutputImpl(outputConfig.Target, outputConfig.Port.Value, outputConfig.DbName, outputConfig.TableName, outputConfig.User, outputConfig.Password);
            }

            return new SCDefaultOutputImpl();
        }

        internal static IServiceChecker CreateServiceChecker(ServiceDef serviceDef)
        {
            if (serviceDef.Port == null)
                return new HttpServiceChecker(serviceDef.Host);

            return new TcpServiceChecker(serviceDef.Host, serviceDef.Port.Value);
        }
    }
}
