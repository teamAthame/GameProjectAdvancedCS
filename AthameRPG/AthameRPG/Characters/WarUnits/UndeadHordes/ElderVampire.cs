using AthameRPG.Contracts;
using AthameRPG.Enums;

namespace DisciplesRpgGame.Units.The_Undead_Hordes
{
    public class ElderVampire : NightWalker
    {
        private const int ElderVampireInitiative = 40;
        private const int ElderVampiredHealth = 210;
        private const int ElderVampireChanceToHit = 80;
        private const int ElderVampireDamage = 60;
        private const Target ElderVampireTargetType = Target.Anyone;
        private const AttackSource ElderVampireAttackSource = AttackSource.Death;
        private const AttackSource ElderVampireImmunity = AttackSource.Death;
        private const AttackType ElderVampireAttackType = AttackType.DrainLifeOverflow;
        private const string ElderVampireName = "ElderVampire";

        protected ElderVampire()
        {
            this.Name = ElderVampireName;
            this.Health = ElderVampiredHealth;
            this.Initiative = ElderVampireInitiative;
            this.ChanceToHit = ElderVampireChanceToHit;
            this.Damage = ElderVampireDamage;
            this.TargetType = ElderVampireTargetType;
            this.AttackSource = ElderVampireAttackSource;
            this.AttackType = ElderVampireAttackType;
            this.Immunity = ElderVampireImmunity;
        }

        public override void AttackCreature(IUnit enemyUnit)
        {
            base.AttackCreature(enemyUnit);
            var damage = this.Damage - enemyUnit.Armor > 0 ? this.Damage - enemyUnit.Armor : 0;
            enemyUnit.Health -= damage;
            this.Health += damage;
        }
    }
}
