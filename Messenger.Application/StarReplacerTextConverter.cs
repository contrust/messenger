using System.Collections.Generic;

namespace Messenger.Application.MessageHandlers
{
    public class StarReplacerTextConverter: WordReplacingTextConverter
    {
        public StarReplacerTextConverter()
        {
            this.wordsForReplace = new HashSet<string>() {"goose"};
        }
        public override string ConvertWord(string word)
        {
            return wordsForReplace.Contains(word) ? new string('*', word.Length) : word;
        }
    }
}