using System.Collections.Generic;

namespace Messenger.Domain.Models
{
    public class Chat
    {
        public Chat()
        {
            Messages = new List<Message>();
            Participants = new List<User>();
        }
        public enum DialogType
        {
            MultiUserChat,
            PrivateChat,
            PersonalChat,
            Channel
        }
        public int Id {get; set;}
        public string Name {get; set;}
        public DialogType Type {get; set;}
        public ICollection<User> Participants {get; set;}
        public ICollection<Message> Messages {get; set;}
    }
}