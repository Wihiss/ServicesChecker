using ServicesCheckerLib.Def;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServicesCheckerLib.Interfaces
{
    public interface IServiceChecker
    {
        CheckResult Check();
    }
}
