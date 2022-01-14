using System;
using System.Collections.Generic;
using Messenger.Domain.Models;

namespace Messenger.Application.MessageHandlers.Filter
{
    public abstract class FilterMessagesHandler: IMessagesHandler
    {
        public Message HandleMessage(IEnumerable<Message> oldMessages, Message newMessage)
        {
            return IsMessageAllowed(oldMessages, newMessage) ? newMessage : null;
        }

        protected abstract bool IsMessageAllowed(IEnumerable<Message> oldMessages, Message newMessage);
    }
}