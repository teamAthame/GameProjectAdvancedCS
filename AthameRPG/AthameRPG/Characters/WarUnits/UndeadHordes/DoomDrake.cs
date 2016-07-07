using AthameRPG.Contracts;
using AthameRPG.Enums;

namespace DisciplesRpgGame.Units.The_Undead_Hordes
{
    public class DoomDrake : NightWalker
    {
        private const int DoomDrakeInitiative = 35;
        private const int DoomDrakeLordHealth = 300;
        private const int DoomDrakeChanceToHit = 80;
        private const int DoomDrakeDamage = 40;
        private const Target DoomDrakeTargetType = Target.Anyone;
        private const AttackSource DoomDrakeAttackSource = AttackSource.Death;
        private const AttackSource DoomDrakeImmunity = AttackSource.None;
        private const AttackType DoomDrakeAttackType = AttackType.Breath;
        private const string DoomDrakeName = "DoomDrake";

        protected DoomDrake()
        {
            this.Name = DoomDrakeName;
            this.Health = DoomDrakeLordHealth;
            this.Initiative = DoomDrakeInitiative;
            this.ChanceToHit = DoomDrakeChanceToHit;
            this.Damage = DoomDrakeDamage;
            this.TargetType = DoomDrakeTargetType;
            this.AttackSource = DoomDrakeAttackSource;
            this.AttackType = DoomDrakeAttackType;
            this.Immunity = DoomDrakeImmunity;
        }

        public override void AttackCreature(IUnit enemyUnit)
        {
            base.AttackCreature(enemyUnit);
            var damage = this.Damage - enemyUnit.Armor > 0 ? this.Damage - enemyUnit.Armor : 0;
            enemyUnit.Health -= damage;
        }
    }
}
