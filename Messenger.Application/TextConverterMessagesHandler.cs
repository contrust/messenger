using Messenger.Domain.Models;
using System.Collections.Generic;

namespace Messenger.Application.MessageHandlers
{
    public abstract class TextConverterMessagesHandler: IMessagesHandler
    {
        private ITextConverter converter;
        public TextConverterMessagesHandler(ITextConverter converter)
        {
            this.converter = converter;
        }
        public Message HandleMessage(IEnumerable<Message> messages, Message newMessage)
        {
            if (newMessage == null || newMessage.Content == null) return null;
            newMessage.Content = converter.Convert(newMessage.Content);
            return newMessage;
        }
    }
}