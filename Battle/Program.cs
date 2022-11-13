using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Battle
{
    internal class Program
    {
        public static bool isMacintosh;
        static void Main(string[] args)
        {
            if(RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                isMacintosh = true;

            if (!isMacintosh)
            {
                Console.ForegroundColor = ConsoleColor.White; //force white if not default.
                //Console.SetWindowSize(70, 30);
            }

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
                    System.Environment.Exit(0);
                }
                else if(playerOption.ToLower() == "n")
                {
                    ScreenManager.GameNeverStarted();
                    System.Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("\n\nInvalid Entry (press any key)");
                    Console.ReadKey();
                    ScreenManager.IntroScreen();
                }

            } while (!validEntry);
        }
    }
}