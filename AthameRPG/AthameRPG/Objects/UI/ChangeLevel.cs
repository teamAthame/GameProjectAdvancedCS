using AthameRPG.Enums;
using AthameRPG.GameEngine.Loaders;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AthameRPG.Objects.UI
{
    // loaded from MapManager
    public class ChangeLevel
    {
        private const string ImageChangeLevelPath = @"../Content/Image/levelChanging";
        private const string LoadingSymbol = ".";
        private const float SymbolDrawStepX = 5f;
        private const int MinFrameSwitch = 100;
        private const int MaxLoadingTime = 4000;
        private const int StartingTime = 0;


        private Vector2 imageDrawCoord;
        private Vector2 symbolDrawCoord;

        private int frameCounter;
        private int switchCounter;
        private int elapsedTime;

        private Texture2D levelChangeImage;

        public ChangeLevel()
        {
            this.LoadDefaults();
        }

        private void LoadDefaults()
        {
            this.Status = Status.Completed;
            this.symbolDrawCoord = new Vector2(300, 450);
            this.imageDrawCoord = Vector2.Zero;
            this.switchCounter = MinFrameSwitch;
            this.elapsedTime = StartingTime;
        }

        public Status Status { get; set; }

        public void Restart()
        {
            this.LoadDefaults();
        }

        public void LoadContent(ContentManager contentManager)
        {
            this.levelChangeImage = contentManager.Load<Texture2D>(ImageChangeLevelPath);
        }

        public void Update(GameTime gameTime)
        {
            this.frameCounter += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (this.frameCounter >= this.switchCounter)
            {
                this.frameCounter = 0;

                this.symbolDrawCoord.X += SymbolDrawStepX;

                this.elapsedTime += this.switchCounter;

                if (this.elapsedTime == MaxLoadingTime)
                {
                    this.Status = Status.Completed;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.levelChangeImage,this.imageDrawCoord,Color.White);
            spriteBatch.DrawString(FontLoader.BigSizeFont, LoadingSymbol,this.symbolDrawCoord,Color.White);
        }
    }
}
