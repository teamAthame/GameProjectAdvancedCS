namespace DisciplesRpgGame.Units.The_Undead_Hordes
{
    using DisciplesRpgGame.Enums;
    using DisciplesRpgGame.Interfaces;

    public class Shade : NightWalker
    {
        private const int ShadeInitiative = 20;
        private const int ShadeHealth = 135;
        private const int ShadeChanceToHit = 50;
        private const int ShadeDamage = 0;
        private const Target ShadeTargetType = Target.Anyone;
        private const AttackSource ShadeAttackSource = AttackSource.Mind;
        private const AttackSource ShadeImmunity = AttackSource.Death;
        private const AttackType ShadeAttackType = AttackType.Paralyze;
        private const string ShadeName = "Shade";

        protected Shade()
        {
            this.Name = ShadeName;
            this.Health = ShadeHealth;
            this.Initiative = ShadeInitiative;
            this.ChanceToHit = ShadeChanceToHit;
            this.Damage = ShadeDamage;
            this.TargetType = ShadeTargetType;
            this.AttackSource = ShadeAttackSource;
            this.AttackType = ShadeAttackType;
            this.Immunity = ShadeImmunity;
        }

        public override void AttackCreature(IUnit enemyUnit)
        {
            base.AttackCreature(enemyUnit);

            enemyUnit.Initiative = 0;
        }
    }
}
