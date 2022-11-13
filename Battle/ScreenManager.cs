using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Battle.Items;
using System.Reflection;
using System.Threading;


namespace Battle
{
    public static class ScreenManager
    {
        public static void IntroScreen()
        {
            Console.Clear();

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
        }
        public static void GetPlayerNameScreen()
        {
            Console.Clear();
            Console.WriteLine
                (
                    "\n\nYour hunger seems to be insatiable.\n" +
                    "It occurs to you that you will not be accompanied alone\n" +
                    "on this adventure since your belly does have a mind of its own."
                );

            Console.WriteLine("\nWhat is your name, traveller?\n");
            Console.Write("--> ");
        }
        public static void AskToVisitStore(string playerName)
        {
            Console.Clear();
            Console.WriteLine
                (
                    $"\n{playerName}, before you go, would you like to visit the store?\n" +
                    "\nThis will be your only opportunity to " +
                    "\nbuy things to help in your battles."
                );

            Console.Write("\n(y/n): ");
        }
        public static void BattleScreenUpdate(Monster monster, Player player, string statusText, int whoseturn)
        {
            string enemy;

            if (monster.CurrentHP > 0)
                enemy = File.ReadAllText("Text/" + monster.Type + ".txt");
            else //display the dead version of the monster. File prefix is "dead" on those monsters.
                enemy = File.ReadAllText("Text/dead" + monster.Type + ".txt");
            
            Console.Clear();

            Console.WriteLine($"\tName: {monster.Name}");
            Console.WriteLine($"\tType: {monster.Type}");
            Console.WriteLine($"\tHP:   {monster.CurrentHP}");

            PrintVisualMeter(monster.StartingHP, monster.CurrentHP, true);

            Console.WriteLine();
            Console.WriteLine(enemy);
            Console.WriteLine($"\n\t{statusText}\n\n");
            Console.WriteLine($"\t{player.Name}");
            Console.WriteLine($"\tHP:   {player.CurrentHP}\t{player.PlayerCondition}");

            PrintVisualMeter(player.StartingHP, player.CurrentHP, true);

            Console.WriteLine($"\tMP:   {player.CurrentMP}\t");

            PrintVisualMeter(player.StartingMP, player.CurrentMP, false);

            Console.WriteLine($"\tGold: {player.gold}");
            Console.WriteLine();

            if (whoseturn == 1)
            {
                /* the magic menu will not go into its own screen like 
                 * the inventory does. Magic will still be a set of 
                 * player options that can be performed while the 
                 * user has visibility of the MP meter/bar on the battle screen.
                 */
                if (!player.MagicMenuOpen)
                    Console.WriteLine(GetPlayerOptions());
                else
                    Console.WriteLine(GetMagicOptions());
                
                Console.Write("\nAction: ");
            }
            else
                Console.WriteLine("  (press any key)");
        }
        public static string GetPlayerOptions()
        {
            return "1) Attack\n" +
                   "2) Strong Attack\t(50% miss chance)\n" +
                   "3) Magic\n" +
                   "4) Item\n" +
                   "5) Run Away";
        }
        public static string GetMagicOptions()
        {
            return $"1) Fireball\t\t {MagicManager.FIREBALL_MP_COST}MP\n" +
                   $"2) Arcane missiles\t {MagicManager.ARCANE_MP_COST}MP\n" +
                   $"3) Heal\t\t\t {MagicManager.HEAL_MP_COST}MP\n" +
                   $"4) Go back";
        }
        public static void ShowInventoryScreen(Player player, int itemCount)
        {
            if (itemCount > 0)
            {
                Console.Clear();
                Console.WriteLine("current inventory...\n\n");

                Console.WriteLine(InventoryManager.GetSubtotaledInventory(player.Inventory));
                Console.WriteLine("\n9) Go back");
                Console.WriteLine($"\nCurrent HP: {player.CurrentHP}/{player.StartingHP}");
                Console.WriteLine($"Current MP: {player.CurrentMP}/{player.StartingMP}");
                Console.WriteLine($"Status: {player.PlayerCondition}");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("\n\n\t(Inventory is empty...)");

                Console.WriteLine("\n9) Go back");
                Console.WriteLine($"\nCurrent HP: {player.CurrentHP}/{player.StartingHP}");
                Console.WriteLine($"Current MP: {player.CurrentMP}/{player.StartingMP}");
                Console.WriteLine($"Status: {player.PlayerCondition}");
            }
        }
        public static void AfterFightScreen(Player player)
        {
            Console.Clear();
            Console.WriteLine("You have won a battle.\n");
            Console.WriteLine("What would you like to do next?\n\n");
            Console.WriteLine("1) Continue fighting!");
            Console.WriteLine("2) Give up and go to the bakery\n\n");
            Console.WriteLine($"Current gold: {player.gold}\n\n");
            Console.Write("Action: ");
            
        }
        public static void VictoryScreen(Player player)
        {
            Console.Clear();
            Console.WriteLine($"~~~ Congratulations! ~~~");
            Console.WriteLine($"\n{player.Name}, you have won all five fights!\n\n");
            Console.WriteLine("(press any key to continue)");
            Console.ReadKey();
        }
        public static void StoreFront(int goldAmount)
        {
            Console.Clear();
            Console.WriteLine("Welcome to the store!");
            Console.WriteLine("What would you like to purchase?\n");

            Console.WriteLine("\tGold: " + goldAmount);

            Console.WriteLine("\nName\t\t\tDesc\t\t\tCost");
            Console.WriteLine("-------------------------------------------------------");
            Console.WriteLine
                (
                    "1) Health Potion\theals 25hp\t\t20g" +
                    "\n2) Magic Potion\t\trestores 25mp\t\t20g" +
                    "\n3) Antidote\t\tcures poison\t\t10g" +
                    "\n4) Sword\t\t+2 dmg melee\t\t30g" +
                    "\n5) Armor\t\t-2 dmg taken\t\t40g" +
                    "\n\n6) (Leave)\n"
                );
        }
        //startingAmt: beginning HP or MP
        //currentAmt:  current HP or MP
        //isHealthBar: used to determine if bar should be green or blue
        public static void PrintVisualMeter(int startingAmt, int currentAmt, bool isHealthBar)
        {
            const int LOW_BAR_AMOUNT = 4;
            string rawBarString= VisualMeter.GetFullMeterString(startingAmt, currentAmt);
            int barCount = rawBarString.Count(f => f == '|');

            Console.Write("\t[");

            if (barCount <= LOW_BAR_AMOUNT) // display the bar red whether HP or MP
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(PrintBarsAndSpaces(barCount));

                if(Program.isMacintosh)
                    Console.ForegroundColor = ConsoleColor.Black;
                else
                    Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine("]");
            }
            else 
            {
                if(isHealthBar)
                    Console.ForegroundColor = ConsoleColor.Green;   //health uses green
                else
                    Console.ForegroundColor = ConsoleColor.Blue;    //player mp meter uses blue

                Console.Write(PrintBarsAndSpaces(barCount));

                if (Program.isMacintosh)
                    Console.ForegroundColor = ConsoleColor.Black;
                else
                    Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine("]");
            }
        }
        public static string PrintBarsAndSpaces(int barCount)
        {
            string barString = "";

            for (int i = 1; i <= 20; i++)
            {
                if (i <= barCount)
                    barString += "|";
                else
                    barString += " ";
            }
            return barString;
        }
        public static void TravelingToBakery()
        {
            //screen only showed once per game at game end.
            Console.Clear();
            Console.WriteLine("You are now headed to the bakery!");

            string bakeryBuilding = File.ReadAllText("Text/" + "bakerybuilding.txt");
            Console.WriteLine(bakeryBuilding);

            Console.WriteLine("(Press any key to continue)");
            Console.ReadKey();
        }
        public static void BakeryScreen(Player player)
        {
            Console.Clear();

            string bakeryChoices = File.ReadAllText("Text/" + "bakery.txt");
            
            Console.WriteLine(bakeryChoices);
            Console.WriteLine();
            Console.WriteLine($"Gold: {player.gold}\n");
            Console.Write("Selection:  ");
        }
        public static void WaterPurchasedScreen()
        {
            Console.Clear();

            Console.WriteLine
                (
                    "It was a rough battle to get here...\n" +
                     "yet all you walked away with was your life\n " +
                     "and something to quench your thirst.\n\n" +
                     "Only your mind can taste the sweet glaze\n" +
                     "of what ought to have been yours.\n\n" +

                     "But there are always more battles..."
                );

            Console.WriteLine("(Press any key to continue)");
            Console.ReadKey();
        }
        public static void GlazedDonutPurchasedScreen()
        {
            Console.Clear();

            Console.WriteLine
                (
                    "The donut glaze crumbled around your fingers\n" + 
                    "as you tossed the whole donut into your mouth.\n\n" + 
                    "Although the reward of your battles proved to be\n" + 
                    "worth it, the lure of bacon wafting through the air\n" + 
                    "was a reminder that you could have fought better.\n\n" +
                    
                    "But there are always more battles..."
                );

            Console.WriteLine("(Press any key to continue)");
            Console.ReadKey();
        }
        public static void MapleBaconDonutPurchasedScreen()
        {
            Console.Clear();

            Console.WriteLine
                (
                    "It was at this point that the maple bacon donut\n" +
                    "was finally in your grasp. Holding it up to the sunlight\n" + 
                    "to examine its beauty as a swordsman observes the \n" + 
                    "sharpness of his blade, you shed a single tear at the\n" +
                    "sight of crispy brown pork delicately nestled within\n" + 
                    "a layer of sugar born of a tree's sweet gift.\n\n" + 

                    "The battle was over and your donut was gone in the blink\n" +
                    "of an eye. Satisfied, you knew victory had been won. But your\n" + 
                    "stomach companion would once again need to be cared for.\n\n" + 

                    "And of course there are always more battles..."
                );

            Console.WriteLine("(Press any key to continue)");
            Console.ReadKey();
        }
        public static void GameNeverStarted()
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
        }
        public static void DeathScreen(string playerName)
        {
            Console.Clear();

            Console.WriteLine($"You have fallen in battle, {playerName}.");
            Console.WriteLine($"\nTurns out this one was a worthy foe after all...");
            Console.WriteLine($"\n\n(Press any key)");
        }
        public static void GameOverScreen() //ASCII game over banner
        {
            Console.Clear();

            string gameOverText = File.ReadAllText("Text/" + "gameover.txt");
            Console.WriteLine(gameOverText);
        }
    }
}
