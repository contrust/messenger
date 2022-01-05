using System;
using Messenger.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace Messenger.Application.MessageHandlers
{
    public class FrequencyMessagesHandler: IMessagesHandler
    {
        private int minMessagesFrequencyInSeconds { get; }
        public FrequencyMessagesHandler(int minMessagesFrequencyInSeconds = 5)
        {
            this.minMessagesFrequencyInSeconds = minMessagesFrequencyInSeconds;
        }
        public Message HandleMessage(IEnumerable<Message> messages, Message newMessage)
        {
            if (messages == null || newMessage == null || newMessage.Sender == null) return null;
            var lastOldMessage = messages.LastOrDefault(message => message?.Sender?.Id == newMessage.Sender.Id);
            return (lastOldMessage != null &&
                    lastOldMessage.Sender.Id == newMessage.Sender.Id && 
                    (newMessage.Date - lastOldMessage.Date).TotalSeconds < minMessagesFrequencyInSeconds) ? null : newMessage;
        }
    }
}