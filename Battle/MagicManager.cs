using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Battle
{
    public static class MagicManager
    {
        public static List<int> dmgAmtHolder = new List<int>();

        public static int FIREBALL_MP_COST = 6;
        public static int ARCANE_MP_COST = 10;
        public static int HEAL_MP_COST = 8;
        public enum SpellType
        {
            Fireball,
            ArcaneMissiles,
            Heal
        }

        public static bool MagicSelection(Monster monster, Player player)
        {
            ScreenManager.BattleScreenUpdate(monster, player, String.Empty, 1);
            dmgAmtHolder.Clear();   
            int playerOption; //keyboard entry
            bool castedASpell = false;

            do
            {
                try
                {
                    var keyPress = Console.ReadKey();
                    playerOption = int.Parse(keyPress.KeyChar.ToString());

                    switch (playerOption)
                    {
                        case 1: //fireball
                            {
                                if (player.CurrentMP >= FIREBALL_MP_COST)
                                {
                                    Fireball(monster, player);
                                    player.CurrentMP -= FIREBALL_MP_COST;
                                    castedASpell = true;
                                }
                                else
                                {
                                    string outOfMagicText = "Not enough MP to cast";
                                    ScreenManager.BattleScreenUpdate(monster, player, outOfMagicText, 1);
                                }
                                break;
                            }
                        case 2: //arcane missile
                            {
                                if (player.CurrentMP >= ARCANE_MP_COST)
                                {
                                    ArcaneMissiles(monster, player);
                                    player.CurrentMP -= ARCANE_MP_COST;
                                    castedASpell = true;
                                }
                                else
                                {
                                    string outOfMagicText = "Not enough MP to cast";
                                    ScreenManager.BattleScreenUpdate(monster, player, outOfMagicText, 1);
                                }
                                break;
                            }
                        case 3: //heal self
                            {
                                if (player.CurrentMP >= HEAL_MP_COST)
                                {
                                    HealSelf(monster, player);
                                    player.CurrentMP -= HEAL_MP_COST;
                                    castedASpell = true;
                                }
                                else
                                {
                                    string outOfMagicText = "Not enough MP to cast";
                                    ScreenManager.BattleScreenUpdate(monster, player, outOfMagicText, 1);
                                }
                                break;
                            }
                        case 4: //go back
                            {
                                return false; //go back, not casting anything
                                break;
                            }
                        default: //bad entry
                            {
                                ErrorLogger.UserInputError(MethodBase.GetCurrentMethod().Name, "No number input match");
                                ScreenManager.BattleScreenUpdate(monster, player, String.Empty, 1);
                                break;
                            }
                    }
                }
                catch(Exception ex)
                {
                    ErrorLogger.UserInputError(MethodBase.GetCurrentMethod().Name, ex.Message);
                    ScreenManager.BattleScreenUpdate(monster, player, String.Empty, 1);
                }
            }
            while (!castedASpell);

            return true; //something was casted, player turn used up
        }
        public static void Fireball(Monster monster, Player player)
        {
            Random rng = new Random();
            dmgAmtHolder.Add(rng.Next(15, 21));
            player.MagicAttack(monster, dmgAmtHolder, SpellType.Fireball);
        }

        public static void ArcaneMissiles(Monster monster, Player player)
        {
            Random rng = new Random();
            for (int i = 0; i < 3; i++)
            {
                if (rng.Next(1, 100) <= 50)
                    dmgAmtHolder.Add(rng.Next(12, 19));
            }
            player.MagicAttack(monster, dmgAmtHolder, SpellType.ArcaneMissiles);
        }

        public static void HealSelf(Monster monster, Player player)
        {
            player.HealHP(monster, player);
        }
    }
}
