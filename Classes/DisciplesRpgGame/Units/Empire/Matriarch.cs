using DisciplesRpgGame.Enums;

namespace DisciplesRpgGame.Units.Empire
{
    public class Matriarch : Healer
    {
        private const int MatriarchExperience = 0;
        private const int MatriarchHealth = 100;
        private const int MatriarchArmor = 0;
        private const int MatriarchInitiative = 10;
        private const Target MatriarchTarget = Target.All;
        private const int MatriarchHealing = 40;

        public Matriarch()
            : base(MatriarchExperience, MatriarchHealth, MatriarchArmor, MatriarchInitiative, MatriarchTarget, MatriarchHealing)
        {
        }
    }
}