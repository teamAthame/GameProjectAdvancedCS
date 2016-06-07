namespace DisciplesRpgGame.Units.Empire
{
    public class Angel : Fighter
    {
        private const int AngelExperience = 0;
        private const int AngelHealth = 225;
        private const int AngelArmor = 0;
        private const int AngelInitiative = 50;
        private const int AngelDamage = 125;

        public Angel()
            : base(AngelExperience, AngelHealth, AngelArmor, AngelInitiative, AngelDamage)
        {
        }
    }
}