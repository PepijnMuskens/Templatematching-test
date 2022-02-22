using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loadout_tracker
{
    public class Game
    {
        public SLoadout[] SLoadouts { get; set; }
        public KLoadout KLoadout { get; set; }
        public Game()
        {
            SLoadouts = new SLoadout[4];
        }
    }

    public class KLoadout
    {
        public int[] Perks { get; set; }
        public int Power { get; set; }
        public int[] Addons { get; set; }

        public KLoadout()
        {
            Perks = new int[4];
            Addons = new int[4];
        }
    }

    public class SLoadout
    {
        public int[] Perks { get; set; }
        public int Item { get; set; }
        public int[] Addons { get; set; }

        public SLoadout()
        {
            Perks = new int[4];
            Addons = new int[4];
        }
    }
}
