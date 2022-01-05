namespace Messenger.Application.MessageHandlers
{
    public class WordDeletionMessagesHandler: TextConverterMessagesHandler
    {
        public WordDeletionMessagesHandler(): base(new WordDeletionTextConverter()) {}
    }
}