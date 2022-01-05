using System;
using System.Linq;
using System.Collections.Generic;

namespace Messenger.Application.MessageHandlers
{
    public abstract class WordReplacingTextConverter: ITextConverter
    {
        protected HashSet<string> wordsForReplace;

        public string Convert(string text)
        {
            return String.Join(' ', text.ToLower().Split().Select(word => ConvertWord(word)).Where(word => word != null));
        }

        public abstract string ConvertWord(string word);
    }
}