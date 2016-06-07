

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AthameRPG.Characters
{
    public abstract class Unit 
    {

        private float startPositionX, startPositionY;

        public Unit(float startPositionX, float startPositionY)
        {
            this.StartPositionX = startPositionX;
            this.StartPositionY = startPositionY;
        }

        private float StartPositionX
        {
            get
            {
                return this.startPositionX;
            }
            set
            {
                this.startPositionX = value;
            }
        }

        private float StartPositionY
        {
            get
            {
                return this.startPositionY;
            }
            set
            {
                this.startPositionY = value;
            }
        }
        public abstract void LoadContent();

        public abstract void UnloadContent();

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch);
        
    }
}
