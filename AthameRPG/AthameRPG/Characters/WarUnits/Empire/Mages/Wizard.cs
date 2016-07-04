﻿namespace DisciplesRpgGame.Units.Empire.Mages
{
    public class Wizard : Caster
    {
        private const int WizardHealth = 95;
        private const int WizardDamage = 45;

        public Wizard()
            : base(WizardHealth, WizardDamage)
        {
        }
    }
}