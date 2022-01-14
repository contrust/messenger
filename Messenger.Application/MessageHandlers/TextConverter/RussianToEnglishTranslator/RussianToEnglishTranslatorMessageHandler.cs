namespace Messenger.Application.MessageHandlers
{
    public class RussianToEnglishTranslatorMessageHandler: TextConverterMessagesHandler
    {
        public RussianToEnglishTranslatorMessageHandler() : base(new RussianToEnglishTranslatorTextConverter()) { }
    }
}