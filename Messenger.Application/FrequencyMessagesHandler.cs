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
            if (messages == null) return null;
            var lastOldMessage = messages.LastOrDefault();
            return (lastOldMessage != null &&
                    newMessage != null && 
                    lastOldMessage.Sender?.Id == newMessage.Sender?.Id && 
                    (newMessage.Date - lastOldMessage.Date).Seconds < minMessagesFrequencyInSeconds) ? null : newMessage;
        }
    }
}