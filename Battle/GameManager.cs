using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Battle.Enemies;

namespace Battle
{
    public class GameManager
    {
        public void StartGame()
        {
            string startingName = "";
            bool visitStore;

            startingName = GetValidPlayerName();

            Player p1 = new Player();
            p1.Name = startingName;

            ScreenManager.AskToVisitStore(p1.Name);
            visitStore = PromptUserForStoreEntry(p1.Name);
            if(visitStore) { Store.GoShopping(p1); }

            BattleManager battle = new BattleManager();
            battle.BattleSetup(p1);

            ScreenManager.TravelingToBakery();
            BakeryManager.OpenBakery(p1);

            Console.WriteLine("\n\ntemporary hold........");
            Console.ReadLine();

            Console.WriteLine("game over.");
        }

        public static string GetValidPlayerName()
        {
            bool validName;
            string name;

            do
            {
                validName = true;
                ScreenManager.GetPlayerNameScreen();
                name = Console.ReadLine();

                if (
                    name.Count(f => f == ' ') == name.Length ||
                    name[0] == ' '
                   ) //if name starts with space or is all spaces...
                        {
                            Console.WriteLine("\nInvalid name  (ok)");
                            Console.ReadKey();
                            ScreenManager.GetPlayerNameScreen();
                            validName = false;
                        }

            } while (!validName);

            return name;
        }

        public static bool PromptUserForStoreEntry(string playerName)
        {
            bool validEntry = false;
            do
            {
                string playerOption;
                var keyPress = Console.ReadKey();
                playerOption = keyPress.KeyChar.ToString();

                if (playerOption.ToLower() == "y")
                {
                    return true;
                }
                else if (playerOption.ToLower() == "n")
                {
                    Console.Clear();
                    Console.WriteLine($"You are bold, {playerName}.");
                    Console.WriteLine("May your pockets be lined with enough gold for the bakery!");
                    Console.WriteLine("\n (Press any key) ");
                    Console.ReadKey();
                    return false;
                }
                else
                {
                    Console.WriteLine("\n\nInvalid Entry (ok)");
                    Console.ReadKey();
                    ScreenManager.IntroScreen();
                }

                return true; //default...

            } while (!validEntry);
        }
    }
}
