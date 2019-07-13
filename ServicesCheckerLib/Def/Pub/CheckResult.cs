using ServicesCheckerLib.Def.Pub;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServicesCheckerLib.Def
{
    public class CheckResult
    {
        internal ServiceStatus Status;
        internal Exception LastError;

        internal string GetText()
        {
            if (LastError == null)
                return $"Status: {Status}";

            return $"Status: {Status}; Error: " + LastError.ToString();
        }
    }
}
