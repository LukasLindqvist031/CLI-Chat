using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using CLI_Chat.Server.Models;
using System.Text.Json;
using System.IO;


namespace CLI_Chat.Server.Networking
{
    public class ClientHandler
    {
        private readonly TcpClient _client;
        private readonly string _serverName;
        private User _currentUser;

        public ClientHandler(TcpClient client, string serverName)
        {
            _client = client;
            _serverName = serverName;
        }

        public async Task HandleClientAsync()
        {
            var stream = _client.GetStream();
            var buffer = new byte[1024];

            try
            {
                var bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);

                if (bytesRead == 0)
                {
                    Console.WriteLine("Client disconnected during initalization");
                    return;
                }

                var json = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                _currentUser = JsonSerializer.Deserialize<User>(json);

                Console.WriteLine($"User' {_currentUser.Username}' has joined the server");

                while (true)
                {
                    bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                    if (bytesRead == 0)
                    {
                        Console.WriteLine($"User '{_currentUser.Username}' disconnected.");
                        break;
                    }

                    var messageJson = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    var message = JsonSerializer.Deserialize<Message>(messageJson);

                    Console.WriteLine($"[{_serverName}] {_currentUser.Username}: {message.Content}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                _client.Close();
                Console.WriteLine($"{_currentUser.Username} has disconnected");
            }
        }
    }
}
