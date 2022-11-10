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
            bool playerIsAlive;

            while(fighting)
            {
                //round1
                playerIsAlive = EnterBattle(player, new Bat(MonsterNames.GetMonsterName()));
                if (goToBakery) { break; }
                if (!playerIsAlive) { break; }
                
                //round2
                if (!AskForNextFight(player)) { break; }
                EnterBattle(player, new Ghost(MonsterNames.GetMonsterName()));
                if (goToBakery) { break; }
                if (!playerIsAlive) { break; }

                //round3
                if (!AskForNextFight(player)) { break; }
                EnterBattle(player, new HouseCat(MonsterNames.GetMonsterName()));
                if (goToBakery) { break; }
                if (!playerIsAlive) { break; }

                //round4
                if (!AskForNextFight(player)) { break; }
                EnterBattle(player, new Spider(MonsterNames.GetMonsterName()));
                if (goToBakery) { break; }
                if (!playerIsAlive) { break; }

                //round5
                if (!AskForNextFight(player)) { break; }
                EnterBattle(player, new Ufo(MonsterNames.GetMonsterName()));
                if (goToBakery) { break; }
                if (!playerIsAlive) { break; }

                ScreenManager.VictoryScreen(player);

                fighting = false;
            }

        }//fully exiting battle stage

        public bool EnterBattle(Player player, IMonster monster)
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
                    // poison persists until player uses an
                    // antidote or the fight ends.
                    CheckForPoisonDamage(monster, player);

                    bool badUserEntry = false;
                    menuExitPerformed = false;

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

                                        // if magic WAS performed, then the
                                        // player turn is used up and menuExitPerformed is false.
                                        if (magicWasPerformed)
                                            menuExitPerformed = false;
                                        else
                                        {
                                            //player continues current turn
                                            ScreenManager.BattleScreenUpdate(monster, player, String.Empty, 1);
                                            menuExitPerformed = true; 
                                        }

                                        break;
                                    }
                                case 4: //open inventory
                                    {
                                        InventoryManager.InventoryMenu(player);
                                        ScreenManager.BattleScreenUpdate(monster, player, String.Empty, 1);

                                        //Player can always use inventory items and 
                                        //still remain in current turn, therefore inventory
                                        //menu exit is always true.
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
                                            Console.WriteLine(" (press any key) ");
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
                                default: //no number match
                                    Console.WriteLine("\tInvalid entry.    (press any key)");
                                    Console.ReadKey();
                                    ScreenManager.BattleScreenUpdate(monster, player, String.Empty, 1);
                                    badUserEntry = true;
                                    break;
                            }
                        }
                        catch //non-number catch
                        {
                            Console.WriteLine("\tInvalid entry.    (press any key)");
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
                    monster.Attack(player, rng.Next(monster.MinAttackDmg, monster.MaxAttackDmg));
                    if (player.CurrentHP <= 0) 
                    {
                        return false;
                        //ScreenManager.DeathScreen(monster, player);
                    }
                    
                    whoseturn = 1;
                }
            }

            return true; //player survived the fight.

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
            bool badUserEntry;

            do
            {
                ScreenManager.AfterFightScreen(player);
                badUserEntry = false;

                try
                {
                    var keyPress = Console.ReadKey();
                    playerOption = int.Parse(keyPress.KeyChar.ToString());

                    switch (playerOption)
                    {
                        case 1: //keep fighting
                            return true;
                        case 2: //go to bakery
                            return false;
                        default:
                            {
                                Console.WriteLine("\n\tInvalid entry.    (press any key)");
                                Console.ReadKey();
                                ScreenManager.AfterFightScreen(player);
                                badUserEntry = true;
                                break;
                            }
                    }
                }
                catch
                {
                    Console.WriteLine("\n\tInvalid entry.    (press any key)");
                    Console.ReadKey();
                    ScreenManager.AfterFightScreen(player);
                    badUserEntry = true;
                }
            } while (badUserEntry);
            
            return true; //default yes to next fight.
        }

        public void CheckForPoisonDamage(IMonster monster, Player player)
        {
            int poisonDmgToPlayer = 5;
            string battleStatusText = $"You took {poisonDmgToPlayer} damage from the poison.";

            if (player.PlayerCondition == Condition.Poisoned)
            {
                player.CurrentHP -= poisonDmgToPlayer;
                ScreenManager.BattleScreenUpdate(monster, player, battleStatusText, 1);
            }
        }
        public bool IsSpecialVMonster(string name)
        {
            return true;
        }
    }
}
