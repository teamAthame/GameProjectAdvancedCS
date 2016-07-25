using AthameRPG.Contracts;
using AthameRPG.Enums.Unused;

namespace AthameRPG.Characters.WarUnits.Unused.UndeadHordes
{
    public class Archlich : NightWalker
    {
        private const int ArchlichInitiative = 40;
        private const int ArchlichHealth = 170;
        private const int ArchlichChanceToHit = 80;
        private const int ArchlichDamage = 90;
        private const Target ArchlichTargetType = Target.Anyone;
        private const AttackSource ArchlichAttackSource = AttackSource.Death;
        private const AttackSource ArchlichImmunity = AttackSource.Death;
        private const AttackType ArchlichAttackType = AttackType.Plague;
        private const string ArchlichName = "Archlich";

        protected Archlich()
        {
            this.Name = ArchlichName;
            this.Health = ArchlichHealth;
            this.Initiative = ArchlichInitiative;
            this.ChanceToHit = ArchlichChanceToHit;
            this.Damage = ArchlichDamage;
            this.TargetType = ArchlichTargetType;
            this.AttackSource = ArchlichAttackSource;
            this.AttackType = ArchlichAttackType;
            this.Immunity = ArchlichImmunity;
        }

        public override void AttackCreature(IUnit enemyUnit)
        {
            base.AttackCreature(enemyUnit);

            var damage = this.Damage - enemyUnit.Armor > 0 ? this.Damage - enemyUnit.Armor : 0;
            enemyUnit.Health -= damage;
        }
    }
}
