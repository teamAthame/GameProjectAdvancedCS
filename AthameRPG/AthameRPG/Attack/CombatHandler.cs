using AthameRPG.Contracts;
using AthameRPG.Contracts.Unused;
using AthameRPG.Objects.Characters.Heroes;

namespace AthameRPG.Attack
{
    public abstract class CombatHandler : ICombatHandler
    {

        protected CombatHandler(Unit unit)
        {
            this.Unit = unit;
        }

        public Unit Unit { get; set; }

        public abstract IAttack GenerateAtack();
    }
}