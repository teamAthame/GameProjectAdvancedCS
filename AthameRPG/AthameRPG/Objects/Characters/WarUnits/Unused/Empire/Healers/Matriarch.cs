using AthameRPG.Enums.Unused;

namespace AthameRPG.Characters.WarUnits.Unused.Empire.Healers
{
    public class Matriarch : Healer
    {
        private const int MatriarchExperience = 0;
        private const int MatriarchHealth = 100;
        private const Target MatriarchTarget = Target.All;
        private const int MatriarchHealing = 40;

        public Matriarch() 
            : base(MatriarchExperience, MatriarchHealth, MatriarchTarget, MatriarchHealing)
        {
        }
    }
}