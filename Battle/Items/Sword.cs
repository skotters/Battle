using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle.Items
{
    public class Sword : IBagItems
    {
        public string Name { get; set; }
        public static int Cost { get; set; } = 30;
        public Sword()
        {
            Name = "Sword";
        }
        public override bool Equals(object obj)
        {
            Sword other = obj as Sword;
            return other != null && other.Name == this.Name;
        }
        public override int GetHashCode()
        {
            return 0;
        }
        public override string ToString()
        {
            return "Sword";
        }

        public void UseItem(Player player)
        {
            
        }


    }
}
