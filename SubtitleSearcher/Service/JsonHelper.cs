using System.Collections.Generic;
using BinZone.SubtitleSearcher.Model;
using Newtonsoft.Json;

namespace BinZone.SubtitleSearcher.Service
{
    class JsonHelper
    {
        public static List<Subtitle> Deserialize(string subtitles)
        {
            return JsonConvert.DeserializeObject<List<Subtitle>>(Preprocessing(subtitles));
        }

        private static string Preprocessing(string subtitles)
        {
            var temp = subtitles.Remove(subtitles.LastIndexOf('}'), 1).Replace("{\"sublist\":", "");
            temp = temp.Replace("scid", Subtitle.IdPropertyName);
            temp = temp.Replace("sname", Subtitle.NamePropertyName);
            temp = temp.Replace("sext", Subtitle.ExtensionPropertyName);
            temp = temp.Replace("language", Subtitle.LanguagePropertyName);
            temp = temp.Replace("simility", Subtitle.ConfidencePropertyName);
            temp = temp.Replace("surl", Subtitle.UrlPropertyName);
            temp = temp.Replace("{}", "");

            return temp;
        }
    }
}