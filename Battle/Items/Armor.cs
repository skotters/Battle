using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle.Items
{
    internal class Armor //damage mitigated by 2.
    {
        public string Name { get; set; }

        public Armor()
        {
            Name = "Armor";
        }
        public override bool Equals(object obj)
        {
            Armor other = obj as Armor;
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
            return "Armor";
        }


    }
}
