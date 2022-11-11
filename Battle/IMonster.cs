using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle
{
    public interface IMonster
    {
        public string Name { get; set; }
        public int StartingHP { get; set; }
        public int CurrentHP { get; set; }
        public int MinAttackDmg { get; set; }
        public int MaxAttackDmg { get; set; }
        public string Type { get; set; }
        public VisualMeter MonsterHealthBar { get; set; }
        public void Attack(Player player, int dmgAmount);
        public void TakeDmg(int dmgTaken); 

        //generic Special() method possible?
    }
}
