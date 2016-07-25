using AthameRPG.Contracts;
using AthameRPG.Enums.Unused;

namespace AthameRPG.Characters.WarUnits.Unused.UndeadHordes
{
    public class Warlock : NightWalker
    {
        private const int WarlockInitiative = 40;
        private const int WarlockHealth = 75;
        private const int WarlockChanceToHit = 80;
        private const int WarlockDamage = 30;
        private const Target WarlockTargetType = Target.Anyone;
        private const AttackSource WarlockAttackSource = AttackSource.Weapon;
        private const AttackSource WarlockImmunity = AttackSource.None;
        private const AttackType WarlockAttackType = AttackType.Pestilence;
        private const string WarlockName = "Warlock";

        protected Warlock()
        {
            this.Name = WarlockName;
            this.Health = WarlockHealth;
            this.Initiative = WarlockInitiative;
            this.ChanceToHit = WarlockChanceToHit;
            this.Damage = WarlockDamage;
            this.TargetType = WarlockTargetType;
            this.AttackSource = WarlockAttackSource;
            this.AttackType = WarlockAttackType;
            this.Immunity = WarlockImmunity;
        }

        public override void AttackCreature(IUnit enemyUnit)
        {
            base.AttackCreature(enemyUnit);

            var damage = this.Damage - enemyUnit.Armor > 0 ? this.Damage - enemyUnit.Armor : 0;
            enemyUnit.Health -= damage;
        }
    }
}
