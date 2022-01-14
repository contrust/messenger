using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net;
using System.Web;

namespace Messenger.Application.MessageHandlers
{
    public static class RussianToEnglishTranslator
    {
        public static string Translate(string sourceText)
        {
            string englishTranslation;
            try
            {
                string googleJsonTranslation;
                var url =
                    $"https://translate.googleapis.com/translate_a/single?client=gtx&" +
                    $"sl=ru&" +
                    $"tl=en&" +
                    $"dt=t&dj=1&q={HttpUtility.UrlEncode(sourceText)}";
                using (WebClient wc = new WebClient())
                {
                    wc.Headers.Add("user-agent",
                        "Mozilla/5.0 (Windows NT 6.1) " +
                        "AppleWebKit/537.36 (KHTML, like Gecko) " +
                        "Chrome/41.0.2228.0 Safari/537.36");
                    wc.Encoding = System.Text.Encoding.UTF8;
                    googleJsonTranslation = wc.DownloadString(url);
                }
                var json = (JObject)JsonConvert.DeserializeObject(googleJsonTranslation);
                englishTranslation = String.Join(' ', json["sentences"].Select(x => x["trans"]));
            }
            catch (Exception)
            {
                return null;
            }
            return englishTranslation;
        }
    }
}