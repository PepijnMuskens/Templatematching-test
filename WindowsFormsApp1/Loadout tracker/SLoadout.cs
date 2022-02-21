using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loadout_tracker
{
    public class SLoadout
    {
        public Item Item { get; private set; }
        public Addon[] Addons { get; private set; }
        public Perk[] Perks { get; private set; }

        public SLoadout()
        {
            Addons = new Addon[2];
            Perks = new Perk[4];
        }


        
    }
}
