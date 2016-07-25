using AthameRPG.Contracts;
using AthameRPG.Enums.Unused;

namespace AthameRPG.Characters.WarUnits.Unused.UndeadHordes
{
    public class Wraith : NightWalker
    {
        private const int WraithInitiative = 60;
        private const int WraithHealth = 75;
        private const int WraithChanceToHit = 80;
        private const int WraithDamage = 60;
        private const Target WraithTargetType = Target.Anyone;
        private const AttackSource WraithAttackSource = AttackSource.Weapon;
        private const AttackSource WraithImmunity = AttackSource.Death;
        private const AttackType WraithAttackType = AttackType.Pestilence;
        private const string WraithName = "Wraith";

        protected Wraith()
        {
            this.Name = WraithName;
            this.Health = WraithHealth;
            this.Initiative = WraithInitiative;
            this.ChanceToHit = WraithChanceToHit;
            this.Damage = WraithDamage;
            this.TargetType = WraithTargetType;
            this.AttackSource = WraithAttackSource;
            this.AttackType = WraithAttackType;
            this.Immunity = WraithImmunity;
        }

        public override void AttackCreature(IUnit enemyUnit)
        {
            base.AttackCreature(enemyUnit);

            var damage = this.Damage - enemyUnit.Armor > 0 ? this.Damage - enemyUnit.Armor : 0;
            enemyUnit.Health -= damage;
        }
    }
}
