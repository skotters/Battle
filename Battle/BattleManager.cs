﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Battle.Enemies;

namespace Battle
{
    public class BattleManager
    {
        public void BattleSetup(Player player)
        {
            MonsterNames.LoadFullJSON();

            bool fighting = true;
            
            while(fighting)
            {
                //round1
                //EnterBattle(player, new HouseCat(MonsterNames.GetMonsterName()));
                EnterBattle(player, new Bat(MonsterNames.GetMonsterName()));

                //round2
                if (!AskForNextFight(player)) { break; }
                player.CurrentHP = player.StartingHP;
                EnterBattle(player, new Ghost(MonsterNames.GetMonsterName()));

                //round3
                AskForNextFight(player);
                player.CurrentHP = player.StartingHP;
                EnterBattle(player, new HouseCat(MonsterNames.GetMonsterName()));

                //round4
                AskForNextFight(player);
                player.CurrentHP = player.StartingHP;
                EnterBattle(player, new Spider(MonsterNames.GetMonsterName()));

                //round5
                AskForNextFight(player);
                player.CurrentHP = player.StartingHP;
                EnterBattle(player, new Ufo(MonsterNames.GetMonsterName()));

                ScreenManager.VictoryScreen(player);

                fighting = false;
            }

            Console.WriteLine("entering the bakery");
        }

        public void EnterBattle(Player player, IMonster monster)
        {
            ////if monster name starts with V, gets 20 extra health.
            //if (monster.Name[0].ToString().ToLower().Equals("v"))
            //{
            //    monster.StartingHP = 120;
            //    monster.CurrentHP = 120;
            //}

            int whoseturn = 1;
            int playerOption;
            bool play = true;
            Random rng = new Random();
            string battleStatusText = "You encountered a " + monster.Type;

            ScreenManager.BattleScreenUpdate(monster, player, battleStatusText, whoseturn);
            
            while (play)
            {
                if (whoseturn == 1) //player turn
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
                                break;
                            }
                        case 2: //strong attack
                            {
                                bool successfulHit = rng.Next(1, 101) >= 50; //50% chance to hit. 
                                int tempMinAttackDmg = player.MinAttackDmg + 5;
                                int tempMaxAttackDmg = player.MaxAttackDmg + 10;
                                //bool successfulHit = false; //testing only
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
                                break;
                            }
                        case 3: //magic attack
                            {
                                Console.WriteLine("magic attack pending...");
                                Console.ReadLine();
                                break;
                            }
                        case 4:
                            {
                                InventoryManager.InventoryMenu(player);
                                Console.ReadLine(); //temp hold
                                break;
                            }
                        case 5: // 50% chance to get away and go to bakery
                            {
                                Console.WriteLine("run away!");
                                break;
                            }
                        default:
                            break;
                    }
                    
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
                monster.Name + " was defeated!\n" +
                "\t\tYou gain 50 gold.";

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

        public bool IsSpecialVMonster(string name)
        {
            

            return true;
        }
    }
}