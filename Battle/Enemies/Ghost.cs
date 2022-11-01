namespace Battle.Enemies
{
    internal class Ghost : IMonster
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public int StartingHP { get; set; }
        public int CurrentHP { get; set; }
        public HealthBar monsterHealthBar { get; set; }

        const int SPECIAL_CHANCE_PERCENTAGE = 15;
        Random rng = new Random();
        
        public Ghost(string randomName)
        {
            Type = "Spooky";
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
                Special(player);
                actionText = "The ghost stole 5g! Not cool.\n\t\t";
            }

            player.TakeDmg(dmgAmount);
            actionText += "You took " + dmgAmount + " damage!";

            ScreenManager.BattleScreenUpdate(this, player, actionText, 2);
            Console.ReadKey();
            ScreenManager.BattleScreenUpdate(this, player, actionText, 1);
        }

        public void Special(Player player)
        {
            //GHOST STEALS GOLD
            if (player.gold > 5)
                player.gold -= 5;
            else
                player.gold = 0;
        }

        public void TakeDmg(int dmgTaken)
        {
            CurrentHP -= dmgTaken;
            monsterHealthBar.BarConsoleUpdate(StartingHP, CurrentHP);
        }
    


    }
}
