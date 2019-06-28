using ServicesCheckerLib.Def;
using ServicesCheckerLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServicesCheckerLib.Impl.MongoDb
{
    internal class SCMongoDbOutputImpl : ISCOutput
    {
        public Task Write(DateTime timestamp, ServiceDef serviceDef, CheckResult r)
        {
            throw new NotImplementedException();
        }
    }
}
