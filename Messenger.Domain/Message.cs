using System;

namespace Messenger.Domain.Models
{
    public class Message
    {
        public int Id {get; set;}
        public User Sender {get; set;}
        public string Content {get; set;}
        public DateTime Date {get; set;}
        public int ChatId {get; set;}
    }
}