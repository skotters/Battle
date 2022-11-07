using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Battle
{
    public static class MagicManager
    {
        public static List<int> dmgAmtHolder = new List<int>();
        
        public enum SpellType
        {
            Fireball,
            ArcaneMissiles,
            Heal
        }

        public static bool MagicSelection(IMonster monster, Player player)
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
                                Fireball(monster, player);
                                castedASpell = true;
                                break;
                            }
                        case 2: //arcane missile
                            {
                                ArcaneMissiles(monster, player);
                                castedASpell = true;
                                break;
                            }
                        case 3: //heal self
                            {
                                HealSelf(monster, player);
                                castedASpell = true;
                                break;
                            }
                        case 4: //go back
                            {
                                return false; //go back, not casting anything
                                break;
                            }
                        default: //bad entry
                            break;
                    }
                }
                catch
                {

                }
            }
            while (!castedASpell);

            return true; //something was casted, player turn used up
        }
        public static void Fireball(IMonster monster, Player player)
        {
            Random rng = new Random();
            dmgAmtHolder.Add(rng.Next(15, 21));
            player.MagicAttack(monster, dmgAmtHolder, SpellType.Fireball);

        }

        public static void ArcaneMissiles(IMonster monster, Player player)
        {
            Random rng = new Random();
            for (int i = 0; i < 3; i++)
            {
                if (rng.Next(1, 100) <= 50)
                    dmgAmtHolder.Add(rng.Next(12, 19));

                    
            }
            player.MagicAttack(monster, dmgAmtHolder, SpellType.ArcaneMissiles);

        }

        public static void HealSelf(IMonster monster, Player player)
        {
            player.HealHP(monster, player);
            
        }
    }
}
