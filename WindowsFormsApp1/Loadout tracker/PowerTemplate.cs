using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interface;
namespace Loadout_tracker
{
    class PowerTemplate
    {
        int Id;
        byte[] Template;

        public PowerTemplate(PowerTemplateDTO powerTemplatedto)
        {
            Id = powerTemplatedto.Id;
            Template = powerTemplatedto.Template;
        }
    }
}
