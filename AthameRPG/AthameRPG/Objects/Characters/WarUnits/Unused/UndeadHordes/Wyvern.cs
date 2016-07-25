using AthameRPG.Contracts;
using AthameRPG.Enums.Unused;

namespace AthameRPG.Characters.WarUnits.Unused.UndeadHordes
{
    public class Wyvern : NightWalker
    {
        private const int WyvernInitiative = 35;
        private const int WyvernHealth = 225;
        private const int WyvernChanceToHit = 80;
        private const int WyvernDamage = 25;
        private const Target WyvernTargetType = Target.Anyone;
        private const AttackSource WyvernAttackSource = AttackSource.Death;
        private const AttackSource WyvernImmunity = AttackSource.None;
        private const AttackType WyvernAttackType = AttackType.Breath;
        private const string WyvernName = "Wyvern";

        protected Wyvern()
        {
            this.Name = WyvernName;
            this.Health = WyvernHealth;
            this.Initiative = WyvernInitiative;
            this.ChanceToHit = WyvernChanceToHit;
            this.Damage = WyvernDamage;
            this.TargetType = WyvernTargetType;
            this.AttackSource = WyvernAttackSource;
            this.AttackType = WyvernAttackType;
            this.Immunity = WyvernImmunity;
        }

        public override void AttackCreature(IUnit enemyUnit)
        {
            base.AttackCreature(enemyUnit);

            var damage = this.Damage - enemyUnit.Armor > 0 ? this.Damage - enemyUnit.Armor : 0;
            enemyUnit.Health -= damage;
        }
    }
}
