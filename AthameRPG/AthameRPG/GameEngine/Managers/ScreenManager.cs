using System;
using AthameRPG.Objects.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AthameRPG.GameEngine.Managers
{
    public class ScreenManager
    {
        public const float SCREEN_WIDTH = 800f;
        public const float SCREEN_HEIGHT = 600f;

        private static ScreenManager instance;
        private SoundEffectManager soundEffectManager;

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

        public SoundEffectManager SoundEffectManager
        {
            get { return this.soundEffectManager; }
        }

        public GameScreen CurrentScreen
        {
            get { return this.currentScreen; }
        }

        public void ChangeScreens(string screenName)
        {
            newScreen = (GameScreen)Activator.CreateInstance(Type.GetType("AthameRPG.Objects.Screens." + screenName));

            IsTransitioning = true;
        }

        private void Transition()
        {
            if (IsTransitioning)
            {
                currentScreen.UnloadContent();
                currentScreen = newScreen;
                currentScreen.LoadContent();
                this.currentScreen.OnEvent += this.soundEffectManager.ExecuteQuery;
                IsTransitioning = false;
            }
        }

        public void LoadContent(ContentManager Content)
        {
            this.Content = new ContentManager(Content.ServiceProvider, "Content");
            currentScreen.LoadContent();

            this.soundEffectManager = new SoundEffectManager(this.Content);
            this.currentScreen.OnEvent += this.soundEffectManager.ExecuteQuery;
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
