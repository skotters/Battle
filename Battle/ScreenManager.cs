using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Battle.Items;
using System.Reflection;

namespace Battle
{
    public static class ScreenManager
    {
        public static string GetPlayerOptions()
        {
            return "1) Attack\n" +
                   "2) Strong Attack\n" +
                   "3) Magic\n" +
                   "4) Item\n" +
                   "5) Run Away";
        }

        public static void BattleScreenUpdate(IMonster monster, Player player, string statusText, int whoseturn)
        {
            Console.Clear();

            //string filepath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            //string enemy = File.ReadAllText(filepath + monster.Type + ".txt");
            string enemy = File.ReadAllText("Text/" + monster.Type + ".txt");

            Console.SetWindowSize(70, 30);

            Console.WriteLine($"\tName: {monster.Name}");
            Console.WriteLine($"\tType: {monster.Type}");

            Console.WriteLine($"\tHP:   {monster.CurrentHP}");
            Console.WriteLine($"\t      {monster.monsterHealthBar.BarConsoleUpdate(monster.StartingHP, monster.CurrentHP)}");
            Console.WriteLine();
            Console.WriteLine(enemy);

            Console.WriteLine($"\n\t{statusText}\n\n");
            
            Console.WriteLine($"\t{player.Name}");
            Console.WriteLine($"\tHP:   {player.CurrentHP}\t{player.PlayerCondition}");
            Console.WriteLine($"\t      {player.playerHealthBar.BarConsoleUpdate(player.StartingHP, player.CurrentHP)}");
            Console.WriteLine($"\tMP:   {player.CurrentMP}\t");
            Console.WriteLine($"\t      [||||temp|||||]");
            Console.WriteLine($"\tGold: {player.gold}");
            Console.WriteLine();

            if (whoseturn == 1)
            {
                Console.WriteLine(GetPlayerOptions());
                Console.Write("\nAction: ");
            }
            else
                Console.WriteLine("  (ok)");
        }

        public static void ShowInventoryScreen(Player player)
        {
            Console.Clear();
            Console.WriteLine("current inventory...\n\n");

            //foreach(IBagItems item in stuff)
            //{
            //    Console.WriteLine(item.ToString());
            //}

            //subtotal test
            //Console.WriteLine("about to show subtotal... press key");
            //Console.ReadLine();
            Console.WriteLine(InventoryManager.GetSubtotaledInventory(player.Inventory));
            Console.WriteLine("\n9) Go back");
            Console.WriteLine($"\nCurrent HP: {player.CurrentHP}/{player.StartingHP}");
            Console.WriteLine($"Status: {player.PlayerCondition}");
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
            Console.WriteLine($"{player.Name}, you have won all the fights!\n\n");
            Console.WriteLine("You are now headed to the bakery.\n\n");
            Console.WriteLine("(press any key to continue)");
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

    }
}
