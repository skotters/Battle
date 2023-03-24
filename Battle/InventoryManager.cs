using Battle.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Battle
{
    public static class InventoryManager
    {
        public static Dictionary<IBagItems, int> subtotalInventory = new Dictionary<IBagItems, int>();
        public static string subtotalDisplayed { get; set; }
        public static int subtotalOptionCounter { get; set; }
        public static bool InventoryMenu(Player player)
        {
            OpenWindow(player.Inventory, player); 

            return true;
        }
        public static string GetSubtotaledInventory(List<IBagItems> stuff)
        {
             // *** resets on each call***
            subtotalInventory = new Dictionary<IBagItems, int>(); 
            int listNumber = 1;
            subtotalDisplayed = "";
             // **************************

            foreach (IBagItems item in stuff)
            {
                if (subtotalInventory.ContainsKey(item))
                    subtotalInventory[item]++;              //increments the int dict value (quantity of item)
                else
                    subtotalInventory.Add(item, 1);         //new addition of item to dict
            }

            foreach (var nameAndQty in subtotalInventory)
            {
                subtotalDisplayed += ($"{listNumber}) {nameAndQty.Key} | {nameAndQty.Value}\n");
                listNumber++;
            }

            subtotalOptionCounter = listNumber; 
            return subtotalDisplayed;
        }
        static public void OpenWindow(List<IBagItems> stuff, Player player)
        {
            //only used for index number accessing of dictionary names.
            List<string> nameHolder = new List<string>();

            int i = 0;

            Console.WriteLine("__________________");

            while (true) //stay here until player enters option 9 to exit menu
            {
                try
                {
                        ScreenManager.ShowInventoryScreen(player, stuff.Count);
                        nameHolder = new List<string>(); //reset

                        foreach (var item in subtotalInventory)
                        {
                            nameHolder.Add(item.Key.ToString());
                            i++;
                        }

                    Console.Write("\nEnter number: ");
                    int option = Convert.ToInt32(Console.ReadLine()) - 1;

                    //option works with zero based list, need to re-increment for a break
                    if (option + 1 == 9)
                    {
                        break;
                    }

                    UseStuff(nameHolder[option], stuff, player);
                }
                catch(Exception ex)
                {
                    ErrorLogger.UserInputError(MethodBase.GetCurrentMethod().Name, ex.Message);
                }
            }
        }
        static public void UseStuff(string itemKey, List<IBagItems> stuff, Player player)
        {
            foreach (IBagItems item in stuff)
            {
                //remove item from list if found...
                if (itemKey == item.ToString())
                {
                    if (item.Name == "Armor" || item.Name == "Sword")
                    {
                        Console.WriteLine("\tPassive item, cannot be consumed.    (press any key)");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine($"removing {item.ToString()}");
                        item.UseItem(player);
                        stuff.Remove(item);
                    }
                    break;
                }
            }
        }
    }
}

