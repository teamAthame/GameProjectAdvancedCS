using System;
using AthameRPG.Contracts;
using AthameRPG.Enums;

namespace AthameRPG.Characters.WarUnits
{
    public abstract class AttackUnit : Unit, IAttack
    {
        private int damage;

        protected AttackUnit(int experience, int health, int armor, int initiative, int chanceToHit, Target targetType, AttackSource attackSource, int damage) 
            : base(experience, health, armor, initiative, chanceToHit, targetType, attackSource)
        {
            this.Damage = damage;
        }

        public int Damage
        {
            get
            {
                return this.damage;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Damage", "Damage cannot be a negative number.");
                }

                this.damage = value;
            }
        }
    }
}