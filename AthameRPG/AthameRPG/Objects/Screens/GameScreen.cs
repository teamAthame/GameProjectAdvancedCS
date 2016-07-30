using AthameRPG.Attributes.Behavior;
using AthameRPG.Contracts;
using AthameRPG.Enums;
using AthameRPG.GameEngine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AthameRPG.Objects.Screens
{
    [ClickableObject]
    public abstract  class GameScreen : ISoundable
    {
        public abstract event OnEvent OnEvent;

        protected ContentManager content;

        protected GameScreen()
        {
            this.SoundStatus = SoundStatus.Click;
        }

        public virtual void LoadContent()
        {
            content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");
            
        }
        public virtual void UnloadContent()
        {
            content.Unload();
            
        }
        public virtual void Update(GameTime gameTime)
        {
            
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            
        }

        public SoundStatus SoundStatus { get; protected set; }
        
    }
}
