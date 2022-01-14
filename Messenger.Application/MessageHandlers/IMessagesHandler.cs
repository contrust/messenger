using Messenger.Domain.Models;
using System.Collections.Generic;

namespace Messenger.Application.MessageHandlers
{
    public interface IMessagesHandler
    {
        public Message HandleMessage(IEnumerable<Message> oldMessages, Message newMessage);
    }
}