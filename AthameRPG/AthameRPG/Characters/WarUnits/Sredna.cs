using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthameRPG.Characters.WarUnits
{
    public class Sredna : WarUnit
    {
        private const int DefaultStrengthLevel = 4;

        public Sredna()
        {
            this.strengthLevel = DefaultStrengthLevel;
        }
    }
}
