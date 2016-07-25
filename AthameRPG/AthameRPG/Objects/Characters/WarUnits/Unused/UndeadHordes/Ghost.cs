using AthameRPG.Contracts;
using AthameRPG.Enums.Unused;

namespace AthameRPG.Characters.WarUnits.Unused.UndeadHordes
{
    public class Ghost : NightWalker
    {
        private const int GhostInitiative = 20;
        private const int GhostHealth = 45;
        private const int GhostChanceToHit = 65;
        private const int GhostDamage = 0;
        private const Target GhostTargetType = Target.Anyone;
        private const AttackSource GhostAttackSource = AttackSource.Mind;
        private const AttackSource GhostImmunity = AttackSource.Death;
        private const AttackType GhostAttackType = AttackType.Paralyze;
        private const string GhostName = "Ghost";

        protected Ghost()
        {
            this.Name = GhostName;
            this.Health = GhostHealth;
            this.Initiative = GhostInitiative;
            this.ChanceToHit = GhostChanceToHit;
            this.Damage = GhostDamage;
            this.TargetType = GhostTargetType;
            this.AttackSource = GhostAttackSource;
            this.AttackType = GhostAttackType;
            this.Immunity = GhostImmunity;
        }

        public override void AttackCreature(IUnit enemyUnit)
        {
            base.AttackCreature(enemyUnit);
            
            enemyUnit.Initiative = 0;
        }
    }
}
