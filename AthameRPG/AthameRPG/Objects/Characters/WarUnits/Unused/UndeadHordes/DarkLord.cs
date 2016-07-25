using AthameRPG.Contracts;
using AthameRPG.Enums.Unused;

namespace AthameRPG.Characters.WarUnits.Unused.UndeadHordes
{
    public class DarkLord : NightWalker
    {
        private const int DarkLordInitiative = 50;
        private const int DarkLordHealth = 200;
        private const int DarkLordChanceToHit = 80;
        private const int DarkLordDamage = 75;
        private const Target DarkLordTargetType = Target.Adjacent;
        private const AttackSource DarkLordAttackSource = AttackSource.Weapon;
        private const AttackSource DarkLordImmunity = AttackSource.None;
        private const AttackType DarkLordAttackType = AttackType.UndeadBlade;
        private const string DarkLordName = "DarkLord";

        protected DarkLord()
        {
            this.Name = DarkLordName;
            this.Health = DarkLordHealth;
            this.Initiative = DarkLordInitiative;
            this.ChanceToHit = DarkLordChanceToHit;
            this.Damage = DarkLordDamage;
            this.TargetType = DarkLordTargetType;
            this.AttackSource = DarkLordAttackSource;
            this.AttackType = DarkLordAttackType;
            this.Immunity = DarkLordImmunity;
        }

        public override void AttackCreature(IUnit enemyUnit)
        {
            base.AttackCreature(enemyUnit);
            var damage = this.Damage - enemyUnit.Armor > 0 ? this.Damage - enemyUnit.Armor : 0;
            enemyUnit.Health -= damage;
        }
    }
}
