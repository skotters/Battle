using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle
{
    public interface IBagItems
    {
        public string Name { get; set; }
        public static int Cost { get; set; }

        public void UseItem(Player player);
    }
}
