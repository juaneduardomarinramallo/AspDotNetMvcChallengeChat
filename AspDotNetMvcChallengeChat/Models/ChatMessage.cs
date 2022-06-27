using System;
using System.Collections.Generic;

namespace AspDotNetMvcChallengeChat.Models
{
    public partial class ChatMessage
    {
        public long Id { get; set; }
        public string UserId { get; set; } = null!;
        public int ChatRoomId { get; set; }
        public string? Message { get; set; }

        public virtual ChatRoom ChatRoom { get; set; } = null!;
        public virtual AspNetUser User { get; set; } = null!;
    }
}
