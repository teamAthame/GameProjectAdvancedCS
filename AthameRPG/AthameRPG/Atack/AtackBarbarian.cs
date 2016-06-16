using AthameRPG.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthameRPG.Atack
{
    public class AtackBarbarian : Atack
    {
        private int damage;

        public AtackBarbarian(Unit unit)
            :base(unit)
        {
            this.Unit = unit;
            this.Damage = damage;
        }

        public Unit Unit { get; set; }

        public override int Damage
        {
            get
            {
                return this.damage;
            }

            set
            {
                if (this.Unit.HealthPoints <= 80)
                {
                    this.damage += this.Unit.HealthPoints * 2;
                }
                else
                {
                    this.damage = this.Unit.HealthPoints;
                }

                this.damage = value;
            }
        }
    }
}
