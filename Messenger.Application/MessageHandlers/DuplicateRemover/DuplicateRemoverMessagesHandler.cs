using System;
using Messenger.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace Messenger.Application.MessageHandlers
{
    public class DuplicateRemoverMessagesHandler: IMessagesHandler
    {
        public Message HandleMessage(IEnumerable<Message> messages, Message newMessage)
        {
            if (messages == null || newMessage == null || newMessage.Sender == null) return null;
            var lastOldMessage = messages.LastOrDefault(message => message?.Sender?.Id == newMessage.Sender.Id);
            return (lastOldMessage != null &&
                    newMessage.Sender.Id == lastOldMessage.Sender.Id &&
                    newMessage.Content == lastOldMessage.Content) ? null : newMessage;
        }
    }
}