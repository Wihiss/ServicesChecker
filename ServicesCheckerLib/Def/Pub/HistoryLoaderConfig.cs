using System;
using System.Collections.Generic;
using System.Text;

namespace ServicesCheckerLib.Def.Pub
{
    public class HistoryLoaderConfig : StorageConfig
    {
        private StorageType outputType = StorageType.Default;

        public StorageType OutputType { get => outputType; set => outputType = value; }

        public string Source { get; set; }
    }
}
