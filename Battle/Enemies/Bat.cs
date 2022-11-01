using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Battle.Enemies
{
    internal class Bat : IMonster
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public int StartingHP { get; set; }
        public int CurrentHP { get; set; }
        public HealthBar monsterHealthBar { get; set; }

        const int SPECIAL_CHANCE_PERCENTAGE = 20;
        Random rng = new Random();
        
        public Bat(string randomName)
        {
            Type = "Vampire";
            Name = randomName;

            if (randomName[0] == 'V') //monsters with V name start with more health.
                StartingHP = 120;
            else
                StartingHP = 100;

            CurrentHP = StartingHP;
            monsterHealthBar = new HealthBar();

        }
        public void Attack(Player player, int dmgAmount)
        {
            string actionText = "";

            //if(true)
            if (rng.Next(1, 101) <= SPECIAL_CHANCE_PERCENTAGE) 
            {
                Special(dmgAmount);
                actionText = "The bat used a leech attack and healed itself!\n\t\t";
            }
        
            player.TakeDmg(dmgAmount);
            actionText += "You took " + dmgAmount + " damage!";
            
            ScreenManager.BattleScreenUpdate(this, player, actionText, 2);
            Console.ReadKey();
            ScreenManager.BattleScreenUpdate(this, player, actionText, 1);
        }

        public void Special(int bloodAmount)
        {
            //BAT LEECH ATTACK
            if (this.StartingHP - this.CurrentHP >= bloodAmount)
                this.CurrentHP += bloodAmount;
            else
                CurrentHP = StartingHP; // go max
        }

        public void TakeDmg(int dmgTaken)
        {
            CurrentHP -= dmgTaken;
            monsterHealthBar.BarConsoleUpdate(StartingHP, CurrentHP);
        }
    }
}
