using System;
using AthameRPG.Contracts.Unused;
using AthameRPG.Enums.Unused;

namespace AthameRPG.Objects.Characters.WarUnits.Unused.UndeadHordes
{
    public abstract class NightWalker : IUnit
    {
        private const int MaxArmor = 99;
        private const int MaxInitiative = 100;
        private const int MaxChanceToHit = 100;
        private const int MaxDamage = 300;
        private const int UndeadArmor = 0;

        private int experience;
        private int health;
        private int armor;
        private int initiative;
        private int chanceToHit;
        private int damage;

        protected NightWalker()
        {
            this.IsAlive = true;
            this.Armor = UndeadArmor;
            this.Experience = 0;
        }

        public string Name { get; protected set; }

        public int Experience
        {
            get
            {
                return this.experience;
            }

            protected set
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
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Armor", "Armor must be a non-negative number");
                }

                if (value > MaxArmor)
                {
                    value = MaxArmor;
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
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Initiative", "Initiative must be a non-negative number");
                }

                if (value > MaxInitiative)
                {
                    value = MaxInitiative;
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

            protected set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("ChanceToHit", "ChanceToHit must be a non-negative number");
                }

                if (value > MaxChanceToHit)
                {
                    value = MaxChanceToHit;
                }

                this.chanceToHit = value;
            }
        }

        public Target TargetType { get; protected set; }

        public AttackSource AttackSource { get; protected set; }

        public AttackType AttackType { get; protected set; }

        public AttackSource Immunity { get; protected set; }

        public bool IsAlive { get; protected set; }

        public int Damage
        {
            get
            {
                return this.damage;
            }

            protected set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Damage must be non negative number,");
                }

                if (value > MaxDamage)
                {
                    value = MaxDamage;
                }

                this.damage = value;
            }
        }

        public virtual void AttackCreature(IUnit enemyUnit)
        {
            if (!this.IsAlive || !enemyUnit.IsAlive || enemyUnit.Immunity.Equals(this.AttackSource))
            {
                return;
            }

            var randGenerator = new Random();
            var randValue = randGenerator.Next(0, MaxChanceToHit);
            if (randValue > this.ChanceToHit)
            {
                return;
            } 
        }

        public override string ToString()
        {
            return string.Format(this.Name);
        }          
    }
}
