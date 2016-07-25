using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using AthameRPG.GameEngine.Screens;

namespace AthameRPG.GameEngine
{
    public class ScreenManager
    {
        public const float SCREEN_WIDTH = 800f;
        public const float SCREEN_HEIGHT = 600f;

        private static ScreenManager instance;

        GameScreen currentScreen;
        GameScreen newScreen;
        
        public ScreenManager()
        {
            Dimension = new Vector2(SCREEN_WIDTH, SCREEN_HEIGHT);
            currentScreen = new IntroScreen();
        }

        public bool IsTransitioning { get; private set; }
        public ContentManager Content { get; private set; }
        public Vector2 Dimension { get; private set; }

        public static ScreenManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ScreenManager();
                }

                return instance;
            }
        }

        public GameScreen CurrentScreen
        {
            get { return this.currentScreen; }
        }

        public void ChangeScreens(string screenName)
        {
            newScreen = (GameScreen)Activator.CreateInstance(Type.GetType("AthameRPG.GameEngine.Screens." + screenName));

            IsTransitioning = true;
        }
        private void Transition()
        {
            if (IsTransitioning)
            {
                currentScreen.UnloadContent();
                currentScreen = newScreen;
                currentScreen.LoadContent();
                IsTransitioning = false;
            }
        }

        public void LoadContent(ContentManager Content)
        {
            this.Content = new ContentManager(Content.ServiceProvider, "Content");
            currentScreen.LoadContent();
        }

        public void UnloadContent()
        {
            currentScreen.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            currentScreen.Update(gameTime);
            Transition();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            currentScreen.Draw(spriteBatch);
        }
    }
}
