using System;
using System.Collections.Generic;
using System.Text;

namespace ServicesCheckerLib.Impl
{
    internal class CheckResult
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

    internal enum ServiceStatus
    {
        Available, Unavailable, Unknown
    }
}
