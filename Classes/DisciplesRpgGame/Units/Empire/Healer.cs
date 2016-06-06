using DisciplesRpgGame.Interfaces;
using System;
using DisciplesRpgGame.Enums;

namespace DisciplesRpgGame.Units.Empire
{
    public abstract class Healer : Unit, IHeal
    {
        private int healingPoints;

        protected Healer(int experience, int health, int armor, int initiative, Target targetType, int healingPoints)
            : base(experience, health, armor, initiative, targetType)
        {
            this.HealingPoints = healingPoints;
        }

        public int HealingPoints
        {
            get
            {
                return this.healingPoints;
            }

            private set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("HealingPoints", "HealingPoints cannot be a negative number,");
                }

                this.healingPoints = value;
            }
        }
    }
}