using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AthameRPG.GameEngine;

namespace AthameRPG.Characters
{
    public class Gargamel : Enemy
    {
        private const int cropWidth = 126;
        private const int cropHeight = 168;

        private const float moveSpeedPlayer = 0.8f;

        private float gargamelCenterCoordX;
        private float gargamelCenterCoordY;

        private Rectangle cropCurrentFrameGargamel;
        private Vector2 coordGargamel;


        public Gargamel(float startPositionX, float startPositionY) : base(startPositionX, startPositionY)
        {
            this.CropCurrentFrameGargamel = cropCurrentFrameGargamel;
            coordGargamel = new Vector2(startPositionX - (cropWidth / 2f), startPositionY - (cropHeight / 2f));
        }

        public Rectangle CropCurrentFrameGargamel
        {
            get
            {
                return this.cropCurrentFrameGargamel;
            }
            private set
            {
                this.cropCurrentFrameGargamel = new Rectangle(0, 0, cropWidth, cropHeight);
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
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(CharacterManager.Instance.GargamelImage, coordGargamel, CropCurrentFrameGargamel, Color.White);
        }
    }
}
