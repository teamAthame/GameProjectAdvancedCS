﻿using AthameRPG.Enums;

namespace AthameRPG.Characters.WarUnits.Empire.Healers
{
    public class ImperialPriest : Healer
    {
        private const int ImperialPriestExperience = 0;
        private const int ImperialPriestHealth = 100;
        private const Target ImperialPriestTarget = Target.Anyone;
        private const int ImperialPriestHealing = 80;

        public ImperialPriest() 
            : base(ImperialPriestExperience, ImperialPriestHealth, ImperialPriestTarget, ImperialPriestHealing)
        {
        }
    }
}