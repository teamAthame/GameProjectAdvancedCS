using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthameRPG.Characters.WarUnits
{
    public class Slaba : WarUnit
    {
        // това трябва да го има във всяка гадина  !!!
        private const int DefaultStrengthLevel = 2;

        public Slaba()
        {
            this.strengthLevel = DefaultStrengthLevel;
        }

        protected override float GetDefaultMove()
        {
            throw new NotImplementedException();
        }
    }
}
