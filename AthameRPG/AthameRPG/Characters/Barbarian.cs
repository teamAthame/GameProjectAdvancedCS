using AthameRPG.GameEngine;
using AthameRPG.GameEngine.Maps;
using Microsoft.Xna.Framework;
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
        private const float moveSpeedPlayer = 1f;

        private float playerCenterCoordX;
        private float playerCenterCoordY;
        
        private float mapDetectionFromTop;
        private float mapDetectionFromBottom;
        private float mapDetectionFromLeft;
        private float mapDetectionFromRight;
        
        private Rectangle cropCurrentFramePlayer;
        private Vector2 coordPlayer;
        private Vector2 lastMouseClickPosition;

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

        }
        public override void UnloadContent()
        {

        }

        public override void Update(GameTime gameTime)
        {
            mouse = Mouse.GetState();

            MovingWithMouse();

        }

        private void MovingWithMouse()
        {
            if (mouse.LeftButton == ButtonState.Pressed)
            {
                // take position where need to go
                lastMouseClickPosition.X = mouse.X;
                lastMouseClickPosition.Y = mouse.Y;

            }

            PlayerCenterCoordX = coordPlayer.X + (cropWidth / 2);
            PlayerCenterCoordY = coordPlayer.Y + (cropHeight / 2);

            if (PlayerCenterCoordX != lastMouseClickPosition.X || PlayerCenterCoordY != lastMouseClickPosition.Y)
            {
                if (lastMouseClickPosition.X > PlayerCenterCoordX)
                {
                    coordPlayer.X += ObstacleDetectionWhenGoesRight(moveSpeedPlayer);
                }
                if (lastMouseClickPosition.X < PlayerCenterCoordX)
                {
                    coordPlayer.X -= ObstacleDetectionWhenGoesLeft(moveSpeedPlayer);
                }
                if (lastMouseClickPosition.Y > PlayerCenterCoordY)
                {
                    coordPlayer.Y += ObstacleDetectionWhenGoesDown(moveSpeedPlayer);
                }
                if (lastMouseClickPosition.Y < PlayerCenterCoordY)
                {
                    coordPlayer.Y -= ObstacleDetectionWhenGoesUp(moveSpeedPlayer);
                }
            }
        }

        private float ObstacleDetectionWhenGoesUp(float moveSpeedPlayer)
        {
            var aa = Map.Obstacles;

            float result = moveSpeedPlayer;

            foreach (Vector2 coordinates in Map.Obstacles)
            {
                mapDetectionFromTop = coordinates.Y;
                mapDetectionFromBottom = coordinates.Y + 50f;
                mapDetectionFromLeft = coordinates.X;//
                mapDetectionFromRight = coordinates.X + 50f;
                
                if (Math.Abs(coordPlayer.Y - mapDetectionFromBottom) <= 2f)
                {
                    if ((coordPlayer.X > mapDetectionFromRight) || ((coordPlayer.X + cropWidth) < mapDetectionFromLeft))
                    {
                        result = moveSpeedPlayer;
                    }
                    else
                    {
                        result = 0f;
                        break;
                    }
                    
                }
            }

            return result ;
        }

        private float ObstacleDetectionWhenGoesLeft(float moveSpeedPlayer)
        {
            float result = moveSpeedPlayer;

            foreach (Vector2 coordinates in Map.Obstacles)
            {
                mapDetectionFromTop = coordinates.Y;
                mapDetectionFromBottom = coordinates.Y + 50f;
                mapDetectionFromLeft = coordinates.X;//
                mapDetectionFromRight = coordinates.X + 50f;
                
                if (Math.Abs(coordPlayer.X - mapDetectionFromRight) <= 2f)
                {
                    if ((coordPlayer.Y > mapDetectionFromBottom) || ((coordPlayer.Y + cropHeight) < mapDetectionFromTop))
                    {
                        result = moveSpeedPlayer;
                    }
                    else
                    {
                        result = 0f;
                        break;
                    }

                }
            }

            return result;
        }

        private float ObstacleDetectionWhenGoesDown(float moveSpeedPlayer)
        {
            
            float result = moveSpeedPlayer;

            foreach (Vector2 coordinates in Map.Obstacles)
            {
                mapDetectionFromTop = coordinates.Y;
                mapDetectionFromBottom = coordinates.Y + 50f;
                mapDetectionFromLeft = coordinates.X;
                mapDetectionFromRight = coordinates.X + 50f;
                
                if (Math.Abs((coordPlayer.Y + cropHeight)- mapDetectionFromTop) <= 2f)
                {
                    if ((coordPlayer.X > mapDetectionFromRight) || ((coordPlayer.X + cropWidth) < mapDetectionFromLeft))
                    {
                        result = moveSpeedPlayer;
                    }
                    else
                    {
                        result = 0f;
                        break;
                    }

                }
                
            }

            return result;
        }

        private float ObstacleDetectionWhenGoesRight(float moveSpeedPlayer)
        {

            float result = moveSpeedPlayer;

            foreach (Vector2 coordinates in Map.Obstacles)
            {
                mapDetectionFromTop = coordinates.Y;
                mapDetectionFromBottom = coordinates.Y + 50f;
                mapDetectionFromLeft = coordinates.X;
                mapDetectionFromRight = coordinates.X + 50f;

                if (Math.Abs((coordPlayer.X + cropWidth) - mapDetectionFromLeft) <= 2f)
                {
                    if ((coordPlayer.Y > mapDetectionFromBottom) || ((coordPlayer.Y + cropHeight) < mapDetectionFromTop))
                    {
                        result = moveSpeedPlayer;
                    }
                    else
                    {
                        result = 0f;
                        break;
                    }

                }

            }

            return result;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(CharacterManager.Instance.PlayerImage, coordPlayer, CropCurrentFramePlayer, Color.White);
        }
        
    }
}
