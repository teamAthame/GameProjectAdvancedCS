namespace AthameRPG.Objects.Characters.WarUnits.Unused.Empire.Melee
{
    public class Knight : Fighter
    {
        private const int KnightExperience = 0;
        private const int KnightHealth = 150;
        private const int KnightArmor = 0;
        private const int KnightInitiative = 50;
        private const int KnightDamage = 50;

        public Knight()
            : base(KnightExperience, KnightHealth, KnightArmor, KnightInitiative, KnightDamage)
        {
        }
    }
}