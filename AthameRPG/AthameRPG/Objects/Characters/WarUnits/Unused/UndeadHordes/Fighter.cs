using AthameRPG.Contracts;
using AthameRPG.Enums.Unused;

namespace AthameRPG.Characters.WarUnits.Unused.UndeadHordes
{
    public class Fighter : NightWalker
    {
        private const int FighterInitiative = 50;
        private const int FighterHealth = 120;
        private const int FighterChanceToHit = 80;
        private const int FighterDamage = 25;
        private const Target FighterTargetType = Target.Adjacent;
        private const AttackSource FighterAttackSource = AttackSource.Weapon;
        private const AttackSource FighterImmunity = AttackSource.None;
        private const AttackType FighterAttackType = AttackType.Sword;
        private const string FighterName = "Fighter";

        protected Fighter()
        {
            this.Name = FighterName;
            this.Health = FighterHealth;
            this.Initiative = FighterInitiative;
            this.ChanceToHit = FighterChanceToHit;
            this.Damage = FighterDamage;
            this.TargetType = FighterTargetType;
            this.AttackSource = FighterAttackSource;
            this.AttackType = FighterAttackType;
            this.Immunity = FighterImmunity;
        }

        public override void AttackCreature(IUnit enemyUnit)
        {
            base.AttackCreature(enemyUnit);
            var damage = this.Damage - enemyUnit.Armor > 0 ? this.Damage - enemyUnit.Armor : 0;
            enemyUnit.Health -= damage;
        }
    }
}
