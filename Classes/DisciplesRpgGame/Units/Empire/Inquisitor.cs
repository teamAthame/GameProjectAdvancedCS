namespace DisciplesRpgGame.Units.Empire
{
    public class Inquisitor : Fighter
    {
        private const int InquisitorExperience = 0;
        private const int InquisitorHealth = 180;
        private const int InquisitorArmor = 0;
        private const int InquisitorInitiative = 50;
        private const int InquisitorDamage = 75;

        public Inquisitor()
            : base(InquisitorExperience, InquisitorHealth, InquisitorArmor, InquisitorInitiative, InquisitorDamage)
        {
        }
    }
}