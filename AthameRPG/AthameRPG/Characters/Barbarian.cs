using AthameRPG.GameEngine;
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
    public class Barbarian : Unit
    {
        private const int cropWidth = 32;
        private const int cropHeight = 48;
        private const float moveSpeedPlayer = 1f;

        private float playerCenterCoordX;
        private float playerCenterCoordY;
        private float startPositionX, startPositionY;


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

        public virtual void LoadContent()
        {

        }
        public virtual void UnloadContent()
        {

        }

        public virtual void Update(GameTime gameTime)
        {
            mouse = Mouse.GetState();

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
                    coordPlayer.X += moveSpeedPlayer;
                }
                if (lastMouseClickPosition.X < PlayerCenterCoordX)
                {
                    coordPlayer.X -= moveSpeedPlayer;
                }
                if (lastMouseClickPosition.Y > PlayerCenterCoordY)
                {
                    coordPlayer.Y += moveSpeedPlayer;
                }
                if (lastMouseClickPosition.Y < PlayerCenterCoordY)
                {
                    coordPlayer.Y -= moveSpeedPlayer;
                }
            }

            
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(CharacterManager.Instance.PlayerImage, coordPlayer, CropCurrentFramePlayer, Color.White);
        }


    }
}
