using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AthameRPG.GameEngine;
using AthameRPG.GameEngine.Maps;
using Microsoft.Xna.Framework.Input;

namespace AthameRPG.Characters
{
    public class Gargamel : Enemy
    {
        
        private const float moveSpeedEnemy = 1f;
        
        private Rectangle cropCurrentFrameGargamel;
        private Vector2 coordGargamel;
        private Vector2 drawCoordEnemy;
        

        // za triene po nqkoe vreme TEST garga collision
        KeyboardState key;
        
        public Gargamel(float startPositionX, float startPositionY, int id) : base(startPositionX, startPositionY, id)
        {
            this.CropCurrentFrameGargamel = cropCurrentFrameGargamel;
            coordGargamel = new Vector2(startPositionX - (cropWidth / 2f), startPositionY - (cropHeight / 2f));
            
            //-------------------------------------------------------------------------------------------------------------
            //coordGargamel = new Vector2(startPositionX , startPositionY);
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

        public Vector2 DetectionEnemyCoord
        {
            get
            {
                return new Vector2(coordGargamel.X + CharacterManager.barbarian.CoordP().X, coordGargamel.Y + CharacterManager.barbarian.CoordP().Y);
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

            float plTopSide = Character.DrawCoordPlayer.Y;
            float plBottomSide = Character.DrawCoordPlayer.Y + Character.PlayerCropHeight;
            float plLeftSide = Character.DrawCoordPlayer.X;
            float plRightSide = Character.DrawCoordPlayer.X + Character.PlayerCropWidth;

            bool isPlayerDown = CollisionDetection.IsNear(plTopSide, drawCoordEnemy.Y + cropHeight);
            bool isPlayerUp = CollisionDetection.IsNear(plBottomSide, drawCoordEnemy.Y);
            bool isPlayerLeft = CollisionDetection.IsNear(plRightSide, drawCoordEnemy.X);
            bool isPlayerRight = CollisionDetection.IsNear(plLeftSide, drawCoordEnemy.X + cropWidth);

            if (isPlayerUp && (isPlayerRight || isPlayerLeft))
            {
                coordGargamel.Y -= CollisionDetection.EnemyGoUp(DetectionEnemyCoord, drawCoordEnemy, cropHeight, cropWidth, moveSpeedEnemy);
            }

            if (isPlayerDown && (isPlayerRight || isPlayerLeft))
            {
                coordGargamel.Y += CollisionDetection.EnemyGoDown(DetectionEnemyCoord, drawCoordEnemy, cropHeight, cropWidth, moveSpeedEnemy);
            }

            if (isPlayerLeft && (isPlayerUp || isPlayerDown))
            {
                coordGargamel.X -= CollisionDetection.EnemyGoLeft(DetectionEnemyCoord, drawCoordEnemy, cropHeight, cropWidth, moveSpeedEnemy);
            }

            if (isPlayerRight && (isPlayerUp || isPlayerDown))
            {
                coordGargamel.X += CollisionDetection.EnemyGoRight(DetectionEnemyCoord, drawCoordEnemy, cropHeight, cropWidth, moveSpeedEnemy);
            }

            drawCoordEnemy.X = coordGargamel.X + CharacterManager.barbarian.CoordP().X;
            drawCoordEnemy.Y = coordGargamel.Y + CharacterManager.barbarian.CoordP().Y;
            
            /// Re-Write new position on the screen of enemy  
            CharacterManager.EnemiesPositionList[ID] = drawCoordEnemy;

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            

            spriteBatch.Draw(CharacterManager.Instance.GargamelImage, drawCoordEnemy, CropCurrentFrameGargamel, Color.White);
        }

        
    }
}
