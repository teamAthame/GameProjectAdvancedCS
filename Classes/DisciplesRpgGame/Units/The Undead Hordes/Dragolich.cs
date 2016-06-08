namespace DisciplesRpgGame.Units.The_Undead_Hordes
{
    using DisciplesRpgGame.Enums;
    using DisciplesRpgGame.Interfaces;

    public class Dragolich : NightWalker
    {
        private const int DragolichInitiative = 35;
        private const int DragolichLordHealth = 525;
        private const int DragolichChanceToHit = 80;
        private const int DragolichDamage = 75;
        private const Target DragolichTargetType = Target.Anyone;
        private const AttackSource DragolichAttackSource = AttackSource.Death;
        private const AttackSource DragoLichImmunity = AttackSource.Death;
        private const AttackType DragolichAttackType = AttackType.PestilentialBreath;
        private const string DragolichName = "Dragolich";

        protected Dragolich()
        {
            this.Name = DragolichName;
            this.Health = DragolichLordHealth;
            this.Initiative = DragolichInitiative;
            this.ChanceToHit = DragolichChanceToHit;
            this.Damage = DragolichDamage;
            this.TargetType = DragolichTargetType;
            this.AttackSource = DragolichAttackSource;
            this.AttackType = DragolichAttackType;
            this.Immunity = DragoLichImmunity;
        }

        public override void AttackCreature(IUnit enemyUnit)
        {
            base.AttackCreature(enemyUnit);
            var damage = this.Damage - enemyUnit.Armor > 0 ? this.Damage - enemyUnit.Armor : 0;
            enemyUnit.Health -= damage;
        }
    }
}
