using System;
using System.Net;
using System.Net.Sockets;

namespace TestServer
{
    class Program
    {
        static void Main(string[] args)
        {
	    if (args.Length != 1)
              throw new ArgumentException("The only parameter (port) is expected.");

	    int port;
	    if (!System.Int32.TryParse(args[0], out port))
              throw new ArgumentException("Cannot parse ${args[0]} to int.");

	    if (port <= 0)
              throw new ArgumentException("Wrong port value ${port}. Should be greater than 0.");

            Server s = new Server(port);
            s.Start();
        }
    }
}
