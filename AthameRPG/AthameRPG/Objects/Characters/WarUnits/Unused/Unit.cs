using System;
using AthameRPG.Enums.Unused;

namespace AthameRPG.Objects.Characters.WarUnits.Unused
{
    public abstract class Unit
    {
        private int experience;
        private int health;
        private int armor;
        private int initiative;
        private int chanceToHit;

        protected Unit(int experience, int health, int armor, int initiative, int chanceToHit, Target targetType, AttackSource attackSource)
        {
            this.Experience = experience;
            this.Health = health;
            this.Armor = armor;
            this.Initiative = initiative;
            this.ChanceToHit = chanceToHit;
            this.TargetType = targetType;
            this.AttackSource = attackSource;
            this.IsAlive = true;
        }

        public int Experience
        {
            get
            {
                return this.experience;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Experience", "Experience cannot be a negative number.");
                }

                this.experience = value;
            }
        }

        public int Health
        {
            get
            {
                return this.health;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Health", "Health cannot be a negative number.");
                }

                this.health = value;
            }
        }

        public int Armor
        {
            get
            {
                return this.armor;
            }

            set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentOutOfRangeException("Armor", "Armor must be a number in the range [0...100].");
                }

                this.armor = value;
            }
        }

        public int Initiative
        {
            get
            {
                return this.initiative;
            }

            set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentOutOfRangeException("Initiative", "Initiative must be a number in the range [0...100].");
                }

                this.initiative = value;
            }
        }

        public int ChanceToHit
        {
            get
            {
                return this.chanceToHit;
            }

            set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentOutOfRangeException("ChanceToHit", "ChanceToHit must be in the range [0...100] percent.");
                }

                this.chanceToHit = value;
            }
        }

        public Target TargetType { get; set; }

        public AttackSource AttackSource { get; set; }

        public bool IsAlive { get; set; }
    }
}