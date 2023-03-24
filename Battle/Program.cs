using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Battle
{
    internal class Program
    {
        public static bool isMacintosh;
        static void Main(string[] args)
        {

            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                isMacintosh = true;

            if (!isMacintosh)
                Console.ForegroundColor = ConsoleColor.White; //force white if not default.
                
            Introduction();
        }

        public static void Introduction()
        {
            ScreenManager.IntroScreen();

            bool validEntry = false;

            do
            {
                string playerOption;
                var keyPress = Console.ReadKey();
                playerOption = keyPress.KeyChar.ToString();

                if (playerOption.ToLower() == "y")
                {
                    //play a game
                    GameManager game = new GameManager();
                    game.StartGame();
                    validEntry = true;
                    
                }
                else if(playerOption.ToLower() == "n")
                {
                    ScreenManager.GameNeverStarted();
                }
                else
                {
                    ErrorLogger.UserInputError(MethodBase.GetCurrentMethod().Name, "Bad input on y/n prompt");
                    ScreenManager.IntroScreen();
                }

            } while (!validEntry);

            ErrorLogger.WriteErrorsToFile(); //final program action
        }
    }
}