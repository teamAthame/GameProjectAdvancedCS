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
        
        private const float moveSpeedEnemy = 2f;
        
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

            // test

            key = Keyboard.GetState();

            if (ID == 0)
            {
                if (key.IsKeyDown(Keys.Up))
                {
                    coordGargamel.Y -= CollisionDetection.EnemyGoUp(DetectionEnemyCoord, drawCoordEnemy, cropHeight, cropWidth, moveSpeedEnemy);
                }

                if (key.IsKeyDown(Keys.Down))
                {
                    coordGargamel.Y += CollisionDetection.EnemyGoDown(DetectionEnemyCoord, drawCoordEnemy, cropHeight, cropWidth, moveSpeedEnemy);
                }

                if (key.IsKeyDown(Keys.Left))
                {
                    coordGargamel.X -= CollisionDetection.EnemyGoLeft(DetectionEnemyCoord, drawCoordEnemy, cropHeight, cropWidth, moveSpeedEnemy);
                }

                if (key.IsKeyDown(Keys.Right))
                {
                    coordGargamel.X += CollisionDetection.EnemyGoRight(DetectionEnemyCoord, drawCoordEnemy, cropHeight, cropWidth, moveSpeedEnemy);
                }
            }


            /// testovo ------------------------------------------------------
            //if (true)
            //{
            //    coordGargamel.X -= GoLeft();
            //}

            drawCoordEnemy.X = coordGargamel.X + CharacterManager.barbarian.CoordP().X;
            drawCoordEnemy.Y = coordGargamel.Y + CharacterManager.barbarian.CoordP().Y;
            
            /// Re-Write new position on the screen of enemy  
            CharacterManager.EnemiesPositionList[ID] = drawCoordEnemy;

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            

            spriteBatch.Draw(CharacterManager.Instance.GargamelImage, drawCoordEnemy, CropCurrentFrameGargamel, Color.White);
        }

        //private float GoUp()
        //{
        //    float result = moveSpeedEnemy;

        //    foreach (Vector2 coordinates in Map.Obstacles)
        //    {
        //        float mapDetectionFromTop = coordinates.Y + CharacterManager.barbarian.CoordP().Y;
        //        float mapDetectionFromBottom = coordinates.Y + 50f + CharacterManager.barbarian.CoordP().Y;
        //        float mapDetectionFromLeft = coordinates.X + CharacterManager.barbarian.CoordP().X;
        //        float mapDetectionFromRight = coordinates.X + 50f + CharacterManager.barbarian.CoordP().X;

        //        float pUp = DetectionEnemyCoord.Y;
        //        float pDown = DetectionEnemyCoord.Y + cropHeight;
        //        float pLeft = DetectionEnemyCoord.X ;
        //        float pRight = DetectionEnemyCoord.X + cropWidth;

        //        result = HaveCollision(pUp, mapDetectionFromBottom, pLeft, mapDetectionFromRight, pRight, mapDetectionFromLeft, moveSpeedEnemy);

        //        if (result == 0)
        //        {
        //            break;
        //        }

        //    }

        //    if (result != 0)
        //    {

        //        foreach (var enemy in CharacterManager.EnemiesPositionList)
        //        {
        //            //if (drawCoordGargamel.X != enemy.X && drawCoordGargamel.Y != enemy.Y )
        //            // Does not WORK with this ckeck ...
        //            // when there are on same X ... they don't detect collision

        //            float enemyTop = enemy.Y;
        //            float enemyBottom = enemy.Y + (cropHeight);
        //            float enemyLeft = enemy.X;
        //            float enemyRight = enemy.X + (cropWidth);

        //            float pUp = drawCoordEnemy.Y;
        //            float pDown = drawCoordEnemy.Y + cropHeight;
        //            float pLeft = drawCoordEnemy.X;
        //            float pRight = drawCoordEnemy.X + cropWidth;

        //            ////////////////////// if (1 -2) ...... if (3 > 4) ||   (5 lower than 6) 
        //            result = HaveCollision(pUp, enemyBottom, pLeft, enemyRight, pRight, enemyLeft, moveSpeedEnemy);

        //            if (result == 0)
        //            {
        //                break;
        //            }

        //        }
        //    }

        //    return result;
        //}
        
        //private float GoDown()
        //{
        //    float result = moveSpeedEnemy;

        //    foreach (Vector2 coordinates in Map.Obstacles)
        //    {
        //        float mapDetectionFromTop = coordinates.Y + CharacterManager.barbarian.CoordP().Y;
        //        float mapDetectionFromBottom = coordinates.Y + 50f + CharacterManager.barbarian.CoordP().Y;
        //        float mapDetectionFromLeft = coordinates.X + CharacterManager.barbarian.CoordP().X;
        //        float mapDetectionFromRight = coordinates.X + 50f + CharacterManager.barbarian.CoordP().X;

        //        float pUp = DetectionEnemyCoord.Y ;
        //        float pDown = DetectionEnemyCoord.Y + cropHeight;
        //        float pLeft = DetectionEnemyCoord.X;
        //        float pRight = DetectionEnemyCoord.X + cropWidth;

        //        result = HaveCollision(pDown, mapDetectionFromTop, pLeft, mapDetectionFromRight, pRight, mapDetectionFromLeft, moveSpeedEnemy);

        //        if (result == 0)
        //        {
        //            break;
        //        }

        //    }

        //    if (result != 0)
        //    {

        //        foreach (var enemy in CharacterManager.EnemiesPositionList)
        //        {
        //            //if (drawCoordGargamel.X != enemy.X && drawCoordGargamel.Y != enemy.Y )
        //            // Does not WORK with this ckeck ...
        //            // when there are on same X ... they don't detect collision

        //            float enemyTop = enemy.Y;
        //            float enemyBottom = enemy.Y + (cropHeight);
        //            float enemyLeft = enemy.X;
        //            float enemyRight = enemy.X + (cropWidth);

        //            float pUp = drawCoordEnemy.Y;
        //            float pDown = drawCoordEnemy.Y + cropHeight;
        //            float pLeft = drawCoordEnemy.X;
        //            float pRight = drawCoordEnemy.X + cropWidth;

        //            ////////////////////// if (1 -2) ...... if (3 > 4) ||   (5 lower than 6) 
        //            result = HaveCollision(pDown, enemyTop, pLeft, enemyRight, pRight, enemyLeft, moveSpeedEnemy);

        //            if (result == 0)
        //            {
        //                break;
        //            }

        //        }
        //    }

        //    return result;
        //}
        
        //private float GoLeft()
        //{
        //    float result = moveSpeedEnemy;

        //    foreach (Vector2 coordinates in Map.Obstacles)
        //    {
        //        float mapDetectionFromTop = coordinates.Y+ CharacterManager.barbarian.CoordP().Y;
        //        float mapDetectionFromBottom = coordinates.Y + 50f + CharacterManager.barbarian.CoordP().Y;
        //        float mapDetectionFromLeft = coordinates.X + CharacterManager.barbarian.CoordP().X;
        //        float mapDetectionFromRight = coordinates.X + 50f + CharacterManager.barbarian.CoordP().X;

        //        float pUp = DetectionEnemyCoord.Y;
        //        float pDown = DetectionEnemyCoord.Y + cropHeight;
        //        float pLeft = DetectionEnemyCoord.X;
        //        float pRight = DetectionEnemyCoord.X + cropWidth;

        //        result = HaveCollision(pLeft, mapDetectionFromRight, pUp, mapDetectionFromBottom, pDown, mapDetectionFromTop, moveSpeedEnemy);

        //        if (result == 0)
        //        {
        //            break;
        //        }

        //    }
        //    if (result != 0)
        //    {

        //        foreach (var enemy in CharacterManager.EnemiesPositionList)
        //        {
        //            //if (drawCoordGargamel.X != enemy.X && drawCoordGargamel.Y != enemy.Y )
        //            //{

        //            //}

        //            float enemyTop = enemy.Y;
        //            float enemyBottom = enemy.Y + (cropHeight);
        //            float enemyLeft = enemy.X;
        //            float enemyRight = enemy.X + (cropWidth);

        //            float pUp = drawCoordEnemy.Y;
        //            float pDown = drawCoordEnemy.Y + cropHeight;
        //            float pLeft = drawCoordEnemy.X;
        //            float pRight = drawCoordEnemy.X + cropWidth;

        //            /// if (1 -2) ...... if 3 > 4 | 5 lower than 6 
        //            result = HaveCollision(pLeft, enemyRight, pUp, enemyBottom, pDown, enemyTop, moveSpeedEnemy);

        //            if (result == 0)
        //            {
        //                break;
        //            }

        //        }
        //    }

        //    return result;
        //}

        //private float GoRight()
        //{
        //    float result = moveSpeedEnemy;

        //    foreach (Vector2 coordinates in Map.Obstacles)
        //    {


        //        float mapDetectionFromTop = coordinates.Y + CharacterManager.barbarian.CoordP().Y;
        //        float mapDetectionFromBottom = coordinates.Y + 50f + CharacterManager.barbarian.CoordP().Y;
        //        float mapDetectionFromLeft = coordinates.X + CharacterManager.barbarian.CoordP().X;
        //        float mapDetectionFromRight = coordinates.X + 50f + CharacterManager.barbarian.CoordP().X;

        //        float pUp = DetectionEnemyCoord.Y ;
        //        float pDown = DetectionEnemyCoord.Y + cropHeight;
        //        float pLeft = DetectionEnemyCoord.X;
        //        float pRight = DetectionEnemyCoord.X + cropWidth;

        //        result = HaveCollision(mapDetectionFromLeft, pRight, pUp, mapDetectionFromBottom, pDown, mapDetectionFromTop, moveSpeedEnemy);

        //        if (result == 0)
        //        {
        //            break;
        //        }

        //    }

        //    if (result != 0)
        //    {

        //        foreach (var enemy in CharacterManager.EnemiesPositionList)
        //        {
        //            //if (drawCoordGargamel.X != enemy.X && drawCoordGargamel.Y != enemy.Y )
        //            // Does not WORK with this ckeck ...
        //            // when there are on same X ... they don't detect collision
                    
        //            float enemyTop = enemy.Y;
        //            float enemyBottom = enemy.Y + (cropHeight);
        //            float enemyLeft = enemy.X;
        //            float enemyRight = enemy.X + (cropWidth);

        //            float pUp = drawCoordEnemy.Y;
        //            float pDown = drawCoordEnemy.Y + cropHeight;
        //            float pLeft = drawCoordEnemy.X;
        //            float pRight = drawCoordEnemy.X + cropWidth;

        //            ////////////////////// if (1 -2) ...... if (3 > 4) ||   (5 lower than 6) 
        //            result = HaveCollision(pRight, enemyLeft, pUp, enemyBottom, pDown, enemyTop, moveSpeedEnemy);

        //            if (result == 0)
        //            {
        //                break;
        //            }

        //        }
        //    }

        //    return result;
        //}


        //private float HaveCollision(float pSide, float enemySide, float pCheckFirstSide, float enemyOpositeFirstSide, float pCheckSecondSide, float enemyOpositeSecondSide, float moveSpeedGargamel)
        //{
        //    float result = moveSpeedGargamel;

        //    if (Math.Abs(pSide - enemySide) <= 2)
        //    {
        //        if ((pCheckFirstSide > enemyOpositeFirstSide) || (pCheckSecondSide < enemyOpositeSecondSide))
        //        {
        //            result = moveSpeedGargamel;
        //        }
        //        else
        //        {
        //            result = 0;
        //        }
        //    }

        //    return result;
        //}
    }
}
