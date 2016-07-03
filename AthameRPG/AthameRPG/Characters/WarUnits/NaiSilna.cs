using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthameRPG.Characters.WarUnits
{
    public class NaiSilna : WarUnit
    {
        private const int DefaultStrengthLevel = 7;

        public NaiSilna():base()
        {
            this.strengthLevel = DefaultStrengthLevel;
        }
        public NaiSilna(bool playerUnit) : base(playerUnit)
        {
            this.strengthLevel = DefaultStrengthLevel;
        }
    }
}
