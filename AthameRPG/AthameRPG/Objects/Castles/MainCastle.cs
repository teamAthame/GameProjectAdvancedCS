using System;
using System.Collections.Generic;
using System.Linq;
using AthameRPG.Characters;
using AthameRPG.Characters.WarUnits;
using AthameRPG.GameEngine;
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
            if (!Character.GetIsInBattle && !Character.GetIsInCastle)
            {
                this.drawCoordinates.X = this.coordinatesOnMap.X + CharacterManager.barbarian.CoordP().X;
                this.drawCoordinates.Y = this.coordinatesOnMap.Y + CharacterManager.barbarian.CoordP().Y;
            }
            else if (Character.GetIsInCastle)
            {

            }
            else if (Character.GetIsInBattle)
            {

            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (!Character.GetIsInBattle && !Character.GetIsInCastle)
            {
                spriteBatch.Draw(imageOfCastleOutside, this.drawCoordinates, this.cropCastle, Color.White);
            }
            else if (Character.GetIsInCastle)
            {

            }
            else if (Character.GetIsInBattle)
            {

            }
        }
        

    }
}
