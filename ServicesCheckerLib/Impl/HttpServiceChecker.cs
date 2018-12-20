using ServicesCheckerLib.Def;
using ServicesCheckerLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServicesCheckerLib.Impl
{
    internal class HttpServiceChecker : IServiceChecker
    {
        private readonly string _url;

        internal HttpServiceChecker(string url)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentException(nameof(url));

            _url = url;
        }

        public CheckResult Check()
        {
            CheckResult r = new CheckResult()
            {
                Status = ServiceStatus.Unknown
            };

            try
            {
                var myRequest = (HttpWebRequest) WebRequest.Create(_url);

                using (var response = (HttpWebResponse) myRequest.GetResponse())
                {
                    r.Status = ServiceStatus.Available;
                }
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
    }
}
