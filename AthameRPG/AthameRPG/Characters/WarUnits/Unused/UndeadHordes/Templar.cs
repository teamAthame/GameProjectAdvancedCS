using AthameRPG.Contracts;
using AthameRPG.Enums;

namespace DisciplesRpgGame.Units.The_Undead_Hordes
{
    public class Templar : NightWalker
    {
        private const int TemplarInitiative = 50;
        private const int TemplarHealth = 160;
        private const int TemplarhanceToHit = 80;
        private const int TemplarDamage = 50;
        private const Target TemplarTargetType = Target.Adjacent;
        private const AttackSource TemplarAttackSource = AttackSource.Weapon;
        private const AttackSource TemplarImmunity = AttackSource.None;
        private const AttackType TemplarAttackType = AttackType.Lance;
        private const string TemplarName = "Templar";

        protected Templar()
        {
            this.Name = TemplarName;
            this.Health = TemplarHealth;
            this.Initiative = TemplarInitiative;
            this.ChanceToHit = TemplarhanceToHit;
            this.Damage = TemplarDamage;
            this.TargetType = TemplarTargetType;
            this.AttackSource = TemplarAttackSource;
            this.AttackType = TemplarAttackType;
            this.Immunity = TemplarImmunity;
        }

        public override void AttackCreature(IUnit enemyUnit)
        {
            base.AttackCreature(enemyUnit);

            var damage = this.Damage - enemyUnit.Armor > 0 ? this.Damage - enemyUnit.Armor : 0;
            enemyUnit.Health -= damage;
        }
    }
}
