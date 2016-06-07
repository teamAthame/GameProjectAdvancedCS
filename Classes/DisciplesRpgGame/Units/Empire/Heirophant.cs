using DisciplesRpgGame.Enums;

namespace DisciplesRpgGame.Units.Empire
{
    public class Heirophant : Healer
    {
        private const int HeirophantExperience = 0;
        private const int HeirophantHealth = 125;
        private const Target HeirophantTarget = Target.Anyone;
        private const int HeirophantHealing = 120;

        public Heirophant() 
            : base(HeirophantExperience, HeirophantHealth, HeirophantTarget, HeirophantHealing)
        {
        }
    }
}