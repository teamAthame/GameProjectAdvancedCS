﻿using AthameRPG.Controls;
using AthameRPG.GameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AthameRPG.Characters.Heroes
{
    public abstract class Character : Unit
    {
        /// <summary>
        /// Unit is  abstract class for ALL GOOD PLAYERS
        /// </summary>

        protected static int cropWidth;
        protected static int cropHeight;
        //protected static bool isInCastle;
        //protected static bool isInBattle;
        
        protected float playerCenterCoordX;
        protected float playerCenterCoordY;
        protected float moveSpeedPlayer;
        protected int cropStay;
        protected int north;
        protected int south;
        protected int east;
        protected int west;
        protected int northEast;
        protected int northWest;
        protected int southEast;
        protected int southWest;

        protected Vector2 abstractPlayerPositon;
        protected static Texture2D playerImage;
        protected MouseState mouse;
        protected Vector2 lastMouseClickPosition;
        protected MouseState newMouseState;
        protected MouseState oldMouseState;
        protected static SpriteFont spriteFontSmallLetters;
        //protected double defaultPlayerMove;

        protected static Vector2 drawCoordPlayer;

        public Character(float startPositionX, float startPositionY, int atack, int health, int defence)
            : base(startPositionX, startPositionY, atack, health, defence)
        {
            drawCoordPlayer = new Vector2(startPositionX - cropWidth/2, startPositionY - cropHeight/2);
            this.lastMouseClickPosition = new Vector2(startPositionX - cropWidth / 2, startPositionY - cropHeight / 2);
            this.cropCurrentFrame = new Rectangle(this.cropStay, this.south, cropWidth, cropHeight);
            this.CropCurrentFrame = this.cropCurrentFrame;
            
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            spriteFontSmallLetters = content.Load<SpriteFont>("../Content/Fonts/SmallLetters");
            
        }
        
        public override void Update(GameTime gameTime)
        {
            //base.Update(gameTime);

            this.oldMouseState = this.newMouseState;
            this.newMouseState = Mouse.GetState();
            this.mouse = Mouse.GetState();

            if (!isInCastle && !isInBattle)
            {
                this.lastAbstractCoord = this.abstractPlayerPositon;

                
                // take position where need to go
                //this.mouse.LeftButton == ButtonState.Pressed - for single click movement
                if (MouseExtended.Current.WasDoubleClick(MouseButton.Left))
                {
                    this.lastMouseClickPosition.X = this.mouse.X;
                    this.lastMouseClickPosition.Y = this.mouse.Y;
                }

                //MovingWithMouse();

                if (this.availableMove > 0)
                {
                    CharacterMoving();

                    // re-calculate player available move
                    this.availableMove -= CollisionDetection.CalculateDistanceTravelled(this.lastAbstractCoord,
                        this.abstractPlayerPositon);
                }

                MakeCurrentAnimationFrame(gameTime);
            }
            else if (isInCastle)
            {

                // if EXIT is clicked
                //if (true)
                //{
                //    isInCastle = false;
                //}
            }
            else if (isInBattle)
            {
                // when have a winner .. continue is clicked
                //if (true)
                //{
                //    isInBattle = false;
                //}
            }

        }

        private void MakeCurrentAnimationFrame(GameTime gameTime)
        {
            /// take animation direction 

            this.frameCounter += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (this.frameCounter >= this.switchCounter)
            {
                this.frameCounter = 0;

                this.returnedValue = Animation.SpriteSheetAnimation(this.lastAbstractCoord,
                    this.abstractPlayerPositon,
                    this.direction, this.cropFrame, cropWidth, cropHeight, this.cropStay, this.north, this.south, this.east, this.west,
                    this.northEast,
                    this.northWest, this.southEast, this.southWest);

                this.cropCurrentFrame = this.returnedValue.ImageCrop;
                this.direction = this.returnedValue.Direction;

                this.cropFrame++;

                if (this.cropFrame == 4)
                {
                    this.cropFrame = 0;
                }
            }
        }

        private void CharacterMoving()
        {
            if (this.lastMouseClickPosition.Y < drawCoordPlayer.Y)
            {
                this.abstractPlayerPositon.Y += CollisionDetection.GoUp(this.abstractPlayerPositon,
                    moveSpeedPlayer,
                    drawCoordPlayer, cropWidth, cropHeight);
                this.lastMouseClickPosition.Y += moveSpeedPlayer;
            }

            if (this.lastMouseClickPosition.Y > drawCoordPlayer.Y + cropHeight)
            {
                this.abstractPlayerPositon.Y -= CollisionDetection.GoDown(this.abstractPlayerPositon,
                    moveSpeedPlayer,
                    drawCoordPlayer, cropWidth, cropHeight);
                this.lastMouseClickPosition.Y -= moveSpeedPlayer;
            }

            if (this.lastMouseClickPosition.X < drawCoordPlayer.X)
            {
                this.abstractPlayerPositon.X += CollisionDetection.GoLeft(this.abstractPlayerPositon,
                    moveSpeedPlayer,
                    drawCoordPlayer, cropWidth, cropHeight);
                this.lastMouseClickPosition.X += moveSpeedPlayer;
            }

            if (this.lastMouseClickPosition.X > drawCoordPlayer.X + cropWidth)
            {
                this.abstractPlayerPositon.X -= CollisionDetection.GoRight(abstractPlayerPositon,
                    moveSpeedPlayer,
                    drawCoordPlayer, cropWidth, cropHeight);
                this.lastMouseClickPosition.X -= moveSpeedPlayer;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!isInBattle && !isInCastle)
            {
                spriteBatch.Draw(playerImage, drawCoordPlayer, this.CropCurrentFrame, Color.White);
                spriteBatch.DrawString(spriteFontSmallLetters, "Available steps:" + $"{this.availableMove:F0}",
                        new Vector2(5, 5), Color.GreenYellow);
            }
            else if (isInCastle)
            {

            }
            else if (isInBattle)
            {

            }
            
        }
        
        public Vector2 CoordP()
        {
            return this.abstractPlayerPositon;
        }

        public static Texture2D PlayerImage
        {
            get { return playerImage; }
        }

        public static Vector2 DrawCoordPlayer
        {
            get { return drawCoordPlayer; }
        }

        public static int PlayerCropWidth
        {
            get { return cropWidth; }
        }

        public static int PlayerCropHeight
        {
            get { return cropHeight; }
        }

        public float PlayerCenterCoordX
        {
            get { return this.playerCenterCoordX; }
            private set { this.playerCenterCoordX = value; }
        }

        public float PlayerCenterCoordY
        {
            get { return this.playerCenterCoordY; }
            private set { this.playerCenterCoordY = value; }
        }

        public static bool GetIsInCastle
        {
            get { return isInCastle; }
            set { isInCastle = value; }
        }

        public static bool GetIsInBattle
        {
            get { return isInBattle; }
            set { isInBattle = value; }
        }
    }
}