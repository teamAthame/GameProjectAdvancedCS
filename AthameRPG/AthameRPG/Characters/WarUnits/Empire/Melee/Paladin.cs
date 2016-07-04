namespace DisciplesRpgGame.Units.Empire.Melee
{
    public class Paladin : Fighter
    {
        private const int PaladinExperience = 0;
        private const int PaladinHealth = 175;
        private const int PaladinArmor = 30;
        private const int PaladinInitiative = 50;
        private const int PaladinDamage = 100;

        public Paladin()
            : base(PaladinExperience, PaladinHealth, PaladinArmor, PaladinInitiative, PaladinDamage)
        {
        }
    }
}