using AthameRPG.Enums;
using DisciplesRpgGame.Units;

namespace AthameRPG.Characters.WarUnits.Empire.Ranged
{
    public abstract class Ranger : AttackUnit
    {
        private const int RangerInitiative = 60;
        private const int RangerArmor = 0;
        private const Target RangerTarget = Target.Anyone;
        private const AttackSource RangerAttackSource = AttackSource.Weapon;

        protected Ranger(int experience, int health, int chanceToHit, int damage) 
            : base(experience, health, RangerArmor, RangerInitiative,
                  chanceToHit, RangerTarget, RangerAttackSource, damage)
        {
        }
    }
}