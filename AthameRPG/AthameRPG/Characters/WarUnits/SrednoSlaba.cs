using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthameRPG.Characters.WarUnits
{
    public class SrednoSlaba : WarUnit
    {
        // това трябва да го има във всяка гадина  !!!
        private const int DefaultStrengthLevel = 3;

        public SrednoSlaba()
        {
            this.strengthLevel = DefaultStrengthLevel;
        }

        protected override float GetDefaultMove()
        {
            throw new NotImplementedException();
        }
    }
}
