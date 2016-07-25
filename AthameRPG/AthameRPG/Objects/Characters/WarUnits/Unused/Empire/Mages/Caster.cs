using AthameRPG.Enums.Unused;

namespace AthameRPG.Characters.WarUnits.Unused.Empire.Mages
{
    public abstract class Caster : AttackUnit
    {
        private const int CasterExperience = 0;
        private const int CasterInitiative = 40;
        private const int CasterArmor = 0;
        private const int CasterChanceToHit = 80;
        private const AttackSource CasterAttackSource = AttackSource.Air;
        private const Target CasterTarget = Target.All;

        protected Caster(int health, int damage) 
            : base(CasterExperience, health, CasterArmor, CasterInitiative, 
                  CasterChanceToHit, CasterTarget, CasterAttackSource, damage)
        {
        }
    }
}