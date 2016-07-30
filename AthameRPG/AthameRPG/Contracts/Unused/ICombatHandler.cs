using AthameRPG.Objects.Characters.Heroes;

namespace AthameRPG.Contracts.Unused
{
    public interface ICombatHandler
    {
        Unit Unit { get; set; }

        IAttack GenerateAtack();
    }
}