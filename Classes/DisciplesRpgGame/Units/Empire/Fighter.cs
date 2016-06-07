using System;
using DisciplesRpgGame.Enums;
using DisciplesRpgGame.Interfaces;

namespace DisciplesRpgGame.Units.Empire
{
    public abstract class Fighter : Unit, IAttack
    {
        private const int FighterChanceToHit = 80;
        private const Target FighterTarget = Target.Adjacent;
        private const AttackSource FigtherAttackSource = AttackSource.Weapon;

        private int damage;

        protected Fighter(int experience, int health, int armor, int initiative, int damage) 
            : base(experience, health, armor, initiative, FighterChanceToHit, FighterTarget, FigtherAttackSource)
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