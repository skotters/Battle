using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle.Enemies
{
    internal class HouseCat : IMonster
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public int StartingHP { get; set; }
        public int CurrentHP { get; set; }
        public HealthBar monsterHealthBar { get; set; }
        const int SPECIAL_CHANCE_PERCENTAGE = 20;
        Random rng = new Random();
        public HouseCat(string randomName)
        {
            Type = "Grumpy";
            Name = randomName;
            if (randomName[0] == 'v') //monsters with V name start with more health.
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
                Special(player, dmgAmount);
                actionText = "There's a reason these things aren't man's best friend.\n\t\t" +
                             "It got angry and swiped twice with a hiss!\n\t\t";
                player.TakeDmg(dmgAmount);
                actionText += $"You took {dmgAmount}({dmgAmount / 2}x2) damage!";
            }
            else
            {
                player.TakeDmg(dmgAmount);
                actionText += "You took " + dmgAmount + " damage!";
            }

            ScreenManager.BattleScreenUpdate(this, player, actionText, 2);
            Console.ReadKey();
            ScreenManager.BattleScreenUpdate(this, player, actionText, 1);
        }

        public void Special(Player player, int damage)
        {
            //method even needed?
            //CAT GETS AN ADDITIONAL ATTACK
            player.TakeDmg(damage);
        }

        public void TakeDmg(int dmgTaken)
        {
            CurrentHP -= dmgTaken;
            monsterHealthBar.BarConsoleUpdate(StartingHP, CurrentHP);
        }
    }
}
