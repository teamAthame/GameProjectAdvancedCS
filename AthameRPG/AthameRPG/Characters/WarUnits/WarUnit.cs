using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AthameRPG.Characters.WarUnits
{
    public abstract class WarUnit
    {
        protected int strengthLevel;
        protected Texture2D warUnitImage;
        protected string imagePath;
        protected Vector2 warUnitDrawCoord;
        protected Rectangle cropCurrentFrame;
        protected SpriteEffects warUnitEffect;

        public virtual void LoadContent(ContentManager content)
        {
            this.warUnitImage = content.Load<Texture2D>(this.imagePath);
             
        }

        public int GetStrengthLevel
        {
            get {return this.strengthLevel; }
        }
    }
}
