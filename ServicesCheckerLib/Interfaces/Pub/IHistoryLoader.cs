using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServicesCheckerLib.Interfaces
{
    public interface IHistoryLoader
    {
        Task<List<IServiceStatusElement>> Load(int depth);
    }
}
