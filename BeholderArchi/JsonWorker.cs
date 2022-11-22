using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BeholderArchi;

namespace BeholderArchi
{
    public class AllChats {
        public Chats[] chatsA { get; set; }
    }
    public class Chats {
        public long? chatId { get; set; }
        public Dictionary<string,ChatInfo>? _chatInfo { get; set; }
    }
    public class ChatInfo {
        public string? name { get; set; }
        public string? bio { get; set; }
    }

    internal class JsonWorker
    {
        public static void SaveChat(long chatId, string nameFrom, string? bioFrom)
        {
            var chats = new Chats
            {
                chatId = chatId,
                _chatInfo = new Dictionary<string, ChatInfo>
                {
                    ["ChatInfo"] = new ChatInfo { name = nameFrom, bio = bioFrom },
                },
            };
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(chats, options);
            BeholderArchi.Program.AddChatInfo(jsonString);
        }
    }
}
