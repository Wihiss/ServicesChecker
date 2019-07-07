using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServicesCheckerLib.Interfaces
{
    internal interface ISCHistoryLoader
    {
        Task<List<ISCHistoryElement>> Load(int depth);
    }
}
