using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AthameRPG.GameEngine.Screens
{
    public class MapScreen : GameScreen
    {

        private string newScreen;
        //private SpriteFont spriteFont;
        private Vector2 newScreenPosition;
        
        public MapScreen()
        {
            this.NewScreen = newScreen;
            this.NewScreenPosition = newScreenPosition;
            
        }


        // this was only for test
        public string NewScreen
        {
            get
            {
                return this.newScreen;
            }
            private set
            {
                this.newScreen = "VOALA";
            }
        }

        public Vector2 NewScreenPosition
        {
            get
            {
                return this.newScreenPosition;
            }
            private set
            {
                this.newScreenPosition = new Vector2(0, 0);
            }
        }

        public override void LoadContent()
        {
            base.LoadContent();

            // show coord on the screen -- test
            //spriteFont = content.Load<SpriteFont>("../Content/Fonts/ArialBig");

            MapManager.Instance.LoadContent(content);
            //CharacterManager.Instance.LoadContent(content);

        }
        public override void UnloadContent()
        {
            base.UnloadContent();
            MapManager.Instance.UnloadContent();
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            MapManager.Instance.Update(gameTime);
            
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            //spriteBatch.DrawString(spriteFont, NewScreen, NewScreenPosition, Color.Green);
            MapManager.Instance.Draw(spriteBatch);
        }
    }
}
