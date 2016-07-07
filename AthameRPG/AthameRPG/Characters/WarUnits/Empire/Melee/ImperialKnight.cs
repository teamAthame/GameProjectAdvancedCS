namespace AthameRPG.Characters.WarUnits.Empire.Melee
{
    public class ImperialKnight : Fighter
    {
        private const int ImperialKnightExperience = 0;
        private const int ImperialKnightHealth = 200;
        private const int ImperialKnightArmor = 0;
        private const int ImperialKnightInitiative = 50;
        private const int ImperialKnightDamage = 75;

        public ImperialKnight()
            : base(ImperialKnightExperience, ImperialKnightHealth, ImperialKnightArmor, ImperialKnightInitiative, ImperialKnightDamage)
        {
        }
    }
}