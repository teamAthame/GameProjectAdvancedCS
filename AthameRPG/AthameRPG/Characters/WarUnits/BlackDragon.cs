using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthameRPG.Characters.WarUnits
{
    public class BlackDragon : WarUnit
    {
        private const int DefaultStrengthLevel = 7;
        private const string DefaultImagePath = "../Content/Character/blackDragon";

        public BlackDragon()
        {
            this.strengthLevel = DefaultStrengthLevel;
            this.imagePath = DefaultImagePath;
        }
    }
}
