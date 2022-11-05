using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle
{  
    public enum Condition
    {
        Normal,
        Poisoned,
        Confused
    }
    public class Player
    {   
        public string Name { get; set; }
        public int StartingHP { get; set; }
        public int CurrentHP { get; set; }
        public int StartingMP { get; set; }
        public int CurrentMP { get; set; }
        public Condition PlayerCondition { get; set; }
        public HealthBar playerHealthBar { get; set; }
        public int gold { get; set; } = 50;
        public List<IBagItems> Inventory { get; set; }
        public bool hasSword { get; set; }
        public bool hasArmor { get; set; }
        public int MinAttackDmg { get; set; }
        public int MaxAttackDmg { get; set; }
        public int ConfusionTurnCounter { get; set; }
        
        Random rng = new Random();

        public Player()
        {
            StartingHP = 100;
            CurrentHP = StartingHP;
            StartingMP = 20;
            CurrentMP = StartingMP;
            playerHealthBar = new HealthBar();
            PlayerCondition = Condition.Normal;
            Inventory = new List<IBagItems>();
            hasSword = false;
            hasArmor = false;
            MinAttackDmg = 5;
            MaxAttackDmg = 10;
            ConfusionTurnCounter = 0;

        }
        public void Attack(IMonster monster, int dmgAmount)
        {
            if(ConfusionTurnCounter == 0) { this.PlayerCondition = Condition.Normal; }

            if (PlayerCondition == Condition.Confused)
            {                
                if (rng.Next(1, 101) <= 50) //ouch
                {
                    this.TakeDmg(dmgAmount);
                    string actionText = "In your confusion, you hit yourself for " +
                        dmgAmount + " damage!";                        
                    ScreenManager.BattleScreenUpdate(monster, this, actionText, 2);
                    Console.ReadKey();
                }
                else
                {
                    monster.TakeDmg(dmgAmount);
                    string actionText = $"{monster.Name} takes {dmgAmount} damage!";
                    ScreenManager.BattleScreenUpdate(monster, this, actionText, 2);
                    Console.ReadKey();
                }
                this.ConfusionTurnCounter--;
            }
            else
            {
                monster.TakeDmg(dmgAmount);
                string actionText = monster.Name + " takes " + dmgAmount + " damage!";
                ScreenManager.BattleScreenUpdate(monster, this, actionText, 2);
                Console.ReadKey();
            }
        }

        public void TakeDmg(int dmgTaken)
        {
            if (hasArmor)
                dmgTaken -= 2;
            if (dmgTaken < 0)   //dont take negative damage.
                dmgTaken = 0; 

            CurrentHP -= dmgTaken;
            playerHealthBar.BarConsoleUpdate(StartingHP, CurrentHP);
        }
    }
}
