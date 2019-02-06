using System;
using System.Net;
using System.Net.Sockets;

namespace TestServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Server s = new Server(9430);
            s.Start();
        }
    }
}
