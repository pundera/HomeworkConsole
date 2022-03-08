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

        public string FileNotExist { get; set; }
        public string UriFormatNotCorrect { get; set; }
        public string Exception { get; set; }
        public string SecurityException { get; set; }
        public string FileEmpty { get; set; }
        public string NotValidXml { get; set; }
        public string Success { get; set; }
        public string IOException { get; set; }
        public string NoTagInXml { get; set; }

    }

}
