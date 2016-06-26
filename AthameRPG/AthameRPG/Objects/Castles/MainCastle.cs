using System;
using System.Collections.Generic;
using System.Linq;
using AthameRPG.Characters.WarUnits;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AthameRPG.Objects.Castles
{
    public abstract class MainCastle
    {
        protected static Texture2D imageOfCastleOutside;
        protected const string ImageOfCastleOutsidePath = @"../Content/Obstacles/castles";
        protected Rectangle cropCastle;
        protected Vector2 coordinatesOnMap;
        protected Vector2 drawCoordinates;

        protected Dictionary<WarUnit, decimal> gadini;
        
        protected MainCastle(Vector2 coordinatesOnMap)
        {
            this.coordinatesOnMap = coordinatesOnMap;
            this.gadini = new Dictionary<WarUnit, decimal>();
            
        }
        
        public Texture2D ImageOfCastle
        {
            get { return imageOfCastleOutside; }
        }

        public virtual void LoadContent(ContentManager Content)
        {
            Content = new ContentManager(Content.ServiceProvider, "Content");
            imageOfCastleOutside = Content.Load<Texture2D>(ImageOfCastleOutsidePath);
            
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
