using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Battle.Enemies
{
    internal class Spider : Monster 
    {
        const int SPECIAL_CHANCE_PERCENTAGE = 20;
        Random rng = new Random();

        public Spider(string randomName)
        {
            Type = "Hobbit eater";
            Name = randomName;
            MinAttackDmg = 5;
            MaxAttackDmg = 8;

            if (randomName[0] == 'V') //monsters with V name start with more health.
                StartingHP = 120;
            else
                StartingHP = 100;

            CurrentHP = StartingHP;
            MonsterHealthBar = new VisualMeter();
        }
        public override void Attack(Player player, int dmgAmount)
        {
            string actionText = "";

            if (rng.Next(1, 101) <= SPECIAL_CHANCE_PERCENTAGE)
            {
                Special(player);
                actionText = "You've been poisoned!\n\t\t";
            }

            player.TakeDmg(dmgAmount);
            if (player.hasArmor && dmgAmount >= 2)
                actionText += $"{this.Name} hit you for {dmgAmount - 2} damage.";
            else if (player.hasArmor)
                actionText += $"{this.Name} hit you for 0 damage.";
            else
                actionText += $"{this.Name} hit you for {dmgAmount} damage.";

            ScreenManager.BattleScreenUpdate(this, player, actionText, 2);
            Console.ReadKey();
            ScreenManager.BattleScreenUpdate(this, player, String.Empty, 1);
        }
        public void Special(Player player)
        {
            player.PlayerCondition = Condition.Poisoned;
        }
    }
}
