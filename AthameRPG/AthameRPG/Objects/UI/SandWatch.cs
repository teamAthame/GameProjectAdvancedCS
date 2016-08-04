namespace AthameRPG.Objects.UI
{
    using AthameRPG.Contracts;
    using AthameRPG.Enums;
    using AthameRPG.GameEngine.Collisions;
    using AthameRPG.GameEngine.Managers;
    using AthameRPG.Objects.Characters.Heroes;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    public class SandWatch : ISoundable
    {
        public event OnEvent OnEvent;

        // loaded from MapManager
        public static bool TurnIsClicked;

        private const int DrawCoordX = 750;
        private const int DrawCoordY = 480;
        private const int ImageCropX = 250;
        private const int ImageCropY = 200;
        private const int ImageWidthHeight = 50;
        private const int CounterStarValue = 0;

        private MouseState newMouseState;
        private MouseState oldMouseState;
        
        private Vector2 drawCoord;
        private Rectangle cropDimmensionsOfWatch;

        public SandWatch()
        {
            this.drawCoord = new Vector2(DrawCoordX, DrawCoordY);
            this.cropDimmensionsOfWatch = new Rectangle(ImageCropX, ImageCropY, ImageWidthHeight, ImageWidthHeight);
            this.SoundStatus = SoundStatus.Click2;
        }

        public int TurnCounter { get; private set; }

        public Vector2 DrawCoordSandwatch
        {
            get { return this.drawCoord; }
        }

        public void Restart()
        {
            this.TurnCounter = CounterStarValue;
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
                        this.TurnCounter++;

                        CharacterManager.barbarian.ReFillMovement();

                        foreach (var enemy in CharacterManager.enemiesList)
                        {
                            enemy.ReFillMovement();
                        }
                        CharacterManager.itIsPlayerTurn = false;
                        TurnIsClicked = true;

                        if (this.OnEvent != null)
                        {
                            this.OnEvent(this);
                        }
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

                        if (this.OnEvent != null)
                        {
                            this.OnEvent(this);
                        }
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

        public SoundStatus SoundStatus { get; private set; }
    }
}
