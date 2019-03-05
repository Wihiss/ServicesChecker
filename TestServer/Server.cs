using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace TestServer
{
    internal class Server
    {
        // Thread signal.  
        private readonly ManualResetEvent _ready = new ManualResetEvent(false);
        private readonly IPEndPoint _localEndPoint;
        private readonly Socket _listenerSocket;
        private volatile bool _started = false;

        internal Server(int port)
        {
            // Establish the local endpoint for the socket.  
            // The DNS name of the computer  
            // running the listener is "host.contoso.com".  
            // IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            // IPAddress ipAddress = ipHostInfo.AddressList[0];

            IPAddress address = IPAddress.Parse("127.0.0.1");            
            _localEndPoint = new IPEndPoint(address, port);

            // Create a TCP/IP socket.  
            _listenerSocket = new Socket(address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

	    Console.WriteLine("Socket created at port " + port);
        }

        internal void Start()
        {

            _started = true;

            // Bind the socket to the local endpoint and listen for incoming connections.
            try
            {
                _listenerSocket.Bind(_localEndPoint);
                _listenerSocket.Listen(100);

                while (true)
                {
                    if (!_started)
                        break;

                    // Set the event to nonsignaled state.
                    _ready.Reset();

                    // Start an asynchronous socket to listen for connections.  
                    Console.WriteLine("Waiting for a connection...");
                    _listenerSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);

                    // Wait until a connection is made before continuing.  
                    _ready.WaitOne();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        internal void Stop()
        {
            _started = false;
            _ready.Set();
        }

        private void AcceptCallback(IAsyncResult ar)
        {
            // Signal the main thread to continue.  
            _ready.Set();

            // Get the socket that handles the client request.  
            Socket handlerSocket = _listenerSocket.EndAccept(ar);

            Console.WriteLine("Client connected.");

            // Convert the string data to byte data using ASCII encoding.  
            byte[] byteData = Encoding.ASCII.GetBytes("Connected!");

            // Begin sending the data to the remote device.  
            handlerSocket.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), handlerSocket);
        }

        private static void SendCallback(IAsyncResult ar)
        {
            // Retrieve the socket from the state object.  
            Socket handler = (Socket)ar.AsyncState;

            try
            {
                // Complete sending the data to the remote device.  
                int bytesSent = handler.EndSend(ar);
                Console.WriteLine("{0} bytes sent to client.", bytesSent);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }
        }
    }
}
