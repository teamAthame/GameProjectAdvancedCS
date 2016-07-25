using AthameRPG.Objects.Characters.Heroes;

namespace AthameRPG.Attack
{
    public class AttackBarbarian : Attack
    {
        private int damage;

        public AttackBarbarian(Unit unit)
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