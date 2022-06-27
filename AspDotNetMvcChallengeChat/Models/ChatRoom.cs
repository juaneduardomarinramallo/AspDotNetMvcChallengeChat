using System;
using System.Collections.Generic;

namespace AspDotNetMvcChallengeChat.Models
{
    public partial class ChatRoom
    {
        public ChatRoom()
        {
            ChatMessages = new HashSet<ChatMessage>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<ChatMessage> ChatMessages { get; set; }
    }
}
