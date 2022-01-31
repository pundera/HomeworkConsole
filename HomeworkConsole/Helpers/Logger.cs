using System;
using System.IO;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;

namespace HomeworkConsole.Helpers
{
    internal static class Logger
    {
        public static void Log(string message, params object[] parameters)
        {
            try
            {
                if (parameters == null || parameters.Length == 0)
                {
                    Console.WriteLine(message);
                }
                else
                {
                    Console.WriteLine(string.Format(message, parameters));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Logging exception: Parameters?");
                Console.WriteLine($"Exception: {ex.Message}");
                Console.WriteLine($"Message: {message}");
            }
        }
    }
}
