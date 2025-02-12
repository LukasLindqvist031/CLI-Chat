using CLI_Chat.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI_Chat.Server.Services
{
    public class ChatService
    {
        private readonly Dictionary<string, ChatRoom> _chatRooms = new Dictionary<string, ChatRoom>();

        public bool CreateRoom(string name)
        {
            if (!_chatRooms.ContainsKey(name))
            {
                _chatRooms[name] = new ChatRoom() {Name = name };
                return true;
            }
            return false;
        }

        public bool JoinRoom(string roomName, User username)
        {
            if (_chatRooms.TryGetValue(roomName, out var room))
            {
                room.Users.Add(username);
                return true;
            }
            return false;
        }

        public bool SendMessage(User sender, string roomName, string content)
        {
            if(_chatRooms.TryGetValue(roomName, out var room))
            {
                room.Messages.Add(new Message { Sender = sender, Content = content });
                return true;
            }
            return false;
        }
    }
}
