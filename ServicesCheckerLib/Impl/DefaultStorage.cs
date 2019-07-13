using ServicesCheckerLib.Def;
using ServicesCheckerLib.Def.Pub;
using ServicesCheckerLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServicesCheckerLib.Impl
{
    internal class DefaultStorage : IOutput, IHistoryLoader
    {
        private readonly List<IServiceStatusElement> _historyData = new List<IServiceStatusElement>();

        public Task<List<IServiceStatusElement>> Load(int depth)
        {
            // Default implementation returns an empty collection

            return Task.Factory.StartNew(() => { return _historyData; });
        }

        public Task Write(DateTime timestamp, ServiceDef serviceDef, CheckResult r)
        {
            // Default implementation does nothing

            return Task.CompletedTask;
        }
    }
}
