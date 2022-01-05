using System.Collections.Generic;

namespace Messenger.Application.MessageHandlers
{
    public class WordDeletionTextConverter: WordReplacingTextConverter
    {
        public WordDeletionTextConverter()
        {
            this.wordsForReplace = new HashSet<string>() {"makaka"};
        }
        public override string ConvertWord(string word)
        {
            return wordsForReplace.Contains(word) ? null : word;
        }
    }
}