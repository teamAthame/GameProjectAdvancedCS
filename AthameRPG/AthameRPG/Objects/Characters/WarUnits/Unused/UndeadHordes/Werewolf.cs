using AthameRPG.Contracts.Unused;
using AthameRPG.Enums.Unused;

namespace AthameRPG.Objects.Characters.WarUnits.Unused.UndeadHordes
{
    public class Werewolf : NightWalker
    {
        private const int WerewolfInitiative = 50;
        private const int WerewolfHealth = 100;
        private const int WerewolfChanceToHit = 80;
        private const int WerewolfDamage = 40;
        private const Target WerewolfTargetType = Target.Adjacent;
        private const AttackSource WerewolfAttackSource = AttackSource.Weapon;
        private const AttackSource WerewolfImmunity = AttackSource.Weapon;
        private const AttackType WerewolfAttackType = AttackType.Slash;
        private const string WerewolfName = "Wereworf";

        protected Werewolf()
        {
            this.Name = WerewolfName;
            this.Health = WerewolfHealth;
            this.Initiative = WerewolfInitiative;
            this.ChanceToHit = WerewolfChanceToHit;
            this.Damage = WerewolfDamage;
            this.TargetType = WerewolfTargetType;
            this.AttackSource = WerewolfAttackSource;
            this.AttackType = WerewolfAttackType;
            this.Immunity = WerewolfImmunity;
        }

        public override void AttackCreature(IUnit enemyUnit)
        {
            base.AttackCreature(enemyUnit);

            var damage = this.Damage - enemyUnit.Armor > 0 ? this.Damage - enemyUnit.Armor : 0;
            enemyUnit.Health -= damage;
        }
    }
}
