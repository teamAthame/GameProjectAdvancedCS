namespace AthameRPG.Characters.WarUnits.Empire.Mages
{
    public class Mage : Caster
    {
        private const int MageHealth = 65;
        private const int MageDamage = 30;

        public Mage()
            : base(MageHealth, MageDamage)
        {
        }
    }
}