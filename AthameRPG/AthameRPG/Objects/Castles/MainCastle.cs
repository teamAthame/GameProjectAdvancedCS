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
        protected int switchCounter;
        protected int frameCounter;
        protected int naiSilnaGadinaCount;
        protected static SpriteFont spriteFont;
        protected static SpriteFont spriteFontSmallLetters;
        protected int increaseYforPrintNameOfCreature;
        protected MouseState newMouseState;
        protected MouseState oldMouseState;
        protected int supportCreatureIndex;
        protected int supportButtonRow;
        protected int supportCreatureLevel;

        protected Dictionary<WarUnit, decimal> gadini;
        protected List<WarUnit> supportList;
        protected int[] creatureCounter;

        protected int buttonDifferenceStep;
        protected int minYFirstButton;
        protected int maxYFirstButton;
        protected int minXMinusButton;
        protected int maxXMinusButton;
        protected int minXPlusButton;
        protected int maxXPlusButton;
        protected int minXCheckButton;
        protected int maxXCheckButton;


        protected MainCastle(Vector2 coordinatesOnMap)
        {
            this.coordinatesOnMap = coordinatesOnMap;
            this.gadini = new Dictionary<WarUnit, decimal>();
            this.supportList = new List<WarUnit>();
            this.switchCounter = 100;
            this.frameCounter = 0;
            this.naiSilnaGadinaCount = 0;
            this.increaseYforPrintNameOfCreature = 0;
            this.buttonDifferenceStep = 55;
            this.supportCreatureIndex = 0;
            this.supportButtonRow = 1;
            this.supportCreatureLevel = 0;
            this.minYFirstButton = 7;
            this.maxYFirstButton = 57;
            this.minXMinusButton = 215;
            this.maxXMinusButton = 265;
            this.minXPlusButton = 300;
            this.maxXPlusButton = 350;
            this.minXCheckButton = 360;
            this.maxXCheckButton = 410;

            // index 0- nai silna , 1- sredno silna ..... 
            this.creatureCounter = new int[7];

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
            spriteFont = Content.Load<SpriteFont>("../Content/Fonts/Arial");
            spriteFontSmallLetters = Content.Load<SpriteFont>("../Content/Fonts/SmallLetters");
        }

        public virtual void UnloadContent()
        {

        }

        public virtual void Update(GameTime gameTime)
        {
            this.oldMouseState = this.newMouseState;
            this.newMouseState = Mouse.GetState();
            if (!Character.GetIsInBattle && !Character.GetIsInCastle)
            {
                this.frameCounter += (int) gameTime.ElapsedGameTime.TotalMilliseconds;


                // increase creature population in castles
                if (this.frameCounter >= this.switchCounter)
                {
                    this.frameCounter = 0;

                    this.supportList.Clear();
                    this.supportList = new List<WarUnit>(this.gadini.Keys);

                    foreach (var warUnit in this.supportList)
                    {
                        // when turns is ready ... those values will be increased by turn 

                        if (warUnit.GetStrengthLevel == 7 && this.gadini[warUnit] < 1000)
                        {
                            //this.gadini[warUnit] += 0.00001m;
                            this.gadini[warUnit] += 0.1m;
                        }
                        else if (warUnit.GetStrengthLevel == 6 && this.gadini[warUnit] < 1000)
                        {
                            this.gadini[warUnit] += 0.00005m;
                        }
                        else if (warUnit.GetStrengthLevel == 5 && this.gadini[warUnit] < 1000)
                        {
                            this.gadini[warUnit] += 0.0001m;
                        }
                        else if (warUnit.GetStrengthLevel == 4 && this.gadini[warUnit] < 1000)
                        {
                            this.gadini[warUnit] += 0.0005m;
                        }
                        else if (warUnit.GetStrengthLevel == 3 && this.gadini[warUnit] < 1000)
                        {
                            this.gadini[warUnit] += 0.001m;
                        }
                        else if (warUnit.GetStrengthLevel == 2 && this.gadini[warUnit] < 1000)
                        {
                            this.gadini[warUnit] += 0.005m;
                        }
                        else if (warUnit.GetStrengthLevel == 1 && this.gadini[warUnit] < 1000)
                        {
                            this.gadini[warUnit] += 0.01m;
                        }
                    }
                }

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

                // get in castle

                if (isNearBottomSideOfCastleByX && isNearBottomSideOfCastleByY && mouseIsOverCastleByY &&
                    mouseIsOverCastleByX)
                {
                    if (MouseExtended.Current.WasDoubleClick(MouseButton.Left))
                    {
                        Character.GetIsInCastle = true;

                        // clear creature counter = 0
                        this.creatureCounter = new int[7];
                    }

                }



            }
            else if (Character.GetIsInCastle)
            {
                MouseExtended.Current.GetState(gameTime);

                if (MouseExtended.Current.CurrentState.Position.X > 750 &&
                    MouseExtended.Current.CurrentState.Position.X < 800 &&
                    MouseExtended.Current.CurrentState.Position.Y > 550 &&
                    MouseExtended.Current.CurrentState.Position.Y < 600)
                {
                    // mouse is over exit button
                    this.mouseIsOverReturnButton = true;

                    if (this.newMouseState.LeftButton == ButtonState.Pressed &&
                        this.oldMouseState.LeftButton == ButtonState.Released)
                    {
                        Character.GetIsInCastle = false;
                        // exit from castle
                    }
                }
                else
                {
                    this.mouseIsOverReturnButton = false;
                }

                // buy a creature

                this.supportCreatureIndex = 0;
                this.supportButtonRow = 0;
                this.supportCreatureLevel = 7;
                CheckAndTakActionOnClickedButton(this.supportCreatureLevel, this.supportCreatureIndex,this.minYFirstButton + (this.supportButtonRow * this.buttonDifferenceStep), this.maxYFirstButton + (this.supportButtonRow * this.buttonDifferenceStep), gameTime);

                this.supportCreatureLevel--;
                this.supportCreatureIndex++;
                this.supportButtonRow++;
                CheckAndTakActionOnClickedButton(this.supportCreatureLevel, this.supportCreatureIndex, this.minYFirstButton + (this.supportButtonRow * this.buttonDifferenceStep), this.maxYFirstButton + (this.supportButtonRow * this.buttonDifferenceStep), gameTime);

                this.supportCreatureLevel--;
                this.supportCreatureIndex++;
                this.supportButtonRow++;
                CheckAndTakActionOnClickedButton(this.supportCreatureLevel, this.supportCreatureIndex, this.minYFirstButton + (this.supportButtonRow * this.buttonDifferenceStep), this.maxYFirstButton + (this.supportButtonRow * this.buttonDifferenceStep), gameTime);

                this.supportCreatureLevel--;
                this.supportCreatureIndex++;
                this.supportButtonRow++;
                CheckAndTakActionOnClickedButton(this.supportCreatureLevel, this.supportCreatureIndex, this.minYFirstButton + (this.supportButtonRow * this.buttonDifferenceStep), this.maxYFirstButton + (this.supportButtonRow * this.buttonDifferenceStep), gameTime);

                this.supportCreatureLevel--;
                this.supportCreatureIndex++;
                this.supportButtonRow++;
                CheckAndTakActionOnClickedButton(this.supportCreatureLevel, this.supportCreatureIndex, this.minYFirstButton + (this.supportButtonRow * this.buttonDifferenceStep), this.maxYFirstButton + (this.supportButtonRow * this.buttonDifferenceStep), gameTime);

                this.supportCreatureLevel--;
                this.supportCreatureIndex++;
                this.supportButtonRow++;
                CheckAndTakActionOnClickedButton(this.supportCreatureLevel, this.supportCreatureIndex, this.minYFirstButton + (this.supportButtonRow * this.buttonDifferenceStep), this.maxYFirstButton + (this.supportButtonRow * this.buttonDifferenceStep), gameTime);

                this.supportCreatureLevel--;
                this.supportCreatureIndex++;
                this.supportButtonRow++;
                CheckAndTakActionOnClickedButton(this.supportCreatureLevel, this.supportCreatureIndex, this.minYFirstButton + (this.supportButtonRow * this.buttonDifferenceStep), this.maxYFirstButton + (this.supportButtonRow * this.buttonDifferenceStep), gameTime);
                
            }
            else if (Character.GetIsInBattle)
            {

            }
        }

        private void CheckAndTakActionOnClickedButton(int strengthCreatureLevel, int index,int minY, int maxY, GameTime gameTime)
        {
            MouseExtended.Current.GetState(gameTime);
            

            // if mouse over PLUS button
            if (MouseExtended.Current.CurrentState.Position.X > this.minXPlusButton &&
                MouseExtended.Current.CurrentState.Position.X < this.maxXPlusButton &&
                MouseExtended.Current.CurrentState.Position.Y > minY &&
                MouseExtended.Current.CurrentState.Position.Y < maxY)
            {
                // if click on button
                if (this.newMouseState.LeftButton == ButtonState.Pressed &&
                    this.oldMouseState.LeftButton == ButtonState.Released)
                {
                    // if there are enough creatures 
                    if (this.gadini.FirstOrDefault(x => x.Key.GetStrengthLevel == strengthCreatureLevel).Value - this.creatureCounter[index] >=
                        1)
                    {
                        this.creatureCounter[index]++;

                    }
                }
            }

            //  if mouse is over check/purchase button
            if (MouseExtended.Current.CurrentState.Position.X > this.minXCheckButton &&
                MouseExtended.Current.CurrentState.Position.X < this.maxXCheckButton &&
                MouseExtended.Current.CurrentState.Position.Y > minY &&
                MouseExtended.Current.CurrentState.Position.Y < maxY)
            {
                // purchase a creature
                if (this.newMouseState.LeftButton == ButtonState.Pressed &&
                    this.oldMouseState.LeftButton == ButtonState.Released)
                {
                    for (int i = 0; i < this.creatureCounter[index]; i++)
                    {
                        // add to creature to Player / remove creature from Castle
                        this.RemoveCreature(this.gadini.FirstOrDefault(x => x.Key.GetStrengthLevel == strengthCreatureLevel).Key);
                        CharacterManager.barbarian.AddCreature(
                            this.gadini.FirstOrDefault(x => x.Key.GetStrengthLevel == strengthCreatureLevel).Key);
                    }
                    this.creatureCounter[index] = 0;
                }
            }

            if (MouseExtended.Current.CurrentState.Position.X > this.minXMinusButton &&
                    MouseExtended.Current.CurrentState.Position.X < this.maxXMinusButton &&
                    MouseExtended.Current.CurrentState.Position.Y > minY &&
                    MouseExtended.Current.CurrentState.Position.Y < maxY)
            {
                if (this.newMouseState.LeftButton == ButtonState.Pressed &&
                    this.oldMouseState.LeftButton == ButtonState.Released)
                {
                    if (this.creatureCounter[index] > 0)
                    {
                        this.creatureCounter[index]--;
                    }

                }
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

                spriteBatch.Draw(insideCastle, new Vector2(0, 0), Color.White);

                if (this.mouseIsOverReturnButton)
                {
                    spriteBatch.Draw(MapManager.Instance.Terrain, new Vector2(750, 550), new Rectangle(250, 0, 50, 50),
                        Color.Red);
                }
                else
                {
                    spriteBatch.Draw(MapManager.Instance.Terrain, new Vector2(750, 550), new Rectangle(250, 0, 50, 50),
                        Color.White);
                }

                this.increaseYforPrintNameOfCreature = 20;
                this.supportCreatureIndex = 0;
                // print available creatures
                foreach (var gadinki in this.gadini)
                {
                    spriteBatch.DrawString(spriteFont,
                        gadinki.Key.GetType().Name + " : " +
                        (gadinki.Value - this.creatureCounter[this.supportCreatureIndex]).ToString(),
                        new Vector2(5, this.increaseYforPrintNameOfCreature), Color.White);
                    this.increaseYforPrintNameOfCreature += this.buttonDifferenceStep;
                    this.supportCreatureIndex++;
                }

                this.increaseYforPrintNameOfCreature = 20;
                // print purchase creatures
                foreach (var index in this.creatureCounter)
                {
                    spriteBatch.DrawString(spriteFont, index.ToString(),
                        new Vector2(280, this.increaseYforPrintNameOfCreature), Color.Yellow);
                    this.increaseYforPrintNameOfCreature += this.buttonDifferenceStep;
                }

                //print buttons
                this.increaseYforPrintNameOfCreature = 7;
                for (int i = 0; i < 7; i++)
                {
                    spriteBatch.Draw(MapManager.Instance.Terrain, new Vector2(360, this.increaseYforPrintNameOfCreature),
                        new Rectangle(250, 48, 50, 50), Color.White);
                    spriteBatch.Draw(MapManager.Instance.Terrain, new Vector2(300, this.increaseYforPrintNameOfCreature),
                        new Rectangle(250, 98, 50, 50), Color.Yellow);
                    spriteBatch.Draw(MapManager.Instance.Terrain, new Vector2(215, this.increaseYforPrintNameOfCreature),
                        new Rectangle(250, 148, 50, 50), Color.Gray);
                    this.increaseYforPrintNameOfCreature += this.buttonDifferenceStep;
                }

                // print our army
                spriteBatch.DrawString(spriteFontSmallLetters, "Our Army:",
                        new Vector2(15, 400), Color.White);

                this.increaseYforPrintNameOfCreature = 430;
                foreach (var creatures in CharacterManager.barbarian.availableCreatures)
                {
                    spriteBatch.DrawString(spriteFontSmallLetters, creatures.Key.GetType().Name + ": " + creatures.Value.ToString(),
                        new Vector2(5, this.increaseYforPrintNameOfCreature), Color.White);
                    this.increaseYforPrintNameOfCreature += 20;
                }

                
            }
            else if (Character.GetIsInBattle)
            {

            }
        }

        protected void RemoveCreature(WarUnit warUnit)
        {
            this.gadini[warUnit]--;
        }

        protected void AddCreature(WarUnit warUnit)
        {
            this.gadini[warUnit]++;
        }
    }
}
