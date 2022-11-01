using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle.Items
{
    internal class HealthPotion : IBagItems
    {
        public string Name { get; set; }
        public HealthPotion()
        {
            Name = "HealthPotion";
        }
        public override bool Equals(object obj)
        {
            HealthPotion other = obj as HealthPotion;
            return other != null && other.Name == this.Name;
        }

        public override int GetHashCode()
        {
            // Constant because equals tests mutable member.
            // This will give poor hash performance, but will prevent bugs.
            return 0;
        }
        public override string ToString()
        {
            return "Health Potion";
        }

        public void UseItem(Player player)
        {
            if (player.StartingHP - player.CurrentHP >= 25)
                player.CurrentHP += 25;
            else
                player.CurrentHP = 100;

        }
    }
}
