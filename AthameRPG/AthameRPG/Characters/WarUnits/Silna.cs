using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthameRPG.Characters.WarUnits
{
    public class Silna : WarUnit
    {
        private const int DefaultStrengthLevel = 6;

        public Silna()
        {
            this.strengthLevel = DefaultStrengthLevel;
        }
    }
}
