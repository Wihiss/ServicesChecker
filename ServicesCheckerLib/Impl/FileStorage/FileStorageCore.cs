using ServicesCheckerLib.Def.Pub;
using ServicesCheckerLib.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ServicesCheckerLib.Impl.FileStorage
{
    internal abstract class FileStorageCore
    {
        protected static string strFileName = "srvChkrDb.txt";
        protected readonly string _strFileFullPath;

        protected FileStorageCore(string strFolder)
        {
            if (strFolder == null)
                throw new ArgumentNullException(nameof(strFolder));

            _strFileFullPath = Path.Combine(strFolder, strFileName);
        }

        protected class StorageElement : IServiceStatusElement
        {
            public string Id { get; set; }
            public string ServiceName { get; set; }
            public DateTime Timestamp { get; set; }
            public ServiceStatus Status { get; set; }
            public string LastError { get; set; }
        }
    }
}
