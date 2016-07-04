
using AthameRPG.Characters;
using AthameRPG.Characters.Heroes;
using AthameRPG.GameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AthameRPG.Objects.UI
{
    public class SandWatch
    {
        // loaded from MapManager
        public static bool TurnIsClicked;

        private const int DrawCoordX = 750;
        private const int DrawCoordY = 480;
        private const int ImageCropX = 250;
        private const int ImageCropY = 200;
        private const int ImageWidthHeight = 50;

        protected MouseState newMouseState;
        protected MouseState oldMouseState;
        
        private Vector2 drawCoord;
        private Rectangle cropDimmensionsOfWatch;

        public SandWatch()
        {
            this.drawCoord = new Vector2(DrawCoordX, DrawCoordY);
            this.cropDimmensionsOfWatch = new Rectangle(ImageCropX, ImageCropY, ImageWidthHeight, ImageWidthHeight);
        }

        public Vector2 DrawCoordSandwatch
        {
            get { return this.drawCoord; }
        }
        
        public void Update(GameTime gameTime)
        {
            this.oldMouseState = this.newMouseState;
            this.newMouseState = Mouse.GetState();

            if (!Character.GetIsInBattle && ! Character.GetIsInCastle)
            {
                // this.newMouseState.X > DrawCoordX && this.newMouseState.X < (DrawCoordX + ImageWidthHeight) && this.newMouseState.Y > DrawCoordY && this.newMouseState.Y < (DrawCoordY + ImageWidthHeight)
                //CollisionDetection.IsMouseOverObject(this.drawCoord, ImageCropX, ImageCropY,gameTime)

                if (CollisionDetection.IsMouseOverObject(this.drawCoord, ImageCropX, ImageCropY, gameTime))
                {
                    if (this.newMouseState.LeftButton == ButtonState.Pressed && this.oldMouseState.LeftButton == ButtonState.Released)
                    {
                        CharacterManager.barbarian.ReFillMovement();

                        foreach (var enemy in CharacterManager.enemiesList)
                        {
                            enemy.ReFillMovement();
                        }
                        CharacterManager.itIsPlayerTurn = false;
                        TurnIsClicked = true;
                    }
                }
                
            }
            else if (Character.GetIsInBattle)
            {
                if (CollisionDetection.IsMouseOverObject(this.drawCoord, ImageCropX, ImageCropY, gameTime))
                {
                    if (this.newMouseState.LeftButton == ButtonState.Pressed && this.oldMouseState.LeftButton == ButtonState.Released)
                    {
                        MapManager.Instance.Battlefield.EndCurrentTurn();
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!Character.GetIsInCastle)
            {
                spriteBatch.Draw(MapManager.Instance.Terrain, this.drawCoord, this.cropDimmensionsOfWatch, Color.White);
            }
        }
    }
}
