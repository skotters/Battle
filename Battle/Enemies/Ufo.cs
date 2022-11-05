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

        const int SPECIAL_CHANCE_PERCENTAGE = 30;
        Random rng = new Random();
        public Ufo(string randomName)
        {
            Type = "Area 51 escapee";
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
            if (rng.Next(1, 101) <= SPECIAL_CHANCE_PERCENTAGE && player.PlayerCondition != Condition.Confused)
            {
                Special(player);
                actionText = "The ufo made a weird sound and now you're confused!\n\t\t";
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
            ScreenManager.BattleScreenUpdate(this, player, actionText, 1);
        }

        public void Special(Player player)
        {
            player.PlayerCondition = Condition.Confused;
            player.ConfusionTurnCounter = 3;
        }

        public void TakeDmg(int dmgTaken)
        {
            CurrentHP -= dmgTaken;
            monsterHealthBar.BarConsoleUpdate(StartingHP, CurrentHP);
        }
    }
}
