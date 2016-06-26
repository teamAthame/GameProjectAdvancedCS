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
        protected bool mouseIsOverReturnButton;
        protected static Texture2D imageOfCastleOutside;
        protected const string ImageOfCastleOutsidePath = @"../Content/Obstacles/castles";
        protected string insideFirstCastlePath;
        protected Rectangle cropCastle;
        protected Vector2 coordinatesOnMap;
        protected Vector2 drawCoordinates;
        protected int cropCurrentCastleImageWidth;
        protected int cropCurrentCastleImageHeight;
        protected static Texture2D insideCastle;

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
                MouseExtended.Current.GetState(gameTime);
                if (MouseExtended.Current.CurrentState.Position.X > 750 && MouseExtended.Current.CurrentState.Position.X < 800 && MouseExtended.Current.CurrentState.Position.Y > 550 && MouseExtended.Current.CurrentState.Position.Y < 600)
                {
                    this.mouseIsOverReturnButton = true;

                    if (MouseExtended.Current.WasSingleClick(MouseButton.Left))
                    {
                        Character.GetIsInCastle = false;

                    }
                }
                else
                {
                    this.mouseIsOverReturnButton = false;
                }
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

                if (this.mouseIsOverReturnButton)
                {
                    spriteBatch.Draw(MapManager.Instance.Terrain, new Vector2(750, 550), new Rectangle(250, 0, 50, 50), Color.Red);
                }
                else
                {
                    spriteBatch.Draw(MapManager.Instance.Terrain, new Vector2(750, 550), new Rectangle(250, 0, 50, 50), Color.White);
                }
                
            }
            else if (Character.GetIsInBattle)
            {

            }
        }
        

    }
}
