namespace Messenger.Domain.Models
{
    public class ChatParticipant
    {
        public int Id { get; set; }
        public User Participant { get; set; }
        public ChatRole Role { get; set; }
    }
}