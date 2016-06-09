using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthameRPG.GameEngine.Screens
{
    class IntroScreen : GameScreen
    {
        private const float HALF_SCREEN_WIDTH = ScreenManager.SCREEN_WIDTH / 2;
        private const float HALF_SCREEN_HEIGHT = ScreenManager.SCREEN_HEIGHT / 2;
        private const string INTRO_IMAGE_PATH =  @"../Content/Image/AthameSplashScreen";

        private Color colorIntro = Color.White;
        private Texture2D imageIntro;
        private Vector2 positionIntro;
        private MouseState mouse;

        public IntroScreen()
        {
            imageIntro = imageIntro;
            positionIntro = positionIntro;

        }

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
                ScreenManager.Instance.ChangeScreens("MenuScreen");
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(imageIntro, positionIntro, colorIntro);
        }

    }
}
