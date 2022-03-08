using System;
using System.IO;
using System.Reflection;
using System.Security;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using HomeworkConsole.Helpers;
using Newtonsoft.Json;

namespace HomeworkConsole
{
    public class Document
    {
        public string Title { get; set; }
        public string Text { get; set; }
    }

    public class Program
    {
        static readonly Assembly assembly = Assembly.GetAssembly(new Document().GetType());
        static readonly string currentDirectory = Path.GetDirectoryName(assembly.Location);
        static readonly string sourceFileName = Path.GetFullPath(Path.Combine(currentDirectory, "..\\..\\..\\Source Files\\Document1.xml"));
        static readonly string targetFileName = Path.GetFullPath(Path.Combine(currentDirectory, "..\\..\\..\\Target Files\\Document1.json"));

        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            string source = sourceFileName;
            string target = targetFileName;

            bool justResult = false;

            if (args != null && args.Length == 2)
            {
                // parameters -> source target
                source = args[0];
                target = args[1];
                justResult = true;
            }

            XDocument xml = null;
            if (LoadXml(ref xml, source))
            {
                if (WriteJson(xml, target))
                {
                    if (!justResult)
                    {
                        Logger.Log(t.Success, target);
                    }
                }
            }
        }

        private static readonly THolder t = THolder.T;

        private static bool LoadXml(ref XDocument output, string source)
        {
            if (!File.Exists(source))
            {
                Logger.Log(t.FileNotExist, source);
                return false;
            }

            string text;

            try
            {
                // Beginning with C# 8.0
                // you can use the following alternative syntax for the using statement that doesn't require braces
                using FileStream sourceStream = File.Open(source, FileMode.Open);
                var reader = new StreamReader(sourceStream, Encoding.UTF8);
                text = reader.ReadToEnd();
            }
            catch (UriFormatException uriEx)
            {
                Logger.Log(t.UriFormatNotCorrect, source);
                Logger.Log(t.Exception, uriEx.Message);
                return false;
            }
            catch (SecurityException securityEx)
            {
                Logger.Log(t.SecurityException, source);
                Logger.Log(t.Exception, securityEx.Message);
                return false;
            }
            catch (Exception ex)
            {
                Logger.Log(t.Exception, ex.Message);
                return false;
            }

            if (text == null)
            {
                Logger.Log(t.FileEmpty, source);
                return false;
            }

            return XDocFromString(ref output, text);

        }

        static bool CheckElementInXml(XDocument xdoc, string tag)
        {
            XElement titleEl = xdoc.Root.Element(tag);
            if (titleEl == null)
            {
                Logger.Log(THolder.T.NoTagInXml, tag);
                return false;
            }
            return true;
        }

        static public bool XDocFromString(ref XDocument output, string text)
        {
            try
            {
                output = XDocument.Parse(text);
            }
            catch (XmlException xmlEx)
            {
                // file can have extension .xml but another content (NOT valid XML)
                Logger.Log(THolder.T.NotValidXml);
                Logger.Log(THolder.T.Exception, xmlEx.Message);
                return false;
            }
            catch (Exception ex)
            {
                Logger.Log(THolder.T.Exception, ex.Message);
                return false;
            }

            if (!CheckElementInXml(output, "title")) return false;
            if (!CheckElementInXml(output, "text")) return false;

            return true;
        }

        public static bool WriteJson(XDocument xml, string target)
        {
            string serializedDoc = SerilizeToDoc(xml);

            // Beginning with C# 8.0
            // you can use the following alternative syntax for the using statement that doesn't require braces
            using var targetStream = File.Open(target, FileMode.Create, FileAccess.Write);
            using var sw = new StreamWriter(targetStream, Encoding.UTF8);
            try
            {
                sw.Write(serializedDoc);
                Logger.Log(serializedDoc);
                return true;
            }
            catch (IOException ioEx)
            {
                Logger.Log(THolder.T.IOException, target);
                Logger.Log(THolder.T.Exception, ioEx.Message);
            }

            return false;
        }

        private static string SerilizeToDoc(XDocument xml)
        {
            Document doc = new Document
            {
                Title = xml.Root.Element("title").Value,
                Text = xml.Root.Element("text").Value
            };

            var serializedDoc = JsonConvert.SerializeObject(doc);
            return serializedDoc;
        }
    }
}