using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        

        public ScreenManager()
        {
            Dimension = new Vector2(SCREEN_WIDTH, SCREEN_HEIGHT);
            currentScreen = new MenuScreen();
        }

        public ContentManager Content { get; private set; }
        public Vector2 Dimension { get; private set; }
        GameScreen currentScreen;

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
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            currentScreen.Draw(spriteBatch);
        }
    }
}
