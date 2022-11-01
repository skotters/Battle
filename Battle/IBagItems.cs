using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle
{
    //shouldn't have named this plural..... should have been IBagItem. Too scared to change.
    public interface IBagItems
    {
        public string Name { get; set; }
        public void UseItem(Player player);
    }
}
