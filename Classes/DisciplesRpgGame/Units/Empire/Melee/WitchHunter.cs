namespace DisciplesRpgGame.Units.Empire.Melee
{
    public class WitchHunter : Fighter
    {
        private const int WitchHunterExperience = 0;
        private const int WitchHunterHealth = 140;
        private const int WitchHunterArmor = 0;
        private const int WitchHunterInitiative = 50;
        private const int WitchHunterDamage = 50;

        public WitchHunter()
            : base(WitchHunterExperience, WitchHunterHealth, WitchHunterArmor, WitchHunterInitiative, WitchHunterDamage)
        {
        }
    }
}