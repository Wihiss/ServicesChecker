using ServicesCheckerLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServicesCheckerLib.Impl
{
    internal static class ComponentsFactory
    {
        internal static ITimeMaster CreateTimeMaster() => new TimeMasterImpl();
    }
}
