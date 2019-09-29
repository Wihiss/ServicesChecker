using Newtonsoft.Json;
using ServicesCheckerLib.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ServicesCheckerLib.Impl.FileStorage
{
    internal class FileStorageHistoryLoaderImpl : FileStorageCore, IHistoryLoader
    {
        internal FileStorageHistoryLoaderImpl(string strFolder) : base(strFolder)
        {            
        }

        public Task<List<IServiceStatusElement>> Load(int depth)
        {
            return Task.Run(() => {

                List<IServiceStatusElement> resList = new List<IServiceStatusElement>();

                if (File.Exists(_strFileFullPath))
                {
                    Array.ForEach(File.ReadAllLines(_strFileFullPath),
                        x => resList.Add(JsonConvert.DeserializeObject<StorageElement>(x)));
                }

                return resList;
            });
        }
    }
}
