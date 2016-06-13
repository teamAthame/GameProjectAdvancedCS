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

        private float moveSpeedEnemy = 1f;

        private const int DefaultWariorAttackPoints = 80;
        private const int DefaultWariorHealthPoints = 80;
        private const int DefaultWariorDefence = 40;

        // Animation values
        private const int cropStay = 0;
        private const int north = 440;
        private const int south = 15;
        private const int east = 660;
        private const int west = 224;
        private const int northEast = 550;
        private const int northWest = 330;
        private const int southEast = 770;
        private const int southWest = 122;

        private Rectangle cropCurrentFrameGargamel;
        private Vector2 coordGargamel;
        private Vector2 drawCoordEnemy;


        // za triene po nqkoe vreme TEST garga collision
        //KeyboardState key;

        public Gargamel(float startPositionX, float startPositionY, int id)
            : base(startPositionX, startPositionY, id, DefaultWariorAttackPoints, DefaultWariorHealthPoints, DefaultWariorDefence)
        {
            coordGargamel = new Vector2(startPositionX - (cropWidth / 2f), startPositionY - (cropHeight / 2f));
            direction = "NW";



            //-------------------------------------------------------------------------------------------------------------
            //coordGargamel = new Vector2(startPositionX , startPositionY);
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
            //lastAbstractCoord = drawCoordEnemy;
            lastAbstractCoord = coordGargamel;
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

            frameCounter += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (frameCounter >= switchCounter)
            {
                frameCounter = 0;

                returnedValue = Animation.SpriteSheetAnimation(lastAbstractCoord, coordGargamel,
                    direction, cropFrame, cropWidth, cropHeight, cropStay, south, north, west, east, southWest,
                    southEast, northWest, northEast);

                cropCurrentFrame = returnedValue.ImageCrop;
                direction = returnedValue.Direction;

                cropFrame++;

                if (cropFrame == 4)
                {
                    cropFrame = 0;
                }
            }

            var mouseState = Mouse.GetState();
            var mousePosition = coordGargamel;
            Rectangle area = new Rectangle(new Point((int)coordGargamel.X, (int)drawCoordEnemy.X), new Point((int)coordGargamel.Y, (int)drawCoordEnemy.Y));

            // Check if the mouse position is inside the rectangle

            if (area.Contains(mousePosition))
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    moveSpeedEnemy = 0f;
                    //isAlive = false;
                 
                }
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {


            spriteBatch.Draw(CharacterManager.Instance.GargamelImage, drawCoordEnemy, CropCurrentFrame, Color.White);
        }
    }
}
