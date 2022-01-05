using System.Collections.Generic;

namespace Messenger.Application.MessageHandlers
{
    public class StarReplacerTextConverter: WordReplacingTextConverter
    {
        public StarReplacerTextConverter(HashSet<string> wordsForReplace = null)
        {
            this.wordsForReplace = wordsForReplace;
        }
        public override string ConvertWord(string word)
        {
            return wordsForReplace.Contains(word) ? new string('*', word.Length) : word;
        }
    }
}