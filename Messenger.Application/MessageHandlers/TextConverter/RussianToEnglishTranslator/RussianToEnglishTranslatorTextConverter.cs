namespace Messenger.Application.MessageHandlers
{
    public class RussianToEnglishTranslatorTextConverter: ITextConverter
    {
        public string Convert(string text)
        {
            return RussianToEnglishTranslator.Translate(text);
        }
    }
}