using AthameRPG.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AthameRPG.Characters;

namespace AthameRPG.Atack
{
    public abstract class CombatHandler : ICombatHandler
    {

        protected CombatHandler(Unit unit)
        {
            this.Unit = unit;
        }

        public Unit Unit { get; set; }

        public abstract IAtack GenerateAtack();
    }
}
