using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace HomeworkConsoleTests
{
    [TestClass]
    public class UnitTestXmlToJson
    {
        private readonly string dir = Directory.GetCurrentDirectory();
        private readonly string jsonExpected = "{\"Title\":\"Pozdrav\",\"Text\":\"Velmi vřelý pozdrav!!!\"}"; 

        [TestMethod]
        public void CorrectJsonResult()
        {
            using var sw = new StringWriter();

            // do NOT change CONTENT!!! ==> see 'jsonExpected'
            string source = Path.Combine(dir, @"DataFiles\Document1.xml");            
            // do NOT change CONTENT!!! ==> see 'jsonExpected'
            string target = Path.Combine(dir, @"DataFiles\Document1.json");

            Console.OutputEncoding = Encoding.UTF8;
            Console.SetOut(sw);
            string[] args = new[] { source, target };
            HomeworkConsole.Program.Main(args);
            var result = sw.ToString().Trim();
            Assert.AreEqual(jsonExpected, result);
        }
    }
}
