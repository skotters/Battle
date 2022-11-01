using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Battle.Items;

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

            string enemy = File.ReadAllText(monster.Type + ".txt");

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
            Console.WriteLine($"Current HP: {player.CurrentHP}/{player.StartingHP}");
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

    }
}
