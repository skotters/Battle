using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle.Items
{
    public class Antidote : IBagItems
    {
        public string Name { get; set; }
        public static int Cost { get; set; } = 10;
        public bool isPassive { get; set; }

        public Antidote()
        {
            Name = "Antidote";
            isPassive = false;
        }
        public override bool Equals(object obj)
        {
            Antidote other = obj as Antidote;
            return other != null && other.Name == this.Name;
        }

        public override int GetHashCode()
        {
            return 0;
        }

        public override string ToString()
        {
            return "Antidote";
        }

        public void UseItem(Player player)
        {
            if (player.PlayerCondition == Condition.Poisoned)
                player.PlayerCondition = Condition.Normal;
        }
    }
}
