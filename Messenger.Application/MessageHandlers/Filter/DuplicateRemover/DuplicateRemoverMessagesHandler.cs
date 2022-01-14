using System;
using Messenger.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using Messenger.Application.MessageHandlers.Filter;

namespace Messenger.Application.MessageHandlers
{
    public class DuplicateRemoverMessagesHandler: FilterMessagesHandler
    {
        protected override bool IsMessageAllowed(IEnumerable<Message> oldMessages, Message newMessage)
        {
            if (oldMessages == null || newMessage == null || newMessage.Sender == null) return false;
            var lastOldMessage = oldMessages.LastOrDefault(message => message?.Sender?.Id == newMessage.Sender.Id);
            return lastOldMessage == null ||
                    newMessage.Sender.Id != lastOldMessage.Sender.Id ||
                    newMessage.Content != lastOldMessage.Content;
        }
    }
}