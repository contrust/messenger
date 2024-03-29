using Messenger.Domain.Models;
using System.Collections.Generic;

namespace Messenger.Application.MessageHandlers
{
    public abstract class TextConverterMessagesHandler: IMessagesHandler
    {
        protected ITextConverter converter { get; }
        public TextConverterMessagesHandler(ITextConverter converter)
        {
            this.converter = converter;
        }
        public Message HandleMessage(IEnumerable<Message> messages, Message newMessage)
        {
            if (messages == null || newMessage == null || newMessage.Content == null) return null;
            newMessage.Content = converter.Convert(newMessage.Content);
            return newMessage;
        }
    }
}