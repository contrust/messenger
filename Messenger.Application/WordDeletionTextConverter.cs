using System.Collections.Generic;

namespace Messenger.Application.MessageHandlers
{
    public class WordDeletionTextConverter: WordReplacingTextConverter
    {
        public WordDeletionTextConverter(HashSet<string> wordsForReplace = null)
        {
            this.wordsForReplace = wordsForReplace ?? new HashSet<string>();
        }
        public override string ConvertWord(string word)
        {
            return wordsForReplace.Contains(word) ? null : word;
        }
    }
}