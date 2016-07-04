using AthameRPG.Characters.Heroes;
using AthameRPG.Contracts;

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