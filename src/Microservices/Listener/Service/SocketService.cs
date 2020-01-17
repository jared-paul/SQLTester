using System;
using System.Net;
using System.Net.Sockets;
using Tester.src.Common.Service;

namespace Tester.src.Microservices.Listener.Service
{
    class SocketService : IAsyncService
    {
        private IPAddress address;
        private int port;

        public SocketService(IPAddress address, int port)
        {
            this.address = address;
            this.port = port;
        }

        /// <summary>
        /// Makes a connection with the server
        /// </summary>
        /// <returns>A connected socket</returns>
        /// <exception cref="System.Net.Internals.SocketExceptionFactory.ExtendedSocketException">throws when the client is unable to connect</exception>
        public TcpClient EstablishConnection()
        {
            IPEndPoint remoteEndPoint = new IPEndPoint(address, port);
            TcpClient socket = new TcpClient(address.ToString(), port);
            Console.WriteLine("Connected to " + address.ToString() + "!");
            return socket;
        }
    }
}
