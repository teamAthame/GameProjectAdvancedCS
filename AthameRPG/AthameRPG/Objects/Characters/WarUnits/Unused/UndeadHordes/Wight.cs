using AthameRPG.Contracts.Unused;
using AthameRPG.Enums.Unused;

namespace AthameRPG.Objects.Characters.WarUnits.Unused.UndeadHordes
{
    public class Wight : NightWalker
    {
        private const int WightInitiative = 50;
        private const int WightHealth = 105;
        private const int WightChanceToHit = 80;
        private const int WightDamage = 75;
        private const Target WightTargetType = Target.Anyone;
        private const AttackSource WightAttackSource = AttackSource.Weapon;
        private const AttackSource WightImmunity = AttackSource.Death;
        private const AttackType WightAttackType = AttackType.DeathTouch;
        private const string WightName = "Wight";

        protected Wight()
        {
            this.Name = WightName;
            this.Health = WightHealth;
            this.Initiative = WightInitiative;
            this.ChanceToHit = WightChanceToHit;
            this.Damage = WightDamage;
            this.TargetType = WightTargetType;
            this.AttackSource = WightAttackSource;
            this.AttackType = WightAttackType;
            this.Immunity = WightImmunity;
        }

        public override void AttackCreature(IUnit enemyUnit)
        {
            base.AttackCreature(enemyUnit);

            var damage = this.Damage - enemyUnit.Armor > 0 ? this.Damage - enemyUnit.Armor : 0;
            enemyUnit.Health -= damage;
        }
    }
}
