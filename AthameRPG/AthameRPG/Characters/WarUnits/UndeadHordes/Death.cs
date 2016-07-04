namespace DisciplesRpgGame.Units.The_Undead_Hordes
{
    using Enums;
    using Interfaces;

    public class Death : NightWalker
    {
        private const int DeathInitiative = 60;
        private const int DeathLordHealth = 125;
        private const int DeathChanceToHit = 80;
        private const int DeathDamage = 100;
        private const Target DeathTargetType = Target.Anyone;
        private const AttackSource DeathAttackSource = AttackSource.Death;
        private const AttackSource DeathImmunity = AttackSource.Weapon;
        private const AttackType DeathAttackType = AttackType.DeathTouch;
        private const string DeathName = "Death";

        protected Death()
        {
            this.Name = DeathName;
            this.Health = DeathLordHealth;
            this.Initiative = DeathInitiative;
            this.ChanceToHit = DeathChanceToHit;
            this.Damage = DeathDamage;
            this.TargetType = DeathTargetType;
            this.AttackSource = DeathAttackSource;
            this.AttackType = DeathAttackType;
            this.Immunity = DeathImmunity;
        }

        public override void AttackCreature(IUnit enemyUnit)
        {
            base.AttackCreature(enemyUnit);
            var damage = this.Damage - enemyUnit.Armor > 0 ? this.Damage - enemyUnit.Armor : 0;
            enemyUnit.Health -= damage;
        }
    }
}
