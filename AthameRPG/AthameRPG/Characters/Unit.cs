

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AthameRPG.Characters
{
    public abstract class Unit 
    {
        private bool isAlive;

        private float startPositionX, startPositionY;

        public Unit(float startPositionX, float startPositionY)
        {
            this.StartPositionX = startPositionX;
            this.StartPositionY = startPositionY;
            this.IsAlive = true;
        }

        public bool IsAlive
        {
            get
            {
                return this.isAlive;
            }
            private set
            {
                this.isAlive = value;
            }
        }
        public void KillTarget()
        {
            IsAlive = false;
        }

        public float StartPositionX
        {
            get
            {
                return this.startPositionX;
            }
            private set
            {
                this.startPositionX = value;
            }
        }

        public float StartPositionY
        {
            get
            {
                return this.startPositionY;
            }
            private set
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
