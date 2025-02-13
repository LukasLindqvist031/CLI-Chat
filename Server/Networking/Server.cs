using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading.Tasks;
using CLI_Chat.Server.Services;
using System.Net;

namespace CLI_Chat.Server.Networking
{
    public class Server
    {
        private readonly TcpListener _listener;
        private readonly string _serverName;

        public Server(string ipAdress, int port, string serverName)
        {
            _listener = new TcpListener(IPAddress.Parse(ipAdress), port);
            _serverName = serverName;
        }

        public async Task StartAsync()
        {
            _listener.Start();
            Console.WriteLine("Server started...");

            while (true)
            {
                var client = await _listener.AcceptTcpClientAsync();
                var handler = new ClientHandler(client, _serverName);
                _ = handler.HandleClientAsync();
            }

        }
    }
}
