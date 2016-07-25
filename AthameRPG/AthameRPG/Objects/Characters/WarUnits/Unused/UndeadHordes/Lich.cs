using AthameRPG.Contracts;
using AthameRPG.Enums.Unused;

namespace AthameRPG.Characters.WarUnits.Unused.UndeadHordes
{
    public class Lich : NightWalker
    {
        private const int LichInitiative = 40;
        private const int LichHealth = 140;
        private const int LichChanceToHit = 80;
        private const int LichDamage = 70;
        private const Target LichTargetType = Target.Anyone;
        private const AttackSource LichAttackSource = AttackSource.Death;
        private const AttackSource LichImmunity = AttackSource.Death;
        private const AttackType LichAttackType = AttackType.Plague;
        private const string LichName = "Lich";

        protected Lich()
        {
            this.Name = LichName;
            this.Health = LichHealth;
            this.Initiative = LichInitiative;
            this.ChanceToHit = LichChanceToHit;
            this.Damage = LichDamage;
            this.TargetType = LichTargetType;
            this.AttackSource = LichAttackSource;
            this.AttackType = LichAttackType;
            this.Immunity = LichImmunity;
        }

        public override void AttackCreature(IUnit enemyUnit)
        {
            base.AttackCreature(enemyUnit);

            var damage = this.Damage - enemyUnit.Armor > 0 ? this.Damage - enemyUnit.Armor : 0;
            enemyUnit.Health -= damage;
        }
    }
}
