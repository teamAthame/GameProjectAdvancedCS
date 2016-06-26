using System;
using System.Collections.Generic;
using System.Linq;
using AthameRPG.Characters;
using AthameRPG.Characters.WarUnits;
using AthameRPG.Controls;
using AthameRPG.GameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AthameRPG.Objects.Castles
{
    public abstract class MainCastle
    {
        protected static Texture2D imageOfCastleOutside;
        protected const string ImageOfCastleOutsidePath = @"../Content/Obstacles/castles";
        protected string insideFirstCastlePath;
        protected Rectangle cropCastle;
        protected Vector2 coordinatesOnMap;
        protected Vector2 drawCoordinates;
        protected int cropCurrentCastleImageWidth;
        protected int cropCurrentCastleImageHeight;
        protected static Texture2D insideCastle;
        protected MouseState mouse;

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
            insideCastle = Content.Load<Texture2D>(insideFirstCastlePath);
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

                this.mouse = Mouse.GetState();
                MouseExtended.Current.GetState(gameTime);

                bool isNearBottomSideOfCastleByX = (this.drawCoordinates.X + (this.cropCurrentCastleImageWidth/2)) > 375 &&
                                                   (this.drawCoordinates.X + (this.cropCurrentCastleImageWidth/2)) < 425;

                bool isNearBottomSideOfCastleByY = (Character.DrawCoordPlayer.Y -
                                                    (this.drawCoordinates.Y + this.cropCurrentCastleImageHeight)) < 5;

                bool mouseIsOverCastleByX = MouseExtended.Current.CurrentState.Position.X > this.drawCoordinates.X &&
                                            MouseExtended.Current.CurrentState.Position.X <
                                            this.drawCoordinates.X + this.cropCurrentCastleImageWidth;

                bool mouseIsOverCastleByY = MouseExtended.Current.CurrentState.Position.Y > this.drawCoordinates.Y &&
                                            MouseExtended.Current.CurrentState.Position.Y <
                                            this.drawCoordinates.Y + this.cropCurrentCastleImageHeight;

                if (isNearBottomSideOfCastleByX && isNearBottomSideOfCastleByY && mouseIsOverCastleByY && mouseIsOverCastleByX)
                {
                    if (MouseExtended.Current.WasDoubleClick(MouseButton.Left))
                    {
                        Character.GetIsInCastle = true;

                    }
                }
                

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
                spriteBatch.Draw(insideCastle, new Vector2(0,0), Color.White);
            }
            else if (Character.GetIsInBattle)
            {

            }
        }
        

    }
}
