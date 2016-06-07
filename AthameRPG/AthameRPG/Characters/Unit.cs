using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using AthameRPG.GameEngine;

namespace AthameRPG.Characters
{
    public abstract class Unit
    {
        /// <summary>
        /// Unit is  abstract class for ALL GOOD PLAYERS
        /// </summary>

        
        public Unit()
        {

            
        }

        public virtual void LoadContent()
        {

        }
        public virtual void UnloadContent()
        {

        }

        public virtual void Update(GameTime gameTime)
        {
            
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            
        }
    }
}
