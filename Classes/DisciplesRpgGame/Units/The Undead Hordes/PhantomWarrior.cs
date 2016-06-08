namespace DisciplesRpgGame.Units.The_Undead_Hordes
{
    using DisciplesRpgGame.Enums;
    using DisciplesRpgGame.Interfaces;

    public class PhantomWarrior : NightWalker
    {
        private const int PhantomWarriorInitiative = 50;
        private const int PhantomWarriorHealth = 320;
        private const int PhantomWarriorChanceToHit = 80;
        private const int PhantomWarriorDamage = 125;
        private const Target PhantomWarriorTargetType = Target.Adjacent;
        private const AttackSource PhantomWarriorAttackSource = AttackSource.Weapon;
        private const AttackSource PhantomWarriorImmunity = AttackSource.Death;
        private const AttackType PhantomWarriorAttackType = AttackType.UndeadBlade;
        private const string PhantomWarriorName = "PhantomWarrior";

        protected PhantomWarrior()
        {
            this.Name = PhantomWarriorName;
            this.Health = PhantomWarriorHealth;
            this.Initiative = PhantomWarriorInitiative;
            this.ChanceToHit = PhantomWarriorChanceToHit;
            this.Damage = PhantomWarriorDamage;
            this.TargetType = PhantomWarriorTargetType;
            this.AttackSource = PhantomWarriorAttackSource;
            this.AttackType = PhantomWarriorAttackType;
            this.Immunity = PhantomWarriorImmunity;
        }

        public override void AttackCreature(IUnit enemyUnit)
        {
            base.AttackCreature(enemyUnit);

            var damage = this.Damage - enemyUnit.Armor > 0 ? this.Damage - enemyUnit.Armor : 0;
            enemyUnit.Health -= damage;
        }
    }
}
