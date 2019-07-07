using System;
using System.Collections.Generic;
using System.Text;

namespace ServicesCheckerLib.Def.Pub
{
    public class OutputConfig : StorageConfig
    {
        private StorageType outputType = StorageType.Default;

        public StorageType OutputType { get => outputType; set => outputType = value; }

        public string Target { get; set; }
    }
}
