using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle.Items
{
    internal class Antidote : IBagItems
    {
        public string Name { get; set; } 

        public Antidote()
        {
            Name = "Antidote";
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
