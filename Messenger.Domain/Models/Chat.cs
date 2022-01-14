using System.Collections.Generic;

namespace Messenger.Domain.Models
{
    public class Chat
    {
        public Chat()
        {
            Messages = new List<Message>();
            Participants = new List<ChatParticipant>();
        }
        public int Id {get; set;}
        public string Name {get; set;}
        public ChatType Type {get; set;}
        public ICollection<ChatParticipant> Participants {get; set;}
        public ICollection<Message> Messages {get; set;}
    }
}