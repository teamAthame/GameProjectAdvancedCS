using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics; 

namespace AthameRPG.GameEngine.Screens
{
    public class MenuScreen :GameScreen
    {
        private const string NEW_GAME_TEXT = "NEW GAME";
        private const float IMAGE_X = 150f;
        private const float IMAGE_Y = 144f;
        private const float NEW_GAME_X = 280f;
        private const float NEW_GAME_Y = 480f;
        private const float EXIT_X = 0f;
        private const float EXIT_Y = 0f;

        private SpriteFont spriteFont;
        private Vector2 imagePosition;
        private Vector2 newGamePosition;

        public MenuScreen()
        {
            this.ImagePosition = imagePosition;
            this.NewGamePosition = newGamePosition;
        }
        //
        Texture2D image;
        string path;
        

        public Vector2 NewGamePosition
        {
            get
            {
                return this.newGamePosition;
            }
            private set
            {
                this.newGamePosition = new Vector2(NEW_GAME_X, NEW_GAME_Y);
            }
        }
        public Vector2 ImagePosition
        {
            get
            {
                return this.imagePosition;
            }
            private set
            {
                this.imagePosition = new Vector2(IMAGE_X, IMAGE_Y);
            }
        }

        public override void LoadContent()
        {
            base.LoadContent();

            path = "../Content/Image/athame3";
            image = content.Load<Texture2D>(path);
            spriteFont = content.Load<SpriteFont>("../Content/Fonts/ArialBig");
        }
        public override void UnloadContent()
        {
            base.UnloadContent();
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            

            spriteBatch.Draw(image, ImagePosition, Color.Gold);
            spriteBatch.DrawString(spriteFont, NEW_GAME_TEXT , NewGamePosition, Color.Red);
            
        }
    }
}
