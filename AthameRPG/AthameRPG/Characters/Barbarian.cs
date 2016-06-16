using AthameRPG.GameEngine;
using AthameRPG.GameEngine.Maps;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AthameRPG.Characters
{
    using Atack;
    using AthameRPG.Controls;

    public class Barbarian : Character
    {
        private const float moveSpeedPlayer = 3f;

        private float playerCenterCoordX;
        private float playerCenterCoordY;
        public Vector2 abstractPlayerPositon;

        private const int DefaultBarbarianAttackPoints = 120;
        private const int DefaultBarbarianHealthPoints = 180;
        private const int DefaultBarbarianDefencePoints = 70;

        // Animation values // Pixels on .png where starts current row
        private const int cropStay = 360;
        private const int north = 395;
        private const int south = 20;
        private const int east = 580;
        private const int west = 210;
        private const int northEast = 485;
        private const int northWest = 300;
        private const int southEast = 675;
        private const int southWest = 120;

        //private Texture2D playerImage;
        private const string PATH_BARBARIAN_IMAGE = @"../Content/Character/HexenFighter";
        
        private MouseState mouse;
        private Vector2 lastMouseClickPosition;
        protected ContentManager content;
        
        /// proba 
        //private SpriteFont font;


        public Barbarian(float startPositionX, float startPositionY) 
            : base(startPositionX, startPositionY, DefaultBarbarianAttackPoints, DefaultBarbarianHealthPoints, 
                  DefaultBarbarianDefencePoints)
        {
            //this.CropCurrentFrame = cropCurrentFrame;
            //this.AtackHandler =new AtackBarbarian();
        }
       
        public float PlayerCenterCoordX
        {
            get
            {
                return this.playerCenterCoordX;
            }
            private set
            {
                this.playerCenterCoordX = value;
            }
        }

        public float PlayerCenterCoordY
        {
            get
            {
                return this.playerCenterCoordY;
            }
            private set
            {
                this.playerCenterCoordY = value;
            }
        }
        
        public override void LoadContent()
        {

            // komplekt ! :)
            content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");
            playerImage = content.Load<Texture2D>(PATH_BARBARIAN_IMAGE);

            //font = content.Load<SpriteFont>("../Content/Fonts/ArialBig");
        }

        public override void UnloadContent()
        {

        }

        public Vector2 CoordP()
        {
            return abstractPlayerPositon;
        }

        public override void Update(GameTime gameTime)
        {
            mouse = Mouse.GetState();
            lastAbstractCoord = abstractPlayerPositon;

            //ENEMY CROP PICTURE IS BIGGER THAN ACTUAL WHAT WE SEE !!! 

            //#1#

            //MovingWithMouse();
            // mouse.LeftButton == ButtonState.Pressed

            // take position where need to go
            if (mouse.LeftButton == ButtonState.Pressed)
            {
                lastMouseClickPosition.X = mouse.X;
                lastMouseClickPosition.Y = mouse.Y;
            }
            
            if (lastMouseClickPosition.Y < drawCoordPlayer.Y)
            {
                abstractPlayerPositon.Y += CollisionDetection.GoUp(abstractPlayerPositon, moveSpeedPlayer, drawCoordPlayer, cropWidth, cropHeight);
                lastMouseClickPosition.Y += moveSpeedPlayer;
            }

            if (lastMouseClickPosition.Y > drawCoordPlayer.Y + cropHeight)
            {
                abstractPlayerPositon.Y -= CollisionDetection.GoDown(abstractPlayerPositon, moveSpeedPlayer, drawCoordPlayer, cropWidth, cropHeight);
                lastMouseClickPosition.Y -= moveSpeedPlayer;
            }

            if (lastMouseClickPosition.X < drawCoordPlayer.X)
            {
                abstractPlayerPositon.X += CollisionDetection.GoLeft(abstractPlayerPositon, moveSpeedPlayer, drawCoordPlayer, cropWidth, cropHeight);
                lastMouseClickPosition.X += moveSpeedPlayer;
            }

            if (lastMouseClickPosition.X > drawCoordPlayer.X + cropWidth)
            {
                abstractPlayerPositon.X -= CollisionDetection.GoRight(abstractPlayerPositon, moveSpeedPlayer, drawCoordPlayer, cropWidth, cropHeight);
                lastMouseClickPosition.X -= moveSpeedPlayer;
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

            frameCounter += (int) gameTime.ElapsedGameTime.TotalMilliseconds;

            if (frameCounter >= switchCounter)
            {
                frameCounter = 0;
                
                returnedValue = Animation.SpriteSheetAnimation(lastAbstractCoord, abstractPlayerPositon,
                    direction, cropFrame, cropWidth, cropHeight, cropStay, north, south, east, west, northEast,
                    northWest, southEast, southWest);

                cropCurrentFrame = returnedValue.ImageCrop;
                direction = returnedValue.Direction;

                cropFrame++;
                
                if (cropFrame == 4)
                {
                    cropFrame = 0;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(playerImage, drawCoordPlayer, CropCurrentFrame, Color.White);


            // for TEST
            //spriteBatch.DrawString(font, playerPositon.X + " " + playerPositon.Y, new Vector2(30, 30), Color.Blue);
            //spriteBatch.DrawString(font, CharacterManager.enemiesList[0].GARGAcoor.X + " " + CharacterManager.enemiesList[0].GARGAcoor.Y, new Vector2(30, 70), Color.AliceBlue);
            //spriteBatch.DrawString(font, CharacterManager.enemiesList[0].GARGA.X + " " + CharacterManager.enemiesList[0].GARGA.Y, new Vector2(30, 110), Color.AliceBlue);
        }    

        
    }



}
    
    
    
    

