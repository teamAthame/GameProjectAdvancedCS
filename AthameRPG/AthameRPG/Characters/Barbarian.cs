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
    public class Barbarian : Character
    {
        private const int cropWidth = 32;
        private const int cropHeight = 48;
        private const float moveSpeedPlayer = 2f;

        private float playerCenterCoordX;
        private float playerCenterCoordY;

        private float mapDetectionFromTop;
        private float mapDetectionFromBottom;
        private float mapDetectionFromLeft;
        private float mapDetectionFromRight;

        private Rectangle cropCurrentFramePlayer;
        private Vector2 coordPlayer;
        private Vector2 lastMouseClickPosition;

        //Camera camera = new Camera();// proba
        public Vector2 playerPositon; /// proba 
        private SpriteFont font;
        protected ContentManager content;

        private MouseState mouse;

        public Barbarian(float startPositionX, float startPositionY) : base(startPositionX, startPositionY)
        {
            this.CropCurrentFramePlayer = cropCurrentFramePlayer;
            coordPlayer = new Vector2(startPositionX, startPositionY);
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
            content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");
            font = content.Load<SpriteFont>("../Content/Fonts/ArialBig");
        }
        public override void UnloadContent()
        {

        }

        public Vector2 CoordP()
        {
            return playerPositon;
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

                /*
                 
                 ENEMY CROP PICTURE IS BIGGER THAN ACTUAL WHAT WE SEE !!! In this way we have -+ 25 -+ 75 to adjust enemy detection !!!!
                 
                 */

                if (lastMouseClickPosition.Y < coordPlayer.Y)
                {
                    playerPositon.Y += GoUp();
                }
                if (lastMouseClickPosition.Y > coordPlayer.Y)
                {
                    playerPositon.Y -= GoDown();
                }

                if (lastMouseClickPosition.X < coordPlayer.X)
                {
                    playerPositon.X += GoLeft();
                }

                if (lastMouseClickPosition.X > coordPlayer.X)
                {
                    playerPositon.X -= GoRight();
                }
            }
        }

        private float GoUp()
        {
            float result = moveSpeedPlayer;
            
            foreach (Vector2 coordinates in Map.Obstacles)
            {
                
                mapDetectionFromTop = coordinates.Y + playerPositon.Y;
                mapDetectionFromBottom = coordinates.Y + 50f + playerPositon.Y;
                mapDetectionFromLeft = coordinates.X + playerPositon.X;//
                mapDetectionFromRight = coordinates.X + 50f + playerPositon.X;

                float pUp = coordPlayer.Y;
                float pDown = coordPlayer.Y + cropHeight;
                float pLeft = coordPlayer.X;
                float pRight = coordPlayer.X + cropWidth;


                /// if (1 -2) ...... if 3 > 4 | 5 lower than 6 
                result = HaveCollision(pUp, mapDetectionFromBottom, pLeft, mapDetectionFromRight, pRight, mapDetectionFromLeft, moveSpeedPlayer);

                if (result == 0)
                {
                    break;
                }
                
            }

            if (result != 0)
            {
                int count = 0;

                foreach (var enemy in CharacterManager.EnemiesPositionList)
                {
                    count++;

                    float enemyTop = enemy.Y + playerPositon.Y - 25f;
                    float enemyBottom = (enemy.Y + playerPositon.Y) + Enemy.cropHeight - 70f;
                    float enemyLeft = enemy.X + playerPositon.X - 25f;
                    float enemyRight = enemy.X + Enemy.cropWidth + playerPositon.X - 65f;

                    float pUp = coordPlayer.Y;
                    float pDown = coordPlayer.Y + cropHeight;
                    float pLeft = coordPlayer.X;
                    float pRight = coordPlayer.X + cropWidth;

                    /// if (1 -2) ...... if 3 > 4 | 5 lower than 6 
                    result = HaveCollision(pUp, enemyBottom, pLeft, enemyRight, pRight, enemyLeft , moveSpeedPlayer);

                    if (result == 0)
                    {
                        break;
                    }

                }
            }


            return result;
        }
        
        private float GoDown()
        {
            float result = moveSpeedPlayer;

            foreach (Vector2 coordinates in Map.Obstacles)
            {
                mapDetectionFromTop = coordinates.Y + playerPositon.Y;
                mapDetectionFromBottom = coordinates.Y + 50f + playerPositon.Y;
                mapDetectionFromLeft = coordinates.X + playerPositon.X;//
                mapDetectionFromRight = coordinates.X + 50f + playerPositon.X;

                float pUp = coordPlayer.Y;
                float pDown = coordPlayer.Y + cropHeight;
                float pLeft = coordPlayer.X;
                float pRight = coordPlayer.X + cropWidth;

                result = HaveCollision(pDown, mapDetectionFromTop, pLeft, mapDetectionFromRight, pRight, mapDetectionFromLeft, moveSpeedPlayer);

                if (result == 0)
                {
                    break;
                }
            }

            if (result != 0)
            {
                int count = 0;

                foreach (var enemy in CharacterManager.EnemiesPositionList)
                {
                    count++;

                    float enemyTop = enemy.Y + playerPositon.Y - 25f; 
                    float enemyBottom = (enemy.Y + playerPositon.Y) + Enemy.cropHeight - 70f;
                    float enemyLeft = enemy.X + playerPositon.X - 25f;
                    float enemyRight = enemy.X + Enemy.cropWidth + playerPositon.X - 65f;

                    float pUp = coordPlayer.Y;
                    float pDown = coordPlayer.Y + cropHeight;
                    float pLeft = coordPlayer.X;
                    float pRight = coordPlayer.X + cropWidth;

                    /// if (1 -2) ...... if 3 > 4 | 5 lower than 6 
                    result = HaveCollision(pDown, enemyTop, pLeft, enemyRight, pRight, enemyLeft , moveSpeedPlayer);

                    if (result == 0)
                    {
                        break;
                    }
                }
            }

            return result;
        }

        private float GoLeft()
        {
            float result = moveSpeedPlayer;

            foreach (Vector2 coordinates in Map.Obstacles)
            {
                mapDetectionFromTop = coordinates.Y + playerPositon.Y;
                mapDetectionFromBottom = coordinates.Y + 50f + playerPositon.Y;
                mapDetectionFromLeft = coordinates.X + playerPositon.X;//
                mapDetectionFromRight = coordinates.X + 50f + playerPositon.X;

                float pUp = coordPlayer.Y;
                float pDown = coordPlayer.Y + cropHeight;
                float pLeft = coordPlayer.X;
                float pRight = coordPlayer.X + cropWidth;

                result = HaveCollision(pLeft, mapDetectionFromRight, pUp, mapDetectionFromBottom, pDown, mapDetectionFromTop, moveSpeedPlayer);

                if (result == 0)
                {
                    break;
                }
                
            }
            if (result != 0)
            {
                int count = 0;

                foreach (var enemy in CharacterManager.EnemiesPositionList)
                {
                    count++;

                    float enemyTop = enemy.Y + playerPositon.Y - 25f;
                    float enemyBottom = (enemy.Y + playerPositon.Y) + Enemy.cropHeight - 70f;
                    float enemyLeft = enemy.X + playerPositon.X - 25f;
                    float enemyRight = enemy.X + Enemy.cropWidth + playerPositon.X - 65f;

                    float pUp = coordPlayer.Y;
                    float pDown = coordPlayer.Y + cropHeight;
                    float pLeft = coordPlayer.X;
                    float pRight = coordPlayer.X + cropWidth;

                    /// if (1 -2) ...... if 3 > 4 | 5 lower than 6 
                    result = HaveCollision(pLeft, enemyRight, pUp, enemyBottom, pDown, enemyTop, moveSpeedPlayer);

                    if (result == 0)
                    {
                        break;
                    }
                }
            }

            return result;
        }

        private float GoRight()
        {
            float result = moveSpeedPlayer;

            foreach (Vector2 coordinates in Map.Obstacles)
            {
                mapDetectionFromTop = coordinates.Y + playerPositon.Y;
                mapDetectionFromBottom = coordinates.Y + 50f + playerPositon.Y;
                mapDetectionFromLeft = coordinates.X + playerPositon.X;//
                mapDetectionFromRight = coordinates.X + 50f + playerPositon.X;

                float pUp = coordPlayer.Y;
                float pDown = coordPlayer.Y + cropHeight;
                float pLeft = coordPlayer.X;
                float pRight = coordPlayer.X + cropWidth;

                /// if (1 -2) ...... if 3 > 4 | 5 lower than 6 
                result = HaveCollision(pRight, mapDetectionFromLeft, pUp, mapDetectionFromBottom, pDown, mapDetectionFromTop, moveSpeedPlayer);

                if (result == 0)
                {
                    break;
                }
                
            }
            if (result != 0)
            {
                int count = 0;

                foreach (var enemy in CharacterManager.EnemiesPositionList)
                {
                    count++;

                    float enemyTop = enemy.Y + playerPositon.Y - 25f;
                    float enemyBottom = (enemy.Y + playerPositon.Y) + Enemy.cropHeight - 70f;
                    float enemyLeft = enemy.X + playerPositon.X - 25f;
                    float enemyRight = enemy.X + Enemy.cropWidth + playerPositon.X - 65f;

                    float pUp = coordPlayer.Y;
                    float pDown = coordPlayer.Y + cropHeight;
                    float pLeft = coordPlayer.X;
                    float pRight = coordPlayer.X + cropWidth;

                    /// if (1 -2) ...... if 3 > 4 | 5 lower than 6 
                    result = HaveCollision(pRight, enemyLeft, pUp, enemyBottom, pDown, enemyTop, moveSpeedPlayer);

                    if (result == 0)
                    {
                        break;
                    }
                }
            }

            return result;
        }

        private float HaveCollision(float pSide, float enemySide, float pCheckFirstSide, float enemyOpositeFirstSide, float pCheckSecondSide, float enemyOpositeSecondSide, float moveSpeedPlayer)
        {
            float result = moveSpeedPlayer;

            if (Math.Abs(pSide - enemySide) <= 2)
            {
                if ((pCheckFirstSide > enemyOpositeFirstSide) || (pCheckSecondSide < enemyOpositeSecondSide))
                {
                    result = moveSpeedPlayer;
                }
                else
                {
                    result = 0;
                }
            }

            return result;
        }

        /// <summary>
        /// //////////////////////////////   NEEEEEEEEEEEEEEEEEE TRIIIIIIIIIIIIIIIIIIIII -----------------------------
        /// </summary>
        /// <param name="spriteBatch"></param>

        //private void MovingWithMouse()
        //{
        //    if (mouse.LeftButton == ButtonState.Pressed)
        //    {
        //        // take position where need to go
        //        lastMouseClickPosition.X = mouse.X;
        //        lastMouseClickPosition.Y = mouse.Y;

        //    }

        //    PlayerCenterCoordX = coordPlayer.X + (cropWidth / 2);
        //    PlayerCenterCoordY = coordPlayer.Y + (cropHeight / 2);

        //    if (PlayerCenterCoordX != lastMouseClickPosition.X || PlayerCenterCoordY != lastMouseClickPosition.Y)
        //    {
        //        if (lastMouseClickPosition.X > PlayerCenterCoordX)
        //        {
        //            coordPlayer.X += ObstacleDetectionWhenGoesRight(moveSpeedPlayer);
        //        }
        //        if (lastMouseClickPosition.X < PlayerCenterCoordX)
        //        {
        //            coordPlayer.X -= ObstacleDetectionWhenGoesLeft(moveSpeedPlayer);
        //        }
        //        if (lastMouseClickPosition.Y > PlayerCenterCoordY)
        //        {
        //            coordPlayer.Y += ObstacleDetectionWhenGoesDown(moveSpeedPlayer);
        //        }
        //        if (lastMouseClickPosition.Y < PlayerCenterCoordY)
        //        {
        //            coordPlayer.Y -= ObstacleDetectionWhenGoesUp(moveSpeedPlayer);
        //        }
        //    }
        //}

        //private float ObstacleDetectionWhenGoesUp(float moveSpeedPlayer)
        //{

        //    float result = moveSpeedPlayer;

        //    foreach (Vector2 coordinates in Map.Obstacles)
        //    {
        //        mapDetectionFromTop = coordinates.Y;
        //        mapDetectionFromBottom = coordinates.Y + 50f;
        //        mapDetectionFromLeft = coordinates.X;//
        //        mapDetectionFromRight = coordinates.X + 50f;

        //        if (Math.Abs(coordPlayer.Y - mapDetectionFromBottom) <= 2f)
        //        {
        //            if ((coordPlayer.X > mapDetectionFromRight) || ((coordPlayer.X + cropWidth) < mapDetectionFromLeft))
        //            {
        //                result = moveSpeedPlayer;
        //            }
        //            else
        //            {
        //                result = 0f;
        //                break;
        //            }

        //        }
        //    }

        //    return result ;
        //}

        //private float ObstacleDetectionWhenGoesLeft(float moveSpeedPlayer)
        //{
        //    float result = moveSpeedPlayer;

        //    foreach (Vector2 coordinates in Map.Obstacles)
        //    {
        //        mapDetectionFromTop = coordinates.Y;
        //        mapDetectionFromBottom = coordinates.Y + 50f;
        //        mapDetectionFromLeft = coordinates.X;//
        //        mapDetectionFromRight = coordinates.X + 50f;

        //        if (Math.Abs(coordPlayer.X - mapDetectionFromRight) <= 2f)
        //        {
        //            if ((coordPlayer.Y > mapDetectionFromBottom) || ((coordPlayer.Y + cropHeight) < mapDetectionFromTop))
        //            {
        //                result = moveSpeedPlayer;
        //            }
        //            else
        //            {
        //                result = 0f;
        //                break;
        //            }

        //        }
        //    }

        //    return result;
        //}

        //private float ObstacleDetectionWhenGoesDown(float moveSpeedPlayer)
        //{

        //    float result = moveSpeedPlayer;

        //    foreach (Vector2 coordinates in Map.Obstacles)
        //    {
        //        mapDetectionFromTop = coordinates.Y;
        //        mapDetectionFromBottom = coordinates.Y + 50f;
        //        mapDetectionFromLeft = coordinates.X;
        //        mapDetectionFromRight = coordinates.X + 50f;

        //        if (Math.Abs((coordPlayer.Y + cropHeight)- mapDetectionFromTop) <= 2f)
        //        {
        //            if ((coordPlayer.X > mapDetectionFromRight) || ((coordPlayer.X + cropWidth) < mapDetectionFromLeft))
        //            {
        //                result = moveSpeedPlayer;
        //            }
        //            else
        //            {
        //                result = 0f;
        //                break;
        //            }

        //        }

        //    }

        //    return result;
        //}

        //private float ObstacleDetectionWhenGoesRight(float moveSpeedPlayer)
        //{

        //    float result = moveSpeedPlayer;

        //    foreach (Vector2 coordinates in Map.Obstacles)
        //    {
        //        mapDetectionFromTop = coordinates.Y;
        //        mapDetectionFromBottom = coordinates.Y + 50f;
        //        mapDetectionFromLeft = coordinates.X;
        //        mapDetectionFromRight = coordinates.X + 50f;

        //        if (Math.Abs((coordPlayer.X + cropWidth) - mapDetectionFromLeft) <= 2f)
        //        {
        //            if ((coordPlayer.Y > mapDetectionFromBottom) || ((coordPlayer.Y + cropHeight) < mapDetectionFromTop))
        //            {
        //                result = moveSpeedPlayer;
        //            }
        //            else
        //            {
        //                result = 0f;
        //                break;
        //            }

        //        }

        //    }

        //    return result;
        //}

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(CharacterManager.Instance.PlayerImage, coordPlayer, CropCurrentFramePlayer, Color.White);
            spriteBatch.DrawString(font, playerPositon.X + " " + playerPositon.Y, new Vector2(30, 30), Color.Blue); ;
        }

    }
}
