using Microsoft.Xna.Framework;

namespace AthameRPG.Contracts
{
    public interface IDraw
    {
        Vector2 DrawCoord { get; }
        int CropWidth { get; }
        int CropHeight { get; }
    }
}
