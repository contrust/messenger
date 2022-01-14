namespace Messenger.Application.MessageHandlers
{
    public class StarReplacerMessagesHandler: TextConverterMessagesHandler
    {
        public StarReplacerMessagesHandler(): base(new StarReplacerTextConverter()) {}
    }
}