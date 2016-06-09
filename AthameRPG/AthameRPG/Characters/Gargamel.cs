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
        
        private const float moveSpeedPlayer = 0.8f;

        private Rectangle cropCurrentFrameGargamel;
        private Vector2 coordGargamel;
        private Vector2 drawCoordGargamel;


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
            drawCoordGargamel.X = coordGargamel.X + CharacterManager.barbarian.CoordP().X;
            drawCoordGargamel.Y = coordGargamel.Y + CharacterManager.barbarian.CoordP().Y;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            

            spriteBatch.Draw(CharacterManager.Instance.GargamelImage, drawCoordGargamel, CropCurrentFrameGargamel, Color.White);
        }
    }
}
