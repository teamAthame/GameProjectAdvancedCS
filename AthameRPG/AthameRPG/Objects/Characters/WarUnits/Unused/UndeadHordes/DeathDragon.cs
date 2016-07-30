using AthameRPG.Contracts.Unused;
using AthameRPG.Enums.Unused;

namespace AthameRPG.Objects.Characters.WarUnits.Unused.UndeadHordes
{
    public class DeathDragon : NightWalker
    {
        private const int DeathDragonInitiative = 35;
        private const int DeathDragonLordHealth = 375;
        private const int DeathDragonChanceToHit = 80;
        private const int DeathDragonDamage = 55;
        private const Target DeathDragonTargetType = Target.Anyone;
        private const AttackSource DeathDragonAttackSource = AttackSource.Death;
        private const AttackSource DeathDragonImmunity = AttackSource.Death;
        private const AttackType DeathDragonAttackType = AttackType.Breath;
        private const string DeathDragonName = "DeathDragon";

        protected DeathDragon()
        {
            this.Name = DeathDragonName;
            this.Health = DeathDragonLordHealth;
            this.Initiative = DeathDragonInitiative;
            this.ChanceToHit = DeathDragonChanceToHit;
            this.Damage = DeathDragonDamage;
            this.TargetType = DeathDragonTargetType;
            this.AttackSource = DeathDragonAttackSource;
            this.AttackType = DeathDragonAttackType;
            this.Immunity = DeathDragonImmunity;
        }

        public override void AttackCreature(IUnit enemyUnit)
        {
            base.AttackCreature(enemyUnit);
            var damage = this.Damage - enemyUnit.Armor > 0 ? this.Damage - enemyUnit.Armor : 0;
            enemyUnit.Health -= damage;
        }
    }
}
