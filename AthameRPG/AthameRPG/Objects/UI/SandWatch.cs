
using AthameRPG.Characters;
using AthameRPG.GameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AthameRPG.Objects.UI
{
    public class SandWatch
    {
        private const int DrawCoordX = 750;
        private const int DrawCoordY = 480;
        private const int ImageCropX = 250;
        private const int ImageCropY = 200;
        private const int ImageWidthHeight = 50;

        private Vector2 drawCoord;
        private Rectangle cropDimmensionsOfWatch;

        public SandWatch()
        {
            this.drawCoord = new Vector2(DrawCoordX, DrawCoordY);
            this.cropDimmensionsOfWatch = new Rectangle(ImageCropX, ImageCropY, ImageWidthHeight, ImageWidthHeight);
        }

        public void Update(GameTime gameTime)
        {
            if (!Character.GetIsInBattle && ! Character.GetIsInCastle)
            {

            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!Character.GetIsInBattle && !Character.GetIsInCastle)
            {
                spriteBatch.Draw(MapManager.Instance.Terrain, this.drawCoord, this.cropDimmensionsOfWatch, Color.White);
            }
        }
    }
}
