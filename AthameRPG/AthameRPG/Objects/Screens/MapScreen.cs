using AthameRPG.GameEngine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AthameRPG.Objects.Screens
{
    public class MapScreen : GameScreen
    {
        private Vector2 newScreenPosition;
        
        public MapScreen()
        {
            this.NewScreenPosition = newScreenPosition;
        }
        
        public Vector2 NewScreenPosition
        {
            get
            {
                return this.newScreenPosition;
            }
            private set
            {
                this.newScreenPosition = Vector2.Zero;
            }
        }

        public override void LoadContent()
        {
            base.LoadContent();
            
            MapManager.Instance.LoadContent(content);
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

            MapManager.Instance.Draw(spriteBatch);
        }
    }
}
