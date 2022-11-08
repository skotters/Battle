﻿namespace Battle.Enemies
{
    internal class Ghost : IMonster
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public int StartingHP { get; set; }
        public int CurrentHP { get; set; }
        public VisualMeter MonsterHealthBar { get; set; }

        const int SPECIAL_CHANCE_PERCENTAGE = 15;
        Random rng = new Random();
        
        public Ghost(string randomName)
        {
            Type = "Spooky";
            Name = randomName;
            if (randomName[0] == 'V') //monsters with V name start with more health.
                StartingHP = 120;
            else
                StartingHP = 100;

            CurrentHP = StartingHP;
            MonsterHealthBar = new VisualMeter();

        }
        public void Attack(Player player, int dmgAmount)
        {
            string actionText = "";

            if (rng.Next(1, 101) <= SPECIAL_CHANCE_PERCENTAGE) 
            {
                Special(player);
                actionText = "The ghost stole 5g! Not cool.\n\t\t";
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
            //GHOST STEALS GOLD FROM PLAYER
            if (player.gold > 5)
                player.gold -= 5;
            else
                player.gold = 0;
        }

        public void TakeDmg(int dmgTaken)
        {
            CurrentHP -= dmgTaken;
            VisualMeter.GetFullMeterString(StartingHP, CurrentHP);
        }
    


    }
}
