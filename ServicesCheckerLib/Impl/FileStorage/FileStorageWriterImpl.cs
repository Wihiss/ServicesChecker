using ServicesCheckerLib.Def;
using ServicesCheckerLib.Def.Pub;
using ServicesCheckerLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace ServicesCheckerLib.Impl.FileStorage
{
    internal class FileStorageWriterImpl : FileStorageCore, IOutput
    {
        internal FileStorageWriterImpl(string strFolder) : base(strFolder)
        {
            if (File.Exists(_strFileFullPath))
                File.Delete(_strFileFullPath);
        }

        public Task Write(DateTime timestamp, ServiceDef serviceDef, CheckResult r)
        {
            return Task.Run(() => {

                StorageElement sEl = new StorageElement()
                {
                    Id = Guid.NewGuid().ToString(),
                    ServiceName = serviceDef.Name,
                    Timestamp = timestamp,
                    Status = r.Status,
                    LastError = r.LastError == null? null : r.LastError.Message
                };

                File.AppendAllText(_strFileFullPath, JsonConvert.SerializeObject(sEl) + Environment.NewLine);
            });
        }
    }
}