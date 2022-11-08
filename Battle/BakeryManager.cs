using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Battle
{
    public static class BakeryManager
    {
        public static void OpenBakery(Player player)
        {
            bool bakeryOpen = true;
            int playerOption; //keyboard entry

            do
            {
                try
                {
                    ScreenManager.BakeryScreen(player);
                    var keyPress = Console.ReadKey();
                    playerOption = int.Parse(keyPress.KeyChar.ToString());

                    switch (playerOption)
                    {
                        case 1: //water
                            {
                                ScreenManager.WaterPurchasedScreen();
                                bakeryOpen = false;
                                break;
                            }
                        case 2: //glazed donut
                            {
                                if (player.gold >= 100)
                                {
                                    ScreenManager.GlazedDonutPurchasedScreen();
                                    bakeryOpen = false;
                                }
                                else
                                {
                                    Console.WriteLine("\n\nNot enough gold...");
                                    Console.WriteLine("(ok)");
                                    Console.ReadKey();

                                }
                                break;
                            }
                        case 3: //maple bacon donut
                            {
                                if (player.gold >= 150)
                                {
                                    ScreenManager.MapleBaconDonutPurchasedScreen();
                                    bakeryOpen = false;
                                }
                                else
                                {
                                    Console.WriteLine("\n\nNot enough gold...");
                                    Console.WriteLine("(ok)");
                                    Console.ReadKey();
                                }
                                break;
                            }
                        default: //bad entry
                            {
                                Console.WriteLine("\n\tInvalid entry.    (ok)");
                                Console.ReadKey();
                                ScreenManager.BakeryScreen(player);
                                break;
                            }
                    }
                }
                catch
                {
                    Console.WriteLine("\n\tInvalid entry.    (ok)");
                    Console.ReadKey();
                    ScreenManager.BakeryScreen(player);
                    
                }
            } while (bakeryOpen);


            
        }
    }
}
