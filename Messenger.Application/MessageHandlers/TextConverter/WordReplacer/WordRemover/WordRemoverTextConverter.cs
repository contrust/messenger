using System.Collections.Generic;

namespace Messenger.Application.MessageHandlers
{
    public class WordRemoverTextConverter: WordReplacingTextConverter
    {
        public WordRemoverTextConverter(HashSet<string> wordsForReplace = null)
        {
            this.wordsForReplace = wordsForReplace ?? new HashSet<string>();
        }
        public override string ConvertWord(string word)
        {
            return wordsForReplace.Contains(word) ? null : word;
        }
    }
}