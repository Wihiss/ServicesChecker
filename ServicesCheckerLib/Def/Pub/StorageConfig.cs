using System;
using System.Collections.Generic;
using System.Text;

namespace ServicesCheckerLib.Def.Pub
{
    public abstract class StorageConfig
    {
        public int? Port { get; set; }

        public string DbName { get; set; }

        public string TableName { get; set; }

        public string User { get; set; }

        public string Password { get; set; }
    }
}
