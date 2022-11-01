using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battle.Enemies;

namespace Battle
{
    public class GameManager
    {
        public void StartGame()
        {
            string startingName = "";
            string visitStore = "";

            Console.Clear();
            Console.WriteLine
                (
                    "\n\nYour hunger seems to be insatiable.\n" + 
                    "It occurs to you that you will not be accompanied alone\n" + 
                    "on this adventure since your belly does have a mind of its own."
                );

            Console.WriteLine("What is your name, traveller?\n");
            Console.Write("--> ");

            startingName = Console.ReadLine();

            Player p1 = new Player();

            p1.Name = startingName;

            Console.Clear();
            Console.WriteLine
                (
                    $"\n{startingName}, before you go, would you like to visit the store?\n" +
                    "This will be your only opportunity to buy things to help in your battles."
                );

            Console.Write("\n(y/n): ");
            visitStore = Console.ReadLine();

            if (visitStore.ToLower() == "y")
            {
                Console.WriteLine("youre heading to the store...");
                Store.GoShopping(p1);
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"You are bold, {p1.Name}.");
                Console.WriteLine("May your pockets be lined with enough gold for the bakery!");
            }

            Console.Clear();
            //Console.WriteLine("entering monster creation stage...");

            BattleManager battle = new BattleManager();
            battle.BattleSetup(p1);

                        


            Console.WriteLine("hold...");
            Console.ReadLine();


            

            Console.WriteLine("game over.");
        }

    }
}
