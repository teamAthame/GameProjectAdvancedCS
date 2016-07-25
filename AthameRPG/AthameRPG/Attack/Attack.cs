using AthameRPG.Contracts;
using AthameRPG.Objects.Characters.Heroes;

namespace AthameRPG.Attack
{
    public abstract class Attack : IAttack
    {
        protected Attack(Unit unit)
        {
            this.Unit = unit;
        }

        public Unit Unit { get; set; }

        public abstract int Damage { get; set; }
    }
}