using Battle.Items;
using System.Numerics;

namespace Battle
{
    public static class Store
    {
        //const int HEALTHPOTIONCOST = 20;
        //const int MAGICPOTIONCOST = 20;
        //const int ANTIDOTECOST = 10;
        //const int SWORDCOST = 30;
        //const int ARMORCOST = 40;

        public static void GoShopping(Player player)
        {
            ScreenManager.StoreFront(player.gold);
            Console.Write("Option: ");

            bool badUserEntry = false;
            bool openList = true;

            do
            {
                try
                {

                    switch (Convert.ToInt32(Console.ReadLine()))
                    {
                        case 1:
                            if (player.gold - HealthPotion.Cost >= 0)
                            {
                                BuyItem(player, new HealthPotion(), HealthPotion.Cost);
                                //player.Inventory.Add(new HealthPotion());
                                //player.gold -= Convert.ToInt32(ItemCosts.HealthPotion);
                                ScreenManager.StoreFront(player.gold);
                                Console.WriteLine("Added health potion\n");
                                Console.Write("Option: ");
                            }
                            else
                            {
                                ScreenManager.StoreFront(player.gold);
                                Console.WriteLine("Not enough gold.\n");
                                Console.Write("Option: ");
                            }
                            break;
                        case 2:
                            if (player.gold - MagicPotion.Cost >= 0)
                            {
                                BuyItem(player, new MagicPotion(), MagicPotion.Cost);
                                //player.Inventory.Add(new MagicPotion());
                                //player.gold -= Convert.ToInt32(ItemCosts.MagicPotion);
                                ScreenManager.StoreFront(player.gold);
                                Console.WriteLine("Added magic potion\n");
                                Console.Write("Option: ");
                            }
                            else
                            {
                                ScreenManager.StoreFront(player.gold);
                                Console.WriteLine("Not enough gold.\n");
                                Console.Write("Option: ");
                            }
                            break;
                        case 3:
                            if (player.gold - Antidote.Cost >= 0)
                            {
                                BuyItem(player, new Antidote(), Antidote.Cost);
                                //player.Inventory.Add(new Antidote());
                                //player.gold -= Convert.ToInt32(ItemCosts.Antidote);
                                ScreenManager.StoreFront(player.gold);
                                Console.WriteLine("Added antidote\n");
                                Console.Write("Option: ");
                            }
                            else
                            {
                                ScreenManager.StoreFront(player.gold);
                                Console.WriteLine("Not enough gold.\n");
                                Console.Write("Option: ");
                            }
                            break;
                        case 4:
                            if (player.hasSword)
                            {
                                ScreenManager.StoreFront(player.gold);
                                Console.WriteLine("Player already has sword.\n");
                                Console.Write("Option: ");
                            }
                            else
                            {
                                if (player.gold - Sword.Cost >= 0)
                                {
                                    player.hasSword = true;
                                    player.MinAttackDmg++;      //min goes up 1
                                    player.MaxAttackDmg += 2;   //max goes up 2
                                    BuyItem(player, new Sword(), Sword.Cost);
                                    //player.gold -= Convert.ToInt32(ItemCosts.Sword);
                                    ScreenManager.StoreFront(player.gold);
                                    Console.WriteLine("equipped sword\n");
                                    Console.Write("Option: ");
                                }
                                else
                                {
                                    ScreenManager.StoreFront(player.gold);
                                    Console.WriteLine("Not enough gold.\n");
                                    Console.Write("Option: ");
                                }
                            }
                            break;
                        case 5:
                            if (player.hasArmor)
                            {
                                ScreenManager.StoreFront(player.gold);
                                Console.WriteLine("Player already has armor.\n");
                                Console.Write("Option: ");
                            }
                            else
                            {
                                if (player.gold - Armor.Cost >= 0)
                                {
                                    player.hasArmor = true;
                                    BuyItem(player, new Armor(), Armor.Cost);
                                    //player.gold -= Convert.ToInt32(ItemCosts.Armor);
                                    ScreenManager.StoreFront(player.gold);
                                    Console.WriteLine("equipped armor.\n");
                                    Console.Write("Option: ");
                                }
                                else
                                {
                                    ScreenManager.StoreFront(player.gold);
                                    Console.WriteLine("Not enough gold.\n");
                                    Console.Write("Option: ");
                                }
                            }
                            break;
                        case 6:
                            openList = false;
                            break;
                        default:
                            Console.WriteLine("\tInvalid entry.    (ok)");
                            Console.ReadKey();
                            ScreenManager.StoreFront(player.gold);
                            Console.Write("Option: ");
                            badUserEntry = true;
                            break;
                    }

                }
                catch(Exception ex)
                {
                    //Console.WriteLine(ex); 
                    Console.WriteLine("\tInvalid entry.    (ok)");
                    Console.ReadKey();
                    ScreenManager.StoreFront(player.gold);
                    Console.Write("Option: ");
                    
                }
            } while (!badUserEntry && openList);
        }

        public static void BuyItem<T>(Player p1, T item, int itemCost) where T : IBagItems, new()
        {
            p1.Inventory.Add(item);
            p1.gold -= Convert.ToInt32(itemCost);
        }
    }
}

