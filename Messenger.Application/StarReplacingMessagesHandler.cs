namespace Messenger.Application.MessageHandlers
{
    public class StarReplacingMessagesHandler: TextConverterMessagesHandler
    {
        public StarReplacingMessagesHandler(): base(new StarReplacerTextConverter()) {}
    }
}