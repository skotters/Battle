using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle.Items
{
    internal class Sword
    {
        public string Name { get; set; }
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


    }
}
