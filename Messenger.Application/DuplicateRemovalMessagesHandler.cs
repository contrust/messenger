using System;
using Messenger.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace Messenger.Application.MessageHandlers
{
    public class DuplicateRemovalMessagesHandler: IMessagesHandler
    {
        public Message HandleMessage(IEnumerable<Message> messages, Message newMessage)
        {
            if (messages == null) return null;
            var lastOldMessage = messages.LastOrDefault();
            return (lastOldMessage != null && 
                    newMessage != null &&
                    newMessage.Sender?.Id == lastOldMessage.Sender?.Id &&
                    newMessage.Content == lastOldMessage.Content) ? null : newMessage;
        }
    }
}