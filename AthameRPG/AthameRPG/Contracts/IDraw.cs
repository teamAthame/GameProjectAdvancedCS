namespace AthameRPG.Contracts
{
    using Microsoft.Xna.Framework;

    public interface IDraw
    {
        Vector2 DrawCoord { get; }
        int CropWidth { get; }
        int CropHeight { get; }
    }
}
