﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle.Items
{
    internal class MagicPotion : IBagItems
    {
        public string Name { get; set; }
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
            // Constant because equals tests mutable member.
            // This will give poor hash performance, but will prevent bugs.
            return 0;
        }
        public override string ToString()
        {
            return "Magic Potion";
        }

        public void UseItem(Player player)
        {
            if (player.StartingMP - player.CurrentMP >= 5)
                player.CurrentMP += 5;
            else
                player.CurrentMP = 20;
        }
    }
}