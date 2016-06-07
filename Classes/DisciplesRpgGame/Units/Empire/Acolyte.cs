using DisciplesRpgGame.Enums;

namespace DisciplesRpgGame.Units.Empire
{
    public class Acolyte : Healer
    {
        private const int AcolyteExperience = 0;
        private const int AcolyteHealth = 50;
        private const Target AcolyteTarget = Target.Anyone;
        private const int AcolyteHealing = 20;

        public Acolyte() : 
            base(AcolyteExperience, AcolyteHealth, AcolyteTarget, AcolyteHealing)
        {
        }
    }
}