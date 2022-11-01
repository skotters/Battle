using System;
using System.IO;

namespace Battle
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White; //force white if not default.

            Console.WriteLine
                (
                    "Welcome, traveller. You look hungry.\n" +
                    "But to satisfy that hunger, you must fight.\n"
                );

            Console.WriteLine
                (
                    "In this game, you may fight up to five monsters\n" + 
                    "in the hopes of gaining enough gold\n" + 
                    "to buy something at your favorite bakery.\n\n" + 
                    "Beware, the enemies may have some tricks in their\n" +
                    "arsenal that could have perilous results!\n" + 
                    "Also, be extra careful with enemies whose name\n" +
                    "starts with the letter \'V\'. They do start with\n" +
                    "more health than the average enemy.\n"
                );

            Console.WriteLine("Would you like to proceed in this endeavor?");

            Console.Write("(y/n): ");

            string answer = Console.ReadLine();

            if (answer.ToLower() == "y")
            {
                //play a game
                GameManager game = new GameManager();
                game.StartGame();
            }
            else
            {
                Console.Clear();
                Console.WriteLine
                    (
                        "\n\n\n\n\tWith a grumbly belly, you decide to venture\n" +
                        "\tinto the unknown in the hopes that another\n" +
                        "\tbakery presents itself."
                    );
                Console.WriteLine("\n\n\t\tPress any key to close.");
                Console.ReadKey();
                System.Environment.Exit(0);

            }
        }
    }
}