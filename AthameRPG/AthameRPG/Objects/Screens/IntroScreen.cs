using AthameRPG.Contracts;
using AthameRPG.GameEngine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AthameRPG.Objects.Screens
{
    public class IntroScreen : GameScreen
    {
        public override event OnClick OnClick;

        private const string INTRO_IMAGE_PATH =  @"../Content/Image/AthameSplashScreen";
        private const float HALF_SCREEN_WIDTH = ScreenManager.SCREEN_WIDTH / 2;
        private const float HALF_SCREEN_HEIGHT = ScreenManager.SCREEN_HEIGHT / 2;

        private Color colorIntro = Color.White;

        private Texture2D imageIntro; /// where image is keep
        private Vector2 positionIntro; /// draw coordinates of image

        private MouseState mouse;
        
        public Vector2 PositionIntro
        {
            get
            {
                return this.positionIntro;
            }
            private set
            {
                this.positionIntro = value;
            }
        }
        
        public override void LoadContent()
        {
            base.LoadContent();

            imageIntro = content.Load<Texture2D>(INTRO_IMAGE_PATH);
            positionIntro = new Vector2(HALF_SCREEN_WIDTH - (imageIntro.Width / 2), HALF_SCREEN_HEIGHT - (imageIntro.Height / 2));

        }

        public override void UnloadContent()
        {
            base.UnloadContent();

        }

        public override void Update(GameTime gameTime)
        {
            mouse = Mouse.GetState();

            if (mouse.LeftButton == ButtonState.Pressed)
            {
                if (this.OnClick != null)
                {
                    this.OnClick(this);
                }

                
                ScreenManager.Instance.ChangeScreens("MenuScreen");
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(imageIntro, positionIntro, colorIntro);
        }

        
    }
}
