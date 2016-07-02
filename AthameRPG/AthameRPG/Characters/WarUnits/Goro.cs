using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthameRPG.Characters.WarUnits
{
    public class Goro : WarUnit
    {
        private const int DefaultStrengthLevel = 6;
        private const string DefaultImagePath = "../Content/Character/goro";

        public Goro()
        {
            this.strengthLevel = DefaultStrengthLevel;
            this.imagePath = DefaultImagePath;
        }
    }
}
