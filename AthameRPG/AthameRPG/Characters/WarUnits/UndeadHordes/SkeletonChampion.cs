namespace DisciplesRpgGame.Units.The_Undead_Hordes
{
    using DisciplesRpgGame.Enums;
    using DisciplesRpgGame.Interfaces;

    public class SkeletonChampion : NightWalker
    {
        private const int SkeletonChampionInitiative = 50;
        private const int SkeletonChampionHealth = 270;
        private const int SkeletonChampionChanceToHit = 80;
        private const int SkeletonChampionDamage = 100;
        private const Target SkeletonChampionTargetType = Target.Adjacent;
        private const AttackSource SkeletonChampionAttackSource = AttackSource.Weapon;
        private const AttackSource SkeletonChampionImmunity = AttackSource.Death;
        private const AttackType SkeletonChampionAttackType = AttackType.LongSword;
        private const string SkeletonChampionName = "SkeletonChampion";

        protected SkeletonChampion()
        {
            this.Name = SkeletonChampionName;
            this.Health = SkeletonChampionHealth;
            this.Initiative = SkeletonChampionInitiative;
            this.ChanceToHit = SkeletonChampionChanceToHit;
            this.Damage = SkeletonChampionDamage;
            this.TargetType = SkeletonChampionTargetType;
            this.AttackSource = SkeletonChampionAttackSource;
            this.AttackType = SkeletonChampionAttackType;
            this.Immunity = SkeletonChampionImmunity;
        }

        public override void AttackCreature(IUnit enemyUnit)
        {
            base.AttackCreature(enemyUnit);

            var damage = this.Damage - enemyUnit.Armor > 0 ? this.Damage - enemyUnit.Armor : 0;
            enemyUnit.Health -= damage;
        }
    }
}
