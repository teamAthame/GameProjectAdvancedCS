
using AthameRPG.Characters;
using AthameRPG.GameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AthameRPG.Objects.UI
{
    public class SandWatch
    {
        // load from MapManager

        private const int DrawCoordX = 750;
        private const int DrawCoordY = 480;
        private const int ImageCropX = 250;
        private const int ImageCropY = 200;
        private const int ImageWidthHeight = 50;

        protected MouseState newMouseState;
        protected MouseState oldMouseState;
        private bool nextTurnIsClicked;

        private Vector2 drawCoord;
        private Rectangle cropDimmensionsOfWatch;

        public SandWatch()
        {
            this.drawCoord = new Vector2(DrawCoordX, DrawCoordY);
            this.cropDimmensionsOfWatch = new Rectangle(ImageCropX, ImageCropY, ImageWidthHeight, ImageWidthHeight);
        }

        public bool NextTurnIsClicked
        {
            get { return this.nextTurnIsClicked; }
        }

        public void Update(GameTime gameTime)
        {
            this.oldMouseState = this.newMouseState;
            this.newMouseState = Mouse.GetState();

            if (!Character.GetIsInBattle && ! Character.GetIsInCastle)
            {
                if (this.newMouseState.X > DrawCoordX && this.newMouseState.X < (DrawCoordX + ImageWidthHeight) && this.newMouseState.Y > DrawCoordY && this.newMouseState.Y < (DrawCoordY + ImageWidthHeight))
                {
                    if (this.newMouseState.LeftButton == ButtonState.Pressed && this.oldMouseState.LeftButton == ButtonState.Released)
                    {
                        this.nextTurnIsClicked = true;
                    }
                    else
                    {
                        this.nextTurnIsClicked = false;
                    }
                }
                
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
