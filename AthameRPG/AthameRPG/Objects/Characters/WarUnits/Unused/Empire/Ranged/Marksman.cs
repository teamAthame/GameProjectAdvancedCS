namespace AthameRPG.Characters.WarUnits.Unused.Empire.Ranged
{
    public class Marksman : Ranger
    {
        private const int MarksmanExperience = 0;
        private const int MarksmanHealth = 90;
        private const int MarksmanChanceToHit = 85;
        private const int MarksmanDamage = 40;

        public Marksman()
            : base(MarksmanExperience, MarksmanHealth, MarksmanChanceToHit, MarksmanDamage)
        {
        }
    }
}