using AthameRPG.Contracts;
using AthameRPG.Enums.Unused;

namespace AthameRPG.Characters.WarUnits.Unused.UndeadHordes
{
    public class SkeletonWarrior : NightWalker
    {
        private const int SkeletonWarriorInitiative = 50;
        private const int SkeletonWarriorHealth = 220;
        private const int SkeletonWarriorChanceToHit = 80;
        private const int SkeletonWarriorDamage = 75;
        private const Target SkeletonWarriorTargetType = Target.Adjacent;
        private const AttackSource SkeletonWarriorAttackSource = AttackSource.Weapon;
        private const AttackSource SkeletonWarriorImmunity = AttackSource.Death;
        private const AttackType SkeletonWarriorAttackType = AttackType.LongSword;
        private const string SkeletonWarriorName = "SkeletonWarrior";

        protected SkeletonWarrior()
        {
            this.Name = SkeletonWarriorName;
            this.Health = SkeletonWarriorHealth;
            this.Initiative = SkeletonWarriorInitiative;
            this.ChanceToHit = SkeletonWarriorChanceToHit;
            this.Damage = SkeletonWarriorDamage;
            this.TargetType = SkeletonWarriorTargetType;
            this.AttackSource = SkeletonWarriorAttackSource;
            this.AttackType = SkeletonWarriorAttackType;
            this.Immunity = SkeletonWarriorImmunity;
        }

        public override void AttackCreature(IUnit enemyUnit)
        {
            base.AttackCreature(enemyUnit);

            var damage = this.Damage - enemyUnit.Armor > 0 ? this.Damage - enemyUnit.Armor : 0;
            enemyUnit.Health -= damage;
        }
    }
}
