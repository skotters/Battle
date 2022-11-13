using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
        public VisualMeter PlayerHealthBar { get; set; }
        public VisualMeter PlayerMPBar { get; set; }
        public int gold { get; set; } = 50;
        public List<IBagItems> Inventory { get; set; }
        public bool hasSword { get; set; }
        public bool hasArmor { get; set; }
        public int MinAttackDmg { get; set; }
        public int MaxAttackDmg { get; set; }
        public int ConfusionTurnCounter { get; set; }
        public bool MagicMenuOpen { get; set; }
        
        Random rng = new Random();

        public Player()
        {
            StartingHP = 100;
            CurrentHP = StartingHP;
            StartingMP = 20;
            CurrentMP = StartingMP;
            PlayerHealthBar = new VisualMeter();
            PlayerMPBar = new VisualMeter();      
            PlayerCondition = Condition.Normal;
            Inventory = new List<IBagItems>();
            hasSword = false;
            hasArmor = false;
            MinAttackDmg = 5;
            MaxAttackDmg = 8;
            //MinAttackDmg = 50;      
            //MaxAttackDmg = 80;      
            ConfusionTurnCounter = 0;
            MagicMenuOpen = false;
        }
        public void Attack(Monster monster, int dmgAmount)
        {
            if(ConfusionTurnCounter == 0) { this.PlayerCondition = Condition.Normal; }

            if (PlayerCondition == Condition.Confused)
            {                
                if (rng.Next(1, 101) <= 50) //50% chance player hits self.
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
                ScreenManager.BattleScreenUpdate(monster, this, String.Empty, 2);
            }
        }
        public void MagicAttack(Monster monster, List<int> dmgAmount, MagicManager.SpellType spellType)
        {
            //only two offensive magic attacks in place, only need a single if/else

            string actionText;

            if (spellType == MagicManager.SpellType.Fireball)
            {
                monster.TakeDmg(dmgAmount[0]);
                actionText = $"{monster.Name} takes {dmgAmount[0]} fireball damage!";
                ScreenManager.BattleScreenUpdate(monster, this, actionText, 2);
                Console.ReadKey();
            }
            else //arcane missiles...
            {
                int arcaneDmgTotal = 0;
                foreach(var item in dmgAmount)
                {
                    arcaneDmgTotal += item;
                }
                monster.TakeDmg(arcaneDmgTotal);
                actionText = $"{monster.Name} was hit {dmgAmount.Count} times by\n" +
                    $"\tarcane missles for {arcaneDmgTotal} damage!";
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
            VisualMeter.GetFullMeterString(StartingHP, CurrentHP);
        }

        public void HealHP(Monster monster, Player player)
        {
            if (StartingHP - CurrentHP >= 20)
                CurrentHP += 20;
            else
                CurrentHP = StartingHP;

            VisualMeter.GetFullMeterString(StartingHP, CurrentHP);
            string actionText = $"You healed yourself for 20 HP";
            ScreenManager.BattleScreenUpdate(monster, this, actionText, 2);
            Console.ReadKey();
        }
    }
}
