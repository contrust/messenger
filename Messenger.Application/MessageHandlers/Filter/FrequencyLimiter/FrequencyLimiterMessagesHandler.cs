using System;
using Messenger.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using Messenger.Application.MessageHandlers.Filter;

namespace Messenger.Application.MessageHandlers
{
    public class FrequencyLimiterMessagesHandler: FilterMessagesHandler
    {
        private int minMessagesFrequencyInSeconds { get; }
        public FrequencyLimiterMessagesHandler(int minMessagesFrequencyInSeconds = 5)
        {
            this.minMessagesFrequencyInSeconds = minMessagesFrequencyInSeconds;
        }
        protected override bool IsMessageAllowed(IEnumerable<Message> oldMessages, Message newMessage)
        {
            if (oldMessages == null || newMessage is not { Sender: { } }) return false;
            var lastOldMessage = oldMessages.LastOrDefault(message => message?.Sender?.Id == newMessage.Sender.Id);
            return lastOldMessage == null || 
                   lastOldMessage.Sender.Id != newMessage.Sender.Id || 
                   (newMessage.Date - lastOldMessage.Date).TotalSeconds >= minMessagesFrequencyInSeconds;

        }
    }
}