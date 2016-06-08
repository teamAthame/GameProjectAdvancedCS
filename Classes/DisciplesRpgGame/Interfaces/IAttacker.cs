namespace DisciplesRpgGame.Interfaces
{
    using DisciplesRpgGame.Enums;

    public interface IAttacker
    {
        AttackSource AttackSource { get; }

        AttackType AttackType { get; }

        int Initiative { get; set; }

        int ChanceToHit { get; }

        void AttackCreature(IUnit enemyUnit);
    }
}
