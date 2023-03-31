using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle.Items
{
    public class Armor : IBagItems//damage mitigated by 2.
    {
        public string Name { get; set; }
        public static int Cost { get; set; } = 40;
        public bool isPassive { get; set; }
        public Armor()
        {
            Name = "Armor";
            isPassive = true;
        }
        public override bool Equals(object obj)
        {
            Armor other = obj as Armor;
            return other != null && other.Name == this.Name;
        }

        public override int GetHashCode()
        {
            return 0;
        }
        public override string ToString()
        {
            return "Armor";
        }

        public void UseItem(Player player)
        {
            
        }


    }
}
