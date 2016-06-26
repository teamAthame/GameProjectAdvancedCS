using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthameRPG.Characters.WarUnits
{
    public abstract class WarUnit
    {
        protected int strengthLevel;

        public int GetStrengthLevel
        {
            get {return this.strengthLevel; }
        }
    }
}
