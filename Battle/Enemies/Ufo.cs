using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle.Enemies
{
    internal class Ufo: IMonster
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public int StartingHP { get; set; }
        public int CurrentHP { get; set; }
        public HealthBar monsterHealthBar { get; set; }

        public Ufo(string randomName)
        {
            Type = "Area 51 escapee";
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
            player.TakeDmg(dmgAmount);
            string actionText = "You took " + dmgAmount + " damage!";
            ScreenManager.BattleScreenUpdate(this, player, actionText, 2);
            Console.ReadKey();
            ScreenManager.BattleScreenUpdate(this, player, actionText, 1);
        }

        public int Special()
        {
            throw new NotImplementedException();
        }

        public void TakeDmg(int dmgTaken)
        {
            CurrentHP -= dmgTaken;
            monsterHealthBar.BarConsoleUpdate(StartingHP, CurrentHP);
        }
    }
}
