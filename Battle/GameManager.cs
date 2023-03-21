using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Battle.Enemies;

namespace Battle
{
    public class GameManager
    {
        public void StartGame()
        {
            bool gameOn;
            bool visitStore;
            string startingName;
            string phoneNumber;

            startingName = GetValidPlayerName();
            phoneNumber = GetValidPhoneNumber(startingName);

            do  //primary program loop
            {
                Player p1 = new Player();
                p1.Name = startingName;

                ScreenManager.AskToVisitStore(p1.Name);
                visitStore = PromptUserForStoreEntry(p1.Name);
                if (visitStore) { Store.GoShopping(p1); }

                ScreenManager.EnteringBattlefieldScreen(startingName, phoneNumber);

                BattleManager battle = new BattleManager();
                battle.BattleSetup(p1);

                if (p1.CurrentHP <= 0)
                {
                    ScreenManager.DeathScreen(p1.Name);
                    Console.ReadKey();
                }
                else
                {
                    ScreenManager.TravelingToBakery();
                    BakeryManager.OpenBakery(p1);
                }

                gameOn = GameOverContinuePrompt();

            } while (gameOn);
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
                            Console.WriteLine("\nInvalid name  (press any key)");
                            Console.ReadKey();
                            ScreenManager.GetPlayerNameScreen();
                            validName = false;
                        }

            } while (!validName);

            return name;
        }

        public static string GetValidPhoneNumber(string playerName)
        {
            bool validPhone;
            string phone;
            string pattern = @"\(\d{3}\)\d{3}-\d{4}";

            do
            {
                validPhone = true;
                ScreenManager.GetFriendsPhoneNumber(playerName);
                phone = Console.ReadLine();
                if (!Regex.IsMatch(phone, pattern))
                {
                    Console.WriteLine("\nInvalid phone  (press any key)");
                    Console.ReadKey();
                    ScreenManager.GetFriendsPhoneNumber(playerName);
                    validPhone = false;
                }

            } while (!validPhone);

            return phone;
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
                    //Console.Clear();
                    //Console.WriteLine($"You are bold, {playerName}.");
                    //Console.WriteLine("May your pockets be lined with enough gold for the bakery!");
                    //Console.WriteLine("\n (press any key) ");
                    //Console.ReadKey();
                    return false;
                }
                else
                {
                    Console.WriteLine("\n\nInvalid Entry (press any key)");
                    Console.ReadKey();
                    ScreenManager.AskToVisitStore(playerName);
                }


            } while (!validEntry);
            
            return true; //default...
        }
        public static bool GameOverContinuePrompt()
        {
            bool badUserEntry;
            int playerOption;

            do
            {
                ScreenManager.GameOverScreen();
                Console.Write("\n\n\t\tAction: ");
                badUserEntry = false;

                try
                {

                    var keyPress = Console.ReadKey();
                    playerOption = int.Parse(keyPress.KeyChar.ToString());

                    switch (playerOption)
                    {
                        case 1:
                            return true;
                        case 2: 
                            return false;
                        default: //no number match
                            Console.WriteLine("\n\tInvalid entry.    (press any key)");
                            Console.ReadKey();
                            ScreenManager.GameOverScreen();
                            badUserEntry = true;
                            break;
                    }
                }
                catch //non-number catch
                {
                    Console.WriteLine("\n\tInvalid entry.    (press any key)");
                    Console.ReadKey();
                    ScreenManager.GameOverScreen();
                    badUserEntry = true;
                }
            }
            while (badUserEntry);

            return false;

        }
    }
}
