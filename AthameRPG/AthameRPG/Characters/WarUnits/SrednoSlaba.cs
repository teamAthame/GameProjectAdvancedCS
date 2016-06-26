using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthameRPG.Characters.WarUnits
{
    public class SrednoSlaba : WarUnit
    {
        private const int DefaultStrengthLevel = 3;

        public SrednoSlaba()
        {
            this.strengthLevel = DefaultStrengthLevel;
        }
    }
}
