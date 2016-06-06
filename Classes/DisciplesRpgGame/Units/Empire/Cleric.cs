using DisciplesRpgGame.Enums;

namespace DisciplesRpgGame.Units.Empire
{
    public class Cleric : Healer
    {
        private const int ClericExperience = 0;
        private const int ClericHealth = 75;
        private const int ClericArmor = 0;
        private const int ClericInitiative = 10;
        private const Target ClericTarget = Target.All;
        private const int ClericHealing = 20;

        public Cleric() 
            : base(ClericExperience, ClericHealth, ClericArmor, ClericInitiative, ClericTarget, ClericHealing)
        {
        }
    }
}