namespace DisciplesRpgGame.Units.Empire
{
    public class DefenderOfFaith : Fighter
    {
        private const int DefenderOfFaithExperience = 0;
        private const int DefenderOfFaithHealth = 225;
        private const int DefenderOfFaithArmor = 30;
        private const int DefenderOfFaithInitiative = 70;
        private const int DefenderOfFaithDamage = 125;

        public DefenderOfFaith()
            : base(DefenderOfFaithExperience, DefenderOfFaithHealth, DefenderOfFaithArmor, DefenderOfFaithInitiative, DefenderOfFaithDamage)
        {
        }
    }
}