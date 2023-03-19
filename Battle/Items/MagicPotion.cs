using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle.Items
{
    public class MagicPotion : IBagItems
    {
        public string Name { get; set; }
        public static int Cost { get; set; } = 20;
        public MagicPotion()
        {
            Name = "MagicPotion";
        }
        public override bool Equals(object obj)
        {
            MagicPotion other = obj as MagicPotion;
            return other != null && other.Name == this.Name;
        }

        public override int GetHashCode()
        {
            return 0;
        }
        public override string ToString()
        {
            return "Magic Potion";
        }

        public void UseItem(Player player)
        {
            if (player.StartingMP - player.CurrentMP >= 10)
                player.CurrentMP += 10;
            else
                player.CurrentMP = 20;
        }
    }
}
