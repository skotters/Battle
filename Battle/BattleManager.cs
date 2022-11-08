using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Battle.Enemies;

namespace Battle
{
    public class BattleManager
    {
        public bool goToBakery = false;
        public void BattleSetup(Player player)
        {
            MonsterNames.LoadFullJSON();

            bool fighting = true;

            while(fighting)
            {
                //round1
                EnterBattle(player, new Bat(MonsterNames.GetMonsterName()));
                if (goToBakery) { break; }
                
                //round2
                if (!AskForNextFight(player)) { break; }
                EnterBattle(player, new Ghost(MonsterNames.GetMonsterName()));
                if (goToBakery) { break; }

                //round3
                if (!AskForNextFight(player)) { break; }
                EnterBattle(player, new HouseCat(MonsterNames.GetMonsterName()));
                if (goToBakery) { break; }

                //round4
                if (!AskForNextFight(player)) { break; }
                EnterBattle(player, new Spider(MonsterNames.GetMonsterName()));
                if (goToBakery) { break; }

                //round5
                if (!AskForNextFight(player)) { break; }
                EnterBattle(player, new Ufo(MonsterNames.GetMonsterName()));
                if (goToBakery) { break; }

                ScreenManager.VictoryScreen(player);

                fighting = false;
            }

            Console.WriteLine("entering the bakery...");
            

        }

        public void EnterBattle(Player player, IMonster monster)
        {
            player.PlayerCondition = Condition.Normal;
            player.CurrentHP = player.StartingHP;

            int whoseturn = 1;   //player goes first
            int playerOption;    //keyboard entry
            bool play = true;
            bool menuExitPerformed = false;
            Random rng = new Random();
            string battleStatusText = "You encountered a " + monster.Type;

            ScreenManager.BattleScreenUpdate(monster, player, battleStatusText, whoseturn);
            
            while (play)
            {
                if (whoseturn == 1) //player turn
                {
                    bool badUserEntry = false;
                    Poisoncheck(monster, player);

                    do
                    {
                        badUserEntry = false; //reset on each new user entry attempt.
                        try
                        {
                            
                            var keyPress = Console.ReadKey();
                            playerOption = int.Parse(keyPress.KeyChar.ToString());

                            switch (playerOption)
                            {
                                case 1: //standard attack
                                    {
                                        player.Attack(monster, rng.Next(player.MinAttackDmg, player.MaxAttackDmg));

                                        if (monster.CurrentHP <= 0)
                                        {
                                            MonsterDefeated(monster, player, whoseturn);
                                            play = false;
                                        }
                                        menuExitPerformed = false;
                                        break;
                                    }
                                case 2: //strong attack
                                    {
                                        bool successfulHit = rng.Next(1, 101) >= 50; //50% chance to hit. 
                                        int tempMinAttackDmg = player.MinAttackDmg + 5;
                                        int tempMaxAttackDmg = player.MaxAttackDmg + 10;

                                        if (successfulHit)
                                            player.Attack(monster, rng.Next(tempMinAttackDmg, tempMaxAttackDmg));
                                        else
                                        {
                                            battleStatusText = "You missed!";
                                            ScreenManager.BattleScreenUpdate(monster, player, battleStatusText, 2);
                                            Console.ReadKey();
                                        }

                                        if (monster.CurrentHP <= 0)
                                        {
                                            MonsterDefeated(monster, player, whoseturn);
                                            play = false;
                                        }
                                        menuExitPerformed = false;
                                        break;
                                    }
                                case 3: //magic attack
                                    {
                                        bool magicWasPerformed = false;

                                        player.MagicMenuOpen = true;
                                        magicWasPerformed = MagicManager.MagicSelection(monster, player);
                                        player.MagicMenuOpen = false;
                                        

                                        if (monster.CurrentHP <= 0)
                                        {
                                            MonsterDefeated(monster, player, whoseturn);
                                            play = false;
                                        }

                                        // if magic WAS performed, then menuExitPerformed is false.
                                        if (magicWasPerformed)
                                            menuExitPerformed = false;
                                        else
                                        {
                                            ScreenManager.BattleScreenUpdate(monster, player, String.Empty, 1);
                                            menuExitPerformed = true;
                                        }

                                        break;
                                    }
                                case 4: //open inventory
                                    {
                                        InventoryManager.InventoryMenu(player);
                                        ScreenManager.BattleScreenUpdate(monster, player, String.Empty, 1);
                                        menuExitPerformed = true;
                                        break;
                                    }
                                case 5: // 15% chance to get away and go to bakery
                                    {
                                        bool successfulEscape = rng.Next(1, 101) <= 15; 
                                        string actionText;

                                        if (successfulEscape)
                                        {
                                            actionText = "You were able to run away!";
                                            ScreenManager.BattleScreenUpdate(monster, player, actionText, 1);
                                            Console.WriteLine(" (ok) ");
                                            Console.ReadLine();
                                            goToBakery = true;
                                            play = false;
                                        }
                                        else
                                        {
                                            actionText = "You failed to run away!";
                                            ScreenManager.BattleScreenUpdate(monster, player, actionText, 2);
                                            Console.ReadKey();
                                        }
                                        break;
                                    }
                                default: //bad number
                                    Console.WriteLine("\tInvalid entry.    (ok)");
                                    Console.ReadKey();
                                    ScreenManager.BattleScreenUpdate(monster, player, String.Empty, 1);
                                    badUserEntry = true;
                                    break;
                            }
                        }
                        catch //non-number catch
                        {
                            Console.WriteLine("\tInvalid entry.    (ok)");
                            Console.ReadKey();
                            ScreenManager.BattleScreenUpdate(monster, player, String.Empty, 1);
                            badUserEntry = true;
                        }
                    }
                    while (badUserEntry);

                    //if menu was exited from inventory or magic, keep player turn current and reiterate loop.
                    if (menuExitPerformed)
                        whoseturn = 1;
                    else
                        whoseturn = 2;
                }
                else //enemy turn
                {
                    monster.Attack(player, rng.Next(1, 7));
                    if (player.CurrentHP <= 0) 
                    { 
                        play = false;
                        Console.WriteLine("you dead.");
                    }
                    
                    whoseturn = 1;
                }
            }


        }   

        public void MonsterDefeated(IMonster monster, Player player, int whoseturn)
        {
            string defeatText = 
                $"{ monster.Name} was defeated!\n" +
                $"\t\tYou gain 50 gold.";

            player.gold += 50;
            ScreenManager.BattleScreenUpdate(monster, player, defeatText, whoseturn);
            Console.WriteLine("(Press any key to continue)");
            Console.ReadKey();
        }

        public bool AskForNextFight(Player player)
        {
            int playerOption;
            ScreenManager.AfterFightScreen(player);

            var keyPress = Console.ReadKey();
            playerOption = int.Parse(keyPress.KeyChar.ToString());

            switch (playerOption)
            {
                case 1: //keep fighting
                    {
                        return true;
                        break;
                    }
                case 2: //go to bakery
                    {
                        return false;
                        break;
                    }
                default:
                    {
                        return false;
                        break;
                    }
            }
        }

        public void Poisoncheck(IMonster monster, Player player)
        {
            int poisonDmg = 3;
            string battleStatusText = $"You took {poisonDmg} damage from the poison.";

            if (player.PlayerCondition == Condition.Poisoned)
            {
                player.CurrentHP -= poisonDmg;
                ScreenManager.BattleScreenUpdate(monster, player, battleStatusText, 1);
            }
        }
        public bool IsSpecialVMonster(string name)
        {
            return true;
        }
    }
}
