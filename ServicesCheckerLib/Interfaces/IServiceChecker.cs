﻿using ServicesCheckerLib.Def;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServicesCheckerLib.Interfaces
{
    internal interface IServiceChecker
    {
        CheckResult Check();
    }
}
