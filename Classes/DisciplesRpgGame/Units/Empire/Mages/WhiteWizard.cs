namespace DisciplesRpgGame.Units.Empire.Mages
{
    public class WhiteWizard : Caster
    {
        private const int WhiteWizardHealth = 125;
        private const int WhiteWizardDamage = 60;

        public WhiteWizard()
            : base(WhiteWizardHealth, WhiteWizardDamage)
        {
        }
    }
}