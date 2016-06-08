using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input; 

namespace AthameRPG.GameEngine.Screens
{
    public class MenuScreen : GameScreen
    {
        private const string NEW_GAME_TEXT = "NEW GAME";
        private const string EXIT_TEXT = "EXIT";
        private const float IMAGE_X = 150f;
        private const float IMAGE_Y = 50f;
        private const float NEW_GAME_X = 0f;
        private const float NEW_GAME_Y = 390f;
        private const float EXIT_X = 0f;
        private const float EXIT_Y = 460f;
        private const float HALF_SCREEN_WIDTH = ScreenManager.SCREEN_WIDTH / 2;
        //private const float HALF_SCREEN_HEIGHT = ScreenManager.SCREEN_HEIGHT / 2;

        private string imagePath;
        private bool willChangeColor;
        private Color newGameColor;
        private Color exitColor;
        private MouseState mouse;
        private Texture2D image;
        private SpriteFont spriteFont;
        private Vector2 imagePosition;
        private Vector2 newGamePosition;
        private Vector2 exitTextPosition;

        public MenuScreen()
        {
            this.ImagePosition = imagePosition;
            this.NewGameTextPosition = newGamePosition;
            this.ExitTextPosition = exitTextPosition;
            this.imagePath = @"../Content/Image/athame3";
            this.newGameColor = Color.White;
            this.exitColor = Color.Red;
            
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
        public Vector2 NewGameTextPosition
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
        public Vector2 ExitTextPosition
        {
            get
            {
                return this.exitTextPosition;
            }
            private set
            {
                this.exitTextPosition = new Vector2(EXIT_X, EXIT_Y);
            }
        }

        public override void LoadContent()
        {
            base.LoadContent();
            
            image = content.Load<Texture2D>(imagePath);
            spriteFont = content.Load<SpriteFont>("../Content/Fonts/ArialBig");
            
        }
        public override void UnloadContent()
        {
            base.UnloadContent();
            
        }
        public override void Update(GameTime gameTime)
        {
            mouse = Mouse.GetState();
            
            PositioningInTheMiddleOfTheScreen();

            willChangeColor = IsMouseOverText(NEW_GAME_TEXT, NEW_GAME_Y);

            newGameColor = willChangeColor == true ? Color.White : Color.Red;

            if (willChangeColor)
            {
                // GO TO THE GAME !!!
                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    ScreenManager.Instance.ChangeScreens("MapScreen");
                }
            }

            willChangeColor = IsMouseOverText(EXIT_TEXT, EXIT_Y);

            exitColor = willChangeColor == true ? Color.White : Color.Red;

            if (willChangeColor)
            {
                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    Game1.exitGame = true;
                }               
            }

            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            // Center the 'IMAGE','New Game' and 'EXIT' in the middle of the screen
            //PositioningInTheMiddleOfTheScreen();
            spriteBatch.Begin();
            spriteBatch.Draw(image, ImagePosition, Color.Gold);
            spriteBatch.DrawString(spriteFont, NEW_GAME_TEXT, NewGameTextPosition, newGameColor);
            spriteBatch.DrawString(spriteFont, EXIT_TEXT, ExitTextPosition, exitColor);
            spriteBatch.End();
        }

        private void PositioningInTheMiddleOfTheScreen()
        {
            imagePosition = new Vector2(HALF_SCREEN_WIDTH - (image.Width / 2), IMAGE_Y);
            newGamePosition = new Vector2(HALF_SCREEN_WIDTH - spriteFont.MeasureString(NEW_GAME_TEXT).X / 2, NEW_GAME_Y);
            exitTextPosition = new Vector2(HALF_SCREEN_WIDTH - spriteFont.MeasureString(EXIT_TEXT).X / 2, EXIT_Y);
        }

        private bool IsMouseOverText(string text, float coordY)
        {
            bool overX = mouse.X >= HALF_SCREEN_WIDTH - spriteFont.MeasureString(text).X / 2
                && mouse.X <= HALF_SCREEN_WIDTH - spriteFont.MeasureString(text).X / 2
                + spriteFont.MeasureString(text).X;
            bool overY = (mouse.Y >= coordY) && (mouse.Y <= (spriteFont.MeasureString(text).Y + coordY));

            if (overX && overY)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

         
    }
}
