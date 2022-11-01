using Battle.Items;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle
{
    public static class Store
    {
        public static void GoShopping(Player player)
        {
            bool openList = true;

            Console.Clear();
            Console.WriteLine("Welcome to the store!");
            Console.WriteLine("What would you like to purchase?\n");

            Console.WriteLine("\tGold: " + player.gold);

            Console.WriteLine("\nName\t\t\tDesc\t\t\tCost");
            Console.WriteLine("-------------------------------------------------------");
            Console.WriteLine
                (
                    "1) Health Potion\theals 25hp\t\t20g" +
                    "\n2) Magic Potion\t\trestores 25mp\t\t20g" +
                    "\n3) Antidote\t\tcures poison\t\t10g" +
                    "\n4) Sword\t\t+2 dmg melee\t\t30g" +
                    "\n5) Armor\t\t-2 dmg taken\t\t40g" +
                    "\n\n6) (Leave)"
                );

            while (openList)
            {
                switch(Convert.ToInt32(Console.ReadLine()))
                {
                    case 1:
                        player.Inventory.Add(new HealthPotion());
                        Console.WriteLine("Added health potion");
                        break;
                    case 2:
                        player.Inventory.Add(new MagicPotion());
                        Console.WriteLine("Added magic potion");
                        break;
                    case 3:
                        player.Inventory.Add(new Antidote());
                        Console.WriteLine("Added antidote");
                        break;
                    case 4:
                        if (player.hasSword)
                            Console.WriteLine("Player already has sword.");    
                        else
                        {
                            player.hasSword = true;
                            player.MinAttackDmg++;      //min goes up 1
                            player.MaxAttackDmg += 2;   //max goes up 2
                            Console.WriteLine("equipped sword");
                        }
                        break;
                    case 5:
                        if (player.hasArmor)
                            Console.WriteLine("Player already has armor.");
                        else
                        {
                            player.hasArmor = true;
                            Console.WriteLine("equipped armor.");
                        }
                        break;
                    case 6:
                        openList = false;
                        break;
                    default:
                        Console.WriteLine("something broke");
                        break;
                }
            }
        }
    }
}
