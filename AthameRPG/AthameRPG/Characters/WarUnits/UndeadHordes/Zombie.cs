using AthameRPG.Contracts;
using AthameRPG.Enums;

namespace DisciplesRpgGame.Units.The_Undead_Hordes
{
    public class Zombie : NightWalker
    {
        private const int ZombieInitiative = 50;
        private const int ZombieHealth = 170;
        private const int ZombieChanceToHit = 80;
        private const int ZombieDamage = 50;
        private const Target ZombieTargetType = Target.Adjacent;
        private const AttackSource ZombieAttackSource = AttackSource.Weapon;
        private const AttackSource ZombieImmunity = AttackSource.Death;
        private const AttackType ZombieAttackType = AttackType.Slash;
        private const string ZombieName = "Zombie";

        protected Zombie()
        {
            this.Name = ZombieName;
            this.Health = ZombieHealth;
            this.Initiative = ZombieInitiative;
            this.ChanceToHit = ZombieChanceToHit;
            this.Damage = ZombieDamage;
            this.TargetType = ZombieTargetType;
            this.AttackSource = ZombieAttackSource;
            this.AttackType = ZombieAttackType;
            this.Immunity = ZombieImmunity;
        }

        public override void AttackCreature(IUnit enemyUnit)
        {
            base.AttackCreature(enemyUnit);

            var damage = this.Damage - enemyUnit.Armor > 0 ? this.Damage - enemyUnit.Armor : 0;
            enemyUnit.Health -= damage;
        }
    }
}
