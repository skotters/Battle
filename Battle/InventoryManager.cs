using Battle.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
            bool handInBag = true;
            int playerOption;

            while (handInBag) //you're digging around for something...
            {
                //ScreenManager.ShowInventoryScreen(player);
                OpenWindow(player.Inventory, player); //             what is this line..........

                //var keyPress = Console.ReadKey();
                //playerOption = int.Parse(keyPress.KeyChar.ToString());

                //switch (playerOption)
                //{
                    
                //}
            }
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
                    subtotalInventory[item]++;              //increments the int value (quantity of item)
                else
                    subtotalInventory.Add(item, 1);         //new addition of item
            }

            foreach (var nameAndQty in subtotalInventory)
            {
                //subtotalDisplayed += nameAndQty.Key + " " + nameAndQty.Value + "\n";
                subtotalDisplayed += ($"{listNumber}) {nameAndQty.Key} | {nameAndQty.Value}\n");
                listNumber++;
            }

            subtotalOptionCounter = listNumber; //set the property counter needed?
            return subtotalDisplayed;
        }

        //static public void OpenWindow(Dictionary<IBagItems, int> subtotalList, List<IBagItems> stuff, Player player)
        static public void OpenWindow(List<IBagItems> stuff, Player player)
        {
            //only used for index number accessing of dictionary names.
            List<string> nameHolder = new List<string>();

            int i = 0;

            Console.WriteLine("__________________");


            bool go = true;

            while (go)
            {
                ScreenManager.ShowInventoryScreen(player);
                nameHolder = new List<string>(); //reset

                foreach (var item in subtotalInventory)
                {
                    //Console.Write((i + 1) + ") ");
                    //Console.WriteLine(item.Key + "\t|\t" + item.Value);
                    nameHolder.Add(item.Key.ToString());
                    i++;
                }

                Console.Write("\nEnter number: ");
                int option = Convert.ToInt32(Console.ReadLine()) - 1;

                Console.WriteLine("You chose list option # " + option);

                UseStuff(nameHolder[option], stuff, player);
            }
        }

        static public void UseStuff(string itemKey, List<IBagItems> stuff, Player player)
        //static public void UseStuff(IComponent thing, Dictionary<string, int> fullList)
        {

            Console.WriteLine("initial list before removal... count: " + stuff.Count);
            // *********** initial full equip list **********
            foreach (var item in stuff)
            {
                Console.WriteLine(item.Name);
            }
            //***********************************************

            Console.WriteLine("\n\nlooking to use/remote an item from list...");
            foreach (IBagItems item in stuff)
            {
                //remove item from list if found...
                if (itemKey == item.ToString())
                {
                    Console.WriteLine($"removing {item.ToString()}");
                    item.UseItem(player);
                    stuff.Remove(item);
                    break;
                }
            }

            Console.WriteLine("\n\ninitial list after removal... count: " + stuff.Count);
            // *********** initial full equip list **********
            foreach (var item in stuff)
            {
                Console.WriteLine(item.Name);
            }
        }



    }
}

