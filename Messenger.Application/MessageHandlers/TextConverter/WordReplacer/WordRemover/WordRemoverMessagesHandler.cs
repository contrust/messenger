namespace Messenger.Application.MessageHandlers
{
    public class WordRemoverMessagesHandler: TextConverterMessagesHandler
    {
        public WordRemoverMessagesHandler(): base(new WordRemoverTextConverter()) {}
    }
}