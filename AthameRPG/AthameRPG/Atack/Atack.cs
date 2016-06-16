using AthameRPG.Characters;
using AthameRPG.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthameRPG.Atack
{
    public abstract class Atack : IAtack
    {
        protected Atack(Unit unit)
        {
            this.Unit = unit;
        }

        public Unit Unit { get; set; }

        public abstract int Damage { get; set; }
    }
}
