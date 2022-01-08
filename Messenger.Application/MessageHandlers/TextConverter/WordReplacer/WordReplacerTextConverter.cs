using System;
using System.Linq;
using System.Collections.Generic;

namespace Messenger.Application.MessageHandlers
{
    public abstract class WordReplacingTextConverter: ITextConverter
    {
        protected HashSet<string> wordsForReplace { get; set; }

        public string Convert(string text)
        {
            return String.Join(' ', text.Split().Select(ConvertWord).Where(word => word != null));
        }

        protected abstract string ConvertWord(string word);
    }
}