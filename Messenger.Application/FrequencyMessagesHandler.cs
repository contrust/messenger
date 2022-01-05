using Messenger.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace Messenger.Application.MessageHandlers
{
    public class FrequencyMessagesHandler: IMessagesHandler
    {
        private int minMessagesFrequencyInSeconds;
        public FrequencyMessagesHandler()
        {
            this.minMessagesFrequencyInSeconds = 5;
        }
        public Message HandleMessage(IEnumerable<Message> messages, Message newMessage)
        {
            var lastOldMessage = messages.LastOrDefault();
            return (lastOldMessage != null &&
                    newMessage != null && 
                    lastOldMessage.Sender.Id == newMessage.Sender.Id && 
                    (newMessage.Date - lastOldMessage.Date).Seconds < minMessagesFrequencyInSeconds) ? null : newMessage;
        }
    }
}