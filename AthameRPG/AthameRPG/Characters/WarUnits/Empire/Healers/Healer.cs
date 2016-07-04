﻿using System;
using AthameRPG.Enums;
using AthameRPG.Contracts;

namespace AthameRPG.Characters.WarUnits.Empire.Healers
{
    public abstract class Healer : DisciplesRpgGame.Units.Unit, IHeal
    {
        private const int HealerArmor = 0;
        private const int HealerInitiative = 10;
        private const int HealerChanceToHit = 100;
        private const AttackSource HealerAttackSource = AttackSource.Life;

        private int healingPoints;

        protected Healer(int experience, int health, Target targetType, int healingPoints) 
            : base(experience, health, HealerArmor, HealerInitiative, HealerChanceToHit, targetType, HealerAttackSource)
        {
            this.HealingPoints = healingPoints;
        }

        public int HealingPoints
        {
            get
            {
                return this.healingPoints;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("HealingPoints", "HealingPoints cannot be a negative number,");
                }

                this.healingPoints = value;
            }
        }
    }
}