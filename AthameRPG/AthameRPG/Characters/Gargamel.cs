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
        
        private const float moveSpeedGargamel = 2f;

        private Rectangle cropCurrentFrameGargamel;
        private Vector2 coordGargamel;
        private Vector2 drawCoordGargamel;

        //
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
        
        public Vector2 GARGAcoor
        {
            get
            {
                return new Vector2(coordGargamel.X + CharacterManager.barbarian.CoordP().X, coordGargamel.Y + CharacterManager.barbarian.CoordP().Y);
            }
        }

        public Vector2 GARGA
        {
            get
            {
                return new Vector2(coordGargamel.X, coordGargamel.Y);
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

            if (key.IsKeyDown(Keys.Left))
            {
                coordGargamel.X -= GoLeft();
            }
            if (key.IsKeyDown(Keys.Right))
            {
                coordGargamel.X += GoRight();
            }
            
            if (key.IsKeyDown(Keys.Up))
            {
                coordGargamel.Y -= GoUp();
            }
            if (key.IsKeyDown(Keys.Down))
            {
                coordGargamel.Y += GoDown();
            }

            if (true)
            {
                coordGargamel.X -= GoLeft();
            }

            drawCoordGargamel.X = coordGargamel.X + CharacterManager.barbarian.CoordP().X;
            drawCoordGargamel.Y = coordGargamel.Y + CharacterManager.barbarian.CoordP().Y;

            CharacterManager.EnemiesPositionList[ID] = drawCoordGargamel;

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            

            spriteBatch.Draw(CharacterManager.Instance.GargamelImage, drawCoordGargamel, CropCurrentFrameGargamel, Color.White);
        }

        private float GoDown()
        {
            float result = moveSpeedGargamel;

            foreach (Vector2 coordinates in Map.Obstacles)
            {
                float mapDetectionFromTop = coordinates.Y + CharacterManager.barbarian.CoordP().Y;
                float mapDetectionFromBottom = coordinates.Y + 50f + CharacterManager.barbarian.CoordP().Y;
                float mapDetectionFromLeft = coordinates.X + CharacterManager.barbarian.CoordP().X;
                //                                           CharacterManager.barbarian.CoordP().X
                float mapDetectionFromRight = coordinates.X + 50f + CharacterManager.barbarian.CoordP().X;

                float pUp = GARGAcoor.Y ;
                float pDown = GARGAcoor.Y + cropHeight;
                float pLeft = GARGAcoor.X;
                float pRight = GARGAcoor.X + cropWidth;

                result = HaveCollision(pDown, mapDetectionFromTop, pLeft, mapDetectionFromRight, pRight, mapDetectionFromLeft, moveSpeedGargamel);

                if (result == 0)
                {
                    break;
                }

            }



            return result;
        }

        private float GoUp()
        {
            float result = moveSpeedGargamel;

            foreach (Vector2 coordinates in Map.Obstacles)
            {
                float mapDetectionFromTop = coordinates.Y + CharacterManager.barbarian.CoordP().Y;
                float mapDetectionFromBottom = coordinates.Y + 50f + CharacterManager.barbarian.CoordP().Y;
                float mapDetectionFromLeft = coordinates.X + CharacterManager.barbarian.CoordP().X;
                float mapDetectionFromRight = coordinates.X + 50f + CharacterManager.barbarian.CoordP().X;

                float pUp = GARGAcoor.Y;
                float pDown = GARGAcoor.Y + cropHeight;
                float pLeft = GARGAcoor.X ;
                float pRight = GARGAcoor.X + cropWidth;

                result = HaveCollision(pUp, mapDetectionFromBottom, pLeft, mapDetectionFromRight, pRight, mapDetectionFromLeft, moveSpeedGargamel);

                if (result == 0)
                {
                    break;
                }

            }



            return result;
        }

        private float GoRight()
        {
            float result = moveSpeedGargamel;

            foreach (Vector2 coordinates in Map.Obstacles)
            {
                float mapDetectionFromTop = coordinates.Y + CharacterManager.barbarian.CoordP().Y;
                float mapDetectionFromBottom = coordinates.Y + 50f + CharacterManager.barbarian.CoordP().Y;
                float mapDetectionFromLeft = coordinates.X + CharacterManager.barbarian.CoordP().X;
                float mapDetectionFromRight = coordinates.X + 50f + CharacterManager.barbarian.CoordP().X;

                float pUp = GARGAcoor.Y ;
                float pDown = GARGAcoor.Y + cropHeight;
                float pLeft = GARGAcoor.X;
                float pRight = GARGAcoor.X + cropWidth;

                result = HaveCollision(mapDetectionFromLeft, pRight, pUp, mapDetectionFromBottom, pDown, mapDetectionFromTop, moveSpeedGargamel);

                if (result == 0)
                {
                    break;
                }

            }



            return result;
        }

        private float GoLeft()
        {
            float result = moveSpeedGargamel;

            foreach (Vector2 coordinates in Map.Obstacles)
            {
                float mapDetectionFromTop = coordinates.Y+ CharacterManager.barbarian.CoordP().Y;
                float mapDetectionFromBottom = coordinates.Y + 50f + CharacterManager.barbarian.CoordP().Y;
                float mapDetectionFromLeft = coordinates.X + CharacterManager.barbarian.CoordP().X;
                float mapDetectionFromRight = coordinates.X + 50f + CharacterManager.barbarian.CoordP().X;

                float pUp = GARGAcoor.Y;
                float pDown = GARGAcoor.Y + cropHeight;
                float pLeft = GARGAcoor.X;
                float pRight = GARGAcoor.X + cropWidth;

                result = HaveCollision(pLeft, mapDetectionFromRight, pUp, mapDetectionFromBottom, pDown, mapDetectionFromTop, moveSpeedGargamel);

                if (result == 0)
                {
                    break;
                }

            }
            //if (result != 0)
            //{
                
            //    foreach (var enemy in CharacterManager.EnemiesPositionList)
            //    {
                    
            //        float enemyTop = enemy.Y + drawCoordGargamel.Y - 25f;
            //        float enemyBottom = (enemy.Y + drawCoordGargamel.Y) + Enemy.cropHeight - 70f;
            //        float enemyLeft = enemy.X + drawCoordGargamel.X - 25f;
            //        float enemyRight = enemy.X + Enemy.cropWidth + drawCoordGargamel.X - 65f;

            //        float pUp = drawCoordGargamel.Y;
            //        float pDown = drawCoordGargamel.Y + cropHeight;
            //        float pLeft = drawCoordGargamel.X;
            //        float pRight = drawCoordGargamel.X + cropWidth;

            //        /// if (1 -2) ...... if 3 > 4 | 5 lower than 6 
            //        result = HaveCollision(pLeft, enemyRight, pUp, enemyBottom, pDown, enemyTop, moveSpeedGargamel);

            //        if (result == 0)
            //        {
            //            break;
            //        }
            //    }
            //}

            return result;
        }

        private float HaveCollision(float pSide, float enemySide, float pCheckFirstSide, float enemyOpositeFirstSide, float pCheckSecondSide, float enemyOpositeSecondSide, float moveSpeedGargamel)
        {
            float result = moveSpeedGargamel;

            if (Math.Abs(pSide - enemySide) <= 2)
            {
                if ((pCheckFirstSide > enemyOpositeFirstSide) || (pCheckSecondSide < enemyOpositeSecondSide))
                {
                    result = moveSpeedGargamel;
                }
                else
                {
                    result = 0;
                }
            }

            return result;
        }
    }
}
