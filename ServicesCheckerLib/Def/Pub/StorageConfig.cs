using System;
using System.Collections.Generic;
using System.Text;

namespace ServicesCheckerLib.Def.Pub
{
    public class StorageConfig
    {
        private StorageType storagetType = StorageType.Default;

        public StorageType StorageType { get => storagetType; set => storagetType = value; }

        public string Address { get; set; }

        public int? Port { get; set; }

        public string DbName { get; set; }

        public string TableName { get; set; }

        public string User { get; set; }

        public string Password { get; set; }
    }
}
