namespace AthameRPG.GameEngine.Screens
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    internal class SplashScreen : GameScreen
    {
        private KeyboardState keyState;
        private MouseState mousteState;
        private Texture2D image;
        private Vector2 position;
        private string imagePath;

        public SplashScreen()
        {
            this.imagePath = @"../Content/Image/AthameSplashScreen";
            this.Position = new Vector2(0, 0);
        }

        public Vector2 Position
        {
            get
            {
                return this.position;
            }

            set
            {
                this.position = value;
            }
        }

        public override void LoadContent()
        {
            base.LoadContent();
            this.image = content.Load<Texture2D>(this.imagePath);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        { 
            this.keyState = Keyboard.GetState();
            this.mousteState = Mouse.GetState();
            if (this.keyState.IsKeyDown(Keys.Enter) || this.mousteState.LeftButton == ButtonState.Pressed)
            {
                ScreenManager.Instance.ChangeScreens("MenuScreen");
            }
            
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.Draw(this.image, this.position);
        }
    }
}


