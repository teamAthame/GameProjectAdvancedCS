namespace DisciplesRpgGame.Units.The_Undead_Hordes
{
    using DisciplesRpgGame.Enums;
    using DisciplesRpgGame.Interfaces;

    public class Specter : NightWalker
    {
        private const int SpecterInitiative = 20;
        private const int SpecterHealth = 90;
        private const int SpecterChanceToHit = 70;
        private const int SpecterDamage = 0;
        private const Target SpecterTargetType = Target.Anyone;
        private const AttackSource SpecterAttackSource = AttackSource.Mind;
        private const AttackSource SpecterImmunity = AttackSource.Death;
        private const AttackType SpecterAttackType = AttackType.Paralyze;
        private const string SpecterName = "Specter";

        protected Specter()
        {
            this.Name = SpecterName;
            this.Health = SpecterHealth;
            this.Initiative = SpecterInitiative;
            this.ChanceToHit = SpecterChanceToHit;
            this.Damage = SpecterDamage;
            this.TargetType = SpecterTargetType;
            this.AttackSource = SpecterAttackSource;
            this.AttackType = SpecterAttackType;
            this.Immunity = SpecterImmunity;
        }

        public override void AttackCreature(IUnit enemyUnit)
        {
            base.AttackCreature(enemyUnit);

            enemyUnit.Initiative = 0;
        }
    }
}
