using System;
using AthameRPG.Contracts;
using AthameRPG.Enums;

namespace DisciplesRpgGame.Units.The_Undead_Hordes
{
    public class Necromancer : NightWalker
    {
        private const int NecromancerInitiative = 40;
        private const int NecromancerHealth = 105;
        private const int NecromancerChanceToHit = 80;
        private const int NecromancerDamage = 45;
        private const Target NecromancerTargetType = Target.Anyone;
        private const AttackSource NecromancerAttackSource = AttackSource.Death;
        private const AttackSource NecromancerImmunity = AttackSource.None;
        private const AttackType NecromancerAttackType = AttackType.Pestilence;
        private const string NecromancerName = "Necromancer";

        protected Necromancer()
        {
            this.Name = NecromancerName;
            this.Health = NecromancerHealth;
            this.Initiative = NecromancerInitiative;
            this.ChanceToHit = NecromancerChanceToHit;
            this.Damage = NecromancerDamage;
            this.TargetType = NecromancerTargetType;
            this.AttackSource = NecromancerAttackSource;
            this.AttackType = NecromancerAttackType;
            this.Immunity = NecromancerImmunity;
        }

        public override void AttackCreature(IUnit enemyUnit)
        {
            base.AttackCreature(enemyUnit);

            var damage = this.Damage - enemyUnit.Armor > 0 ? this.Damage - enemyUnit.Armor : 0;
            enemyUnit.Health -= damage;
        }
    }
}
