using AthameRPG.Contracts.Unused;
using AthameRPG.Enums.Unused;

namespace AthameRPG.Objects.Characters.WarUnits.Unused.UndeadHordes
{
    public class Initiate : NightWalker
    {
        private const int InitiateInitiative = 40;
        private const int InitiateHealth = 45;
        private const int InitiateChanceToHit = 65;
        private const int InitiateDamage = 15;
        private const Target InitiateTargetType = Target.Anyone;
        private const AttackSource InitiateAttackSource = AttackSource.Death;
        private const AttackSource InitiateImmunity = AttackSource.None;
        private const AttackType InitiateAttackType = AttackType.Pestilence;
        private const string InitiateName = "Initiate";

        protected Initiate()
        {
            this.Name = InitiateName;
            this.Health = InitiateHealth;
            this.Initiative = InitiateInitiative;
            this.ChanceToHit = InitiateChanceToHit;
            this.Damage = InitiateDamage;
            this.TargetType = InitiateTargetType;
            this.AttackSource = InitiateAttackSource;
            this.AttackType = InitiateAttackType;
            this.Immunity = InitiateImmunity;
        }

        public override void AttackCreature(IUnit enemyUnit)
        {
            base.AttackCreature(enemyUnit);

            var damage = this.Damage - enemyUnit.Armor > 0 ? this.Damage - enemyUnit.Armor : 0;
            enemyUnit.Health -= damage;
        }
    }
}
