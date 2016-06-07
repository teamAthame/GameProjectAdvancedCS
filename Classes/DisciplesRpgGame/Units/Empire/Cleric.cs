using DisciplesRpgGame.Enums;

namespace DisciplesRpgGame.Units.Empire
{
    public class Cleric : Healer
    {
        private const int ClericExperience = 0;
        private const int ClericHealth = 75;
        private const Target ClericTarget = Target.All;
        private const int ClericHealing = 20;

        public Cleric() 
            : base(ClericExperience, ClericHealth, ClericTarget, ClericHealing)
        {
        }
    }
}