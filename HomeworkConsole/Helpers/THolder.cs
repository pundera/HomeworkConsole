using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace HomeworkConsole.Helpers
{
    public sealed class THolder
    {
        private static readonly THolder instance = new();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static THolder()
        {
        }

        public static THolder T
        {
            get
            {
                return LoadResources();
            }
        }

        private THolder()
        {
        }

        private static THolder LoadResources()
        {
            string dir = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string json = File.ReadAllText(@$"{dir}\AppResources\ExceptionStrings.json");
            THolder t = JsonConvert.DeserializeObject<THolder>(json);

            return t;
        }

        [JsonRequired]
        public string FileNotExist { get; set; }
        [JsonRequired]
        public string UriFormatNotCorrect { get; set; }
        [JsonRequired]
        public string Exception { get; set; }
        [JsonRequired]
        public string SecurityException { get; set; }
        [JsonRequired]
        public string FileEmpty { get; set; }
        [JsonRequired]
        public string NotValidXml { get; set; }
        [JsonRequired]
        public string Success { get; set; }
        [JsonRequired]
        public string IOException { get; set; }
        [JsonRequired]
        public string NoTagInXml { get; set; }

    }

}
