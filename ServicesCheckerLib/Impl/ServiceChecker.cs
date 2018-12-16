using ServicesCheckerLib.Def;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServicesCheckerLib.Impl
{
    internal class ServiceChecker
    {
        private readonly string _host;
        private readonly int _port;
        private volatile bool _checkInProgress = false;

        internal ServiceChecker(string host, int port)
        {
            if (string.IsNullOrEmpty(host))
                throw new ArgumentException(nameof(host));
            if (port <= 0)
                throw new ArgumentException(nameof(port));

            _host = host;
            _port = port;
        }

        internal CheckResult Check()
        {
            try
            {
                _checkInProgress = true;

                CheckResult r = new CheckResult()
                {
                    Status = ServiceStatus.Unknown
                };

                try
                {
                    using (TcpClient c = new TcpClient(_host, _port))
                        r.Status = ServiceStatus.Available;
                }
                catch (SocketException ex)
                {
                    r.Status = ServiceStatus.Unavailable;
                    r.LastError = ex;
                }
                catch (Exception ex)
                {
                    r.LastError = ex;
                }

                return r;
            }
            finally
            {
                _checkInProgress = false;
            }
        }

        internal bool IsCheckInProgress => _checkInProgress;
    }

    internal class CheckResult
    {
        internal ServiceStatus Status;
        internal Exception LastError;
    }

    internal enum ServiceStatus
    {
        Available, Unavailable, Unknown
    }
}
