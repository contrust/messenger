using System.Collections.Generic;

namespace Messenger.Application.MessageHandlers
{
    public class StarReplacerTextConverter: WordReplacingTextConverter
    {
        public StarReplacerTextConverter(HashSet<string> wordsForReplace = null)
        {
            this.wordsForReplace = wordsForReplace ?? new HashSet<string> {"lol", "rofl", "lmfao"};
        }

        protected override string ConvertWord(string word)
        {
            return wordsForReplace.Contains(word.ToLower()) ? new string('*', word.Length) : word;
        }
    }
}