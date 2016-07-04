namespace AthameRPG.Characters.WarUnits.Empire.Melee
{
    public class GrandInquisitor : Fighter
    {
        private const int GrandInquisitorExperience = 0;
        private const int GrandInquisitorHealth = 210;
        private const int GrandInquisitorArmor = 0;
        private const int GrandInquisitorInitiative = 50;
        private const int GrandInquisitorDamage = 100;

        public GrandInquisitor()
            : base(GrandInquisitorExperience, GrandInquisitorHealth, GrandInquisitorArmor, GrandInquisitorInitiative, GrandInquisitorDamage)
        {
        }
    }
}