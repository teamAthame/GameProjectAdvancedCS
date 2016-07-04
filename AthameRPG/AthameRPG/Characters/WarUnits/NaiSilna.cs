using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthameRPG.Characters.WarUnits
{
    public class NaiSilna : WarUnit
    {
        // това трябва да го има във всяка гадина  !!!
        private const int DefaultStrengthLevel = 7;
        private const int DefaultHealth = 400;

        public NaiSilna():base()
        {
            this.strengthLevel = DefaultStrengthLevel;
        }
        public NaiSilna(bool playerUnit) : base(playerUnit)
        {
            this.strengthLevel = DefaultStrengthLevel;
        }

        public override int GetDefaultHeаlth()
        {
            throw new NotImplementedException();
        }

        protected override float GetDefaultMove()
        {
            throw new NotImplementedException();
        }
    }
}
