namespace AthameRPG.Contracts
{
    public interface ILiving
    {
        int Health { get; set; }

        bool IsAlive { get; }
    }
}