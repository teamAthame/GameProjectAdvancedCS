using AthameRPG.GameEngine;
using AthameRPG.GameEngine.Maps;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthameRPG.Characters
{
    using AthameRPG.Controls;

    public class Barbarian : Character
    {
        private const float moveSpeedPlayer = 2f;

        private float playerCenterCoordX;
        private float playerCenterCoordY;
        
        private Rectangle cropCurrentFramePlayer;

        public Vector2 abstractPlayerPositon;

        private Vector2 lastMouseClickPosition;

        
        /// proba 
        //private SpriteFont font;
        //protected ContentManager content;

        private MouseState mouse;

        public Barbarian(float startPositionX, float startPositionY) : base(startPositionX, startPositionY)
        {
            this.CropCurrentFramePlayer = cropCurrentFramePlayer;
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

        public Rectangle CropCurrentFramePlayer
        {
            get
            {
                return this.cropCurrentFramePlayer;
            }
            private set
            {
                this.cropCurrentFramePlayer = new Rectangle(0, 0, cropWidth, cropHeight);
            }
        }

        public override void LoadContent()
        {

            // komplekt ! :)
            //content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");
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

            //MovingWithMouse();
            if (mouse.LeftButton == ButtonState.Pressed)
            {
                // take position where need to go
                lastMouseClickPosition.X = mouse.X - cropWidth / 2;
                lastMouseClickPosition.Y = mouse.Y - cropHeight / 2;
                
                //ENEMY CROP PICTURE IS BIGGER THAN ACTUAL WHAT WE SEE !!! 

                //#1#

                if (lastMouseClickPosition.Y < drawCoordPlayer.Y)
                {
                    abstractPlayerPositon.Y += CollisionDetection.GoUp(abstractPlayerPositon, moveSpeedPlayer, drawCoordPlayer, cropWidth, cropHeight);
                }
                if (lastMouseClickPosition.Y > drawCoordPlayer.Y)
                {
                    abstractPlayerPositon.Y -= CollisionDetection.GoDown(abstractPlayerPositon, moveSpeedPlayer, drawCoordPlayer, cropWidth, cropHeight);
                }

                if (lastMouseClickPosition.X < drawCoordPlayer.X)
                {
                    abstractPlayerPositon.X += CollisionDetection.GoLeft(abstractPlayerPositon, moveSpeedPlayer, drawCoordPlayer, cropWidth, cropHeight);
                }

                if (lastMouseClickPosition.X > drawCoordPlayer.X)
                {
                    abstractPlayerPositon.X -= CollisionDetection.GoRight(abstractPlayerPositon, moveSpeedPlayer, drawCoordPlayer, cropWidth, cropHeight);
                }
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
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(CharacterManager.Instance.PlayerImage, drawCoordPlayer, CropCurrentFramePlayer, Color.White);
            //spriteBatch.DrawString(font, playerPositon.X + " " + playerPositon.Y, new Vector2(30, 30), Color.Blue);
            //spriteBatch.DrawString(font, CharacterManager.enemiesList[0].GARGAcoor.X + " " + CharacterManager.enemiesList[0].GARGAcoor.Y, new Vector2(30, 70), Color.AliceBlue);
            //spriteBatch.DrawString(font, CharacterManager.enemiesList[0].GARGA.X + " " + CharacterManager.enemiesList[0].GARGA.Y, new Vector2(30, 110), Color.AliceBlue);
        }
        
    }



}
    
    
    
    

