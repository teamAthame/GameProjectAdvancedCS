﻿namespace DisciplesRpgGame.Units.Empire.Mages
{
    public class Apprentice : Caster
    {
        private const int ApprenticeHealth = 35;
        private const int ApprenticeDamage = 15;

        public Apprentice() 
            : base(ApprenticeHealth, ApprenticeDamage)
        {
        }
    }
}