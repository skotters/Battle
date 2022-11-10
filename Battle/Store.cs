using Battle.Items;

namespace Battle
{
    public static class Store
    {
        const int HEALTHPOTIONCOST = 20;
        const int MAGICPOTIONCOST = 20;
        const int ANTIDOTECOST = 10;
        const int SWORDCOST = 30;
        const int ARMORCOST = 40;

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
                            if (player.gold - HEALTHPOTIONCOST >= 0)
                            {
                                player.Inventory.Add(new HealthPotion());
                                player.gold -= HEALTHPOTIONCOST;
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
                            if (player.gold - MAGICPOTIONCOST >= 0)
                            {
                                player.Inventory.Add(new MagicPotion());
                                player.gold -= MAGICPOTIONCOST;
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
                            if (player.gold - ANTIDOTECOST >= 0)
                            {
                                player.Inventory.Add(new Antidote());
                                player.gold -= ANTIDOTECOST;
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
                                if (player.gold - SWORDCOST >= 0)
                                {
                                    player.hasSword = true;
                                    player.MinAttackDmg++;      //min goes up 1
                                    player.MaxAttackDmg += 2;   //max goes up 2
                                    player.gold -= SWORDCOST;
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
                                if (player.gold - ARMORCOST >= 0)
                                {
                                    player.hasArmor = true;
                                    player.gold -= ARMORCOST;
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
    }
}

