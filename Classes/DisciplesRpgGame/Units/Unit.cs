using DisciplesRpgGame.Enums;
using System;

namespace DisciplesRpgGame.Units
{
    public abstract class Unit : GameObject
    {
        private int experience;
        private int health;
        private int armor;
        private int initiative;

        protected Unit(int experience, int health, int armor, int initiative, Target targetType)
        {
            this.Experience = experience;
            this.Health = health;
            this.Armor = armor;
            this.Initiative = initiative;
            this.TargetType = targetType;
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

            protected set
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

            protected set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentOutOfRangeException("Initiative", "Initiative must be a number in the range [0...100].");
                }

                this.initiative = value;
            }
        }

        public Target TargetType { get; protected set; }
    }
}