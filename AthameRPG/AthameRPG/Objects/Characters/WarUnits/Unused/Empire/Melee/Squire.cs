namespace AthameRPG.Characters.WarUnits.Unused.Empire.Melee
{
    public class Squire : Fighter
    {
        private const int SquireExperience = 0;
        private const int SquireHealth = 100;
        private const int SquireArmor = 0;
        private const int SquireInitiative = 50;
        private const int SquireDamage = 25;

        public Squire() 
            : base(SquireExperience, SquireHealth, SquireArmor, SquireInitiative, SquireDamage)
        {
        }
    }
}