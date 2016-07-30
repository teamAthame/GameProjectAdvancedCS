using AthameRPG.Contracts.Unused;
using AthameRPG.Enums.Unused;

namespace AthameRPG.Objects.Characters.WarUnits.Unused.UndeadHordes
{
    public class Dreadwyrm : NightWalker
    {
        private const int DreadwyrmInitiative = 35;
        private const int DreadwyrmLordHealth = 450;
        private const int DreadwyrmChanceToHit = 80;
        private const int DreadwyrmDamage = 65;
        private const Target DreadwyrmTargetType = Target.Anyone;
        private const AttackSource DreadwyrmAttackSource = AttackSource.Death;
        private const AttackSource DreadwyrmImmunity = AttackSource.Death;
        private const AttackType DreadwyrmAttackType = AttackType.PlagueBreath;
        private const string DreadwyrmName = "Dreadwyrm";

        protected Dreadwyrm()
        {
            this.Name = DreadwyrmName;
            this.Health = DreadwyrmLordHealth;
            this.Initiative = DreadwyrmInitiative;
            this.ChanceToHit = DreadwyrmChanceToHit;
            this.Damage = DreadwyrmDamage;
            this.TargetType = DreadwyrmTargetType;
            this.AttackSource = DreadwyrmAttackSource;
            this.AttackType = DreadwyrmAttackType;
            this.Immunity = DreadwyrmImmunity;
        }

        public override void AttackCreature(IUnit enemyUnit)
        {
            base.AttackCreature(enemyUnit);
            var damage = this.Damage - enemyUnit.Armor > 0 ? this.Damage - enemyUnit.Armor : 0;
            enemyUnit.Health -= damage;
        }
    }
}
