using DisciplesRpgGame.Enums;

namespace DisciplesRpgGame.Units.Empire.Melee
{
    public abstract class Fighter : AttackUnit
    {
        private const int FighterChanceToHit = 80;
        private const Target FighterTarget = Target.Adjacent;
        private const AttackSource FigtherAttackSource = AttackSource.Weapon;

        protected Fighter(int experience, int health, int armor, int initiative, int damage)
            : base(experience, health, armor, initiative, FighterChanceToHit, 
                  FighterTarget, FigtherAttackSource, damage)
        {
        }
    }
}