using DisciplesRpgGame.Enums;

namespace DisciplesRpgGame.Units.Empire
{
    public class Priest : Healer
    {
        private const int PriestExperience = 0;
        private const int PriestHealth = 75;
        private const Target PriestTarget = Target.Anyone;
        private const int PriestHealing = 40;

        public Priest() 
            : base(PriestExperience, PriestHealth, PriestTarget, PriestHealing)
        {
        }
    }
}