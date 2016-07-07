using AthameRPG.Characters.Heroes;

namespace AthameRPG.Contracts
{
    public interface ICombatHandler
    {
        Unit Unit { get; set; }

        IAttack GenerateAtack();
    }
}