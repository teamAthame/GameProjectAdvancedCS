﻿using AthameRPG.Contracts.Unused;
using AthameRPG.Enums.Unused;

namespace AthameRPG.Objects.Characters.WarUnits.Unused.UndeadHordes
{
    public class Vampire : NightWalker
    {
        private const int VampireInitiative = 40;
        private const int VampiredHealth = 185;
        private const int VampireChanceToHit = 80;
        private const int VampireDamage = 50;
        private const Target VampireTargetType = Target.Anyone;
        private const AttackSource VampireAttackSource = AttackSource.Death;
        private const AttackSource VampireImmunity = AttackSource.Death;
        private const AttackType VampireAttackType = AttackType.DrainLife;
        private const string VampireName = "Vampire";

        protected Vampire()
        {
            this.Name = VampireName;
            this.Health = VampiredHealth;
            this.Initiative = VampireInitiative;
            this.ChanceToHit = VampireChanceToHit;
            this.Damage = VampireDamage;
            this.TargetType = VampireTargetType;
            this.AttackSource = VampireAttackSource;
            this.AttackType = VampireAttackType;
            this.Immunity = VampireImmunity;
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
