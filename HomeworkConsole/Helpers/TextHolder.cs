using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace HomeworkConsole.Helpers
{
    public sealed class TextHolder
    {
        private static readonly TextHolder instance = new TextHolder();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static TextHolder()
        {
        }

        public static TextHolder T
        {
            get
            {
                return instance.LoadResources();
            }
        }

        private TextHolder()
        {
        }

        private TextHolder LoadResources()
        {            
            Assembly ass = Assembly.GetAssembly(new Program().GetType());
            using Stream stream = ass.GetManifestResourceStream("HomeworkConsole.AppResources.ExceptionStrings.json");
            using StreamReader reader = new StreamReader(stream);
            string result = reader.ReadToEnd();
            TextHolder t = JsonConvert.DeserializeObject<TextHolder>(result);

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
