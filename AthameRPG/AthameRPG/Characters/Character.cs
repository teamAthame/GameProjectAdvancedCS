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
    public abstract class Character : Unit
    {
        /// <summary>
        /// Unit is  abstract class for ALL GOOD PLAYERS
        /// </summary>

        protected const int cropWidth = 70;

        protected const int cropHeight = 80;

        protected float playerCenterCoordX;
        protected float playerCenterCoordY;
        protected const float moveSpeedPlayer = 3f;
        protected int cropStay;
        protected int north;
        protected int south;
        protected int east;
        protected int west;
        protected int northEast;
        protected int northWest;
        protected int southEast;
        protected int southWest;

        public Vector2 abstractPlayerPositon;
        protected static Texture2D playerImage;
        protected MouseState mouse;
        protected Vector2 lastMouseClickPosition;

        protected static Vector2 drawCoordPlayer;

        public Character(float startPositionX, float startPositionY, int atack, int health, int defence)
            : base(startPositionX, startPositionY, atack, health, defence)
        {
            drawCoordPlayer = new Vector2(startPositionX - cropWidth/2, startPositionY - cropHeight/2);
            this.CropCurrentFrame = cropCurrentFrame;
        }

        public override void Update(GameTime gameTime)
        {
            this.mouse = Mouse.GetState();
            this.lastAbstractCoord = this.abstractPlayerPositon;

            //ENEMY CROP PICTURE IS BIGGER THAN ACTUAL WHAT WE SEE !!! 

            //#1#

            //MovingWithMouse();
            // mouse.LeftButton == ButtonState.Pressed

            // take position where need to go
            if (this.mouse.LeftButton == ButtonState.Pressed)
            {
                this.lastMouseClickPosition.X = this.mouse.X;
                this.lastMouseClickPosition.Y = this.mouse.Y;
            }

            if (this.lastMouseClickPosition.Y < drawCoordPlayer.Y)
            {
                this.abstractPlayerPositon.Y += CollisionDetection.GoUp(this.abstractPlayerPositon, moveSpeedPlayer,
                    drawCoordPlayer, cropWidth, cropHeight);
                this.lastMouseClickPosition.Y += moveSpeedPlayer;
            }

            if (this.lastMouseClickPosition.Y > drawCoordPlayer.Y + cropHeight)
            {
                this.abstractPlayerPositon.Y -= CollisionDetection.GoDown(this.abstractPlayerPositon, moveSpeedPlayer,
                    drawCoordPlayer, cropWidth, cropHeight);
                this.lastMouseClickPosition.Y -= moveSpeedPlayer;
            }

            if (this.lastMouseClickPosition.X < drawCoordPlayer.X)
            {
                this.abstractPlayerPositon.X += CollisionDetection.GoLeft(this.abstractPlayerPositon, moveSpeedPlayer,
                    drawCoordPlayer, cropWidth, cropHeight);
                this.lastMouseClickPosition.X += moveSpeedPlayer;
            }

            if (this.lastMouseClickPosition.X > drawCoordPlayer.X + cropWidth)
            {
                this.abstractPlayerPositon.X -= CollisionDetection.GoRight(abstractPlayerPositon, moveSpeedPlayer,
                    drawCoordPlayer, cropWidth, cropHeight);
                this.lastMouseClickPosition.X -= moveSpeedPlayer;
            }

            /*
            //// -------------- Double Click Movement -------------
            //KeyboardExtended.Current.GetState(gameTime);
            //MouseExtended.Current.GetState(gameTime);

            //if (MouseExtended.Current.WasDoubleClick(MouseButton.Left))
            //{
            //    lastMouseClickPosition.X = mouse.X - cropWidth / 2;
            //    lastMouseClickPosition.Y = mouse.Y - cropHeight / 2;

            //    if (lastMouseClickPosition.Y < coordPlayer.Y)
            //    {
            //        playerPositon.Y += GoUp();
            //    }
            //    if (lastMouseClickPosition.Y > coordPlayer.Y)
            //    {
            //        playerPositon.Y -= GoDown();
            //    }

            //    if (lastMouseClickPosition.X < coordPlayer.X)
            //    {
            //        playerPositon.X += GoLeft();
            //    }

            //    if (lastMouseClickPosition.X > coordPlayer.X)
            //    {
            //        playerPositon.X -= GoRight();
            //    }
            //}
            */

            /// take animation direction 
            /// mostra --- CropCurrentFramePlayer = new Rectangle(0, 0, cropWidth, cropHeight);
            /// 

            this.frameCounter += (int) gameTime.ElapsedGameTime.TotalMilliseconds;

            if (this.frameCounter >= this.switchCounter)
            {
                this.frameCounter = 0;

                this.returnedValue = Animation.SpriteSheetAnimation(this.lastAbstractCoord, this.abstractPlayerPositon,
                    this.direction, this.cropFrame, cropWidth, cropHeight, cropStay, north, south, east, west, northEast,
                    northWest, southEast, southWest);

                this.cropCurrentFrame = this.returnedValue.ImageCrop;
                this.direction = this.returnedValue.Direction;

                this.cropFrame++;

                if (this.cropFrame == 4)
                {
                    this.cropFrame = 0;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(playerImage, drawCoordPlayer, this.CropCurrentFrame, Color.White);
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
    }
}
