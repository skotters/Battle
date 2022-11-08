using System;
using System.IO;

namespace Battle
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White; //force white if not default.
            Console.SetWindowSize(70, 30); //only works for windows machines

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
                    System.Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("\n\nInvalid Entry (ok)");
                    Console.ReadKey();
                    ScreenManager.IntroScreen();
                }

            } while (!validEntry);
        }
    }
}