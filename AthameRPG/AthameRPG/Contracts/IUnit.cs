namespace DisciplesRpgGame.Interfaces
{
    public interface IUnit : IAttacker, IArmored, ILiving, IImmune
    {
        string Name { get; }

        int Experience { get; }
    }
}
