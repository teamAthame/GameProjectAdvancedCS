namespace AthameRPG.Characters.WarUnits.Unused.Empire.Ranged
{
    public class Archer : Ranger
    {
        private const int ArcherExperience = 0;
        private const int ArcherHealth = 45;
        private const int ArcherChanceToHit = 80;
        private const int ArcherDamage = 25;

        public Archer() 
            : base(ArcherExperience, ArcherHealth, ArcherChanceToHit, ArcherDamage)
        {
        }
    }
}