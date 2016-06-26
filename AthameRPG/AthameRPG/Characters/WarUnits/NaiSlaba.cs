using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthameRPG.Characters.WarUnits
{
    public class NaiSlaba : WarUnit
    {
        private const int DefaultStrengthLevel = 1;

        public NaiSlaba()
        {
            this.strengthLevel = DefaultStrengthLevel;
        }
    }
}
