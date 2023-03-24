using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle
{
    public static class ErrorLogger
    {
        public static List<string> errorList = new List<string>();

        public static void LogAnError(string methodName, string errorDescription)
        {
            string dateTime = DateTime.Now.ToString("MM/dd/yy");
            methodName = methodName.PadRight(30);

            string fullEntryLine = ($"\n{dateTime}  {methodName} {errorDescription}");

            errorList.Add(fullEntryLine);

        }
        public static void WriteErrorsToFile()
        {
            if (errorList.Count > 0)
            {
                string path = "ErrorLog/ErrorLog.txt";

                foreach (string errorEntry in errorList)
                {
                    File.AppendAllText(path, errorEntry);
                }
            }
        }

        public static void UserInputError(string methodName, string errorDescription)
        {
            LogAnError(methodName, errorDescription);
            Console.WriteLine("\n\tInvalid entry.    (press any key)");
            Console.ReadKey();
        }
    }
}
