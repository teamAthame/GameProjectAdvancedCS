using Microsoft.Xna.Framework;
using AthameRPG.GameEngine;
using Microsoft.Xna.Framework.Graphics;

namespace AthameRPG.Characters
{
    public abstract class Enemy : Unit
    {
        
        public const int cropWidth = 80;
        public const int cropHeight = 85;
        protected int cropStay = 0;
        protected int north = 440;
        protected int south = 15;
        protected int east = 660;
        protected int west = 224;
        protected int northEast = 550;
        protected int northWest = 330;
        protected int southEast = 770;
        protected int southWest = 122;
        protected float moveSpeedEnemy = 1f;
        protected Rectangle cropCurrentFrameGargamel;
        protected Vector2 coordGargamel;
        protected Vector2 drawCoordEnemy;

        private int id;//  NUMBER IN THE ENEMY LIST //

        //public Enemy(float startPositionX, float startPositionY, int atack, int health, int defence) : base(startPositionX, startPositionY, atack, health, defence)
        //{
            
        //}
        
        public Enemy(float startPositionX, float startPositionY, int id, int atack, int health, int defence)
            : base(startPositionX, startPositionY, atack, health, defence)
        {
            this.ID = id;
        }

        public override void LoadContent()
        {
            
        }

        public override void UnloadContent()
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            if (!Character.GetIsInBattle && !Character.GetIsInCastle)
            {
                this.lastAbstractCoord = this.coordGargamel;
                float plTopSide = Character.DrawCoordPlayer.Y;
                float plBottomSide = Character.DrawCoordPlayer.Y + Character.PlayerCropHeight;
                float plLeftSide = Character.DrawCoordPlayer.X;
                float plRightSide = Character.DrawCoordPlayer.X + Character.PlayerCropWidth;

                bool isPlayerDown = CollisionDetection.IsNear(plTopSide, this.drawCoordEnemy.Y + cropHeight);
                bool isPlayerUp = CollisionDetection.IsNear(plBottomSide, this.drawCoordEnemy.Y);
                bool isPlayerLeft = CollisionDetection.IsNear(plRightSide, this.drawCoordEnemy.X);
                bool isPlayerRight = CollisionDetection.IsNear(plLeftSide, this.drawCoordEnemy.X + cropWidth);

                if (isPlayerUp && (isPlayerRight || isPlayerLeft))
                {
                    this.coordGargamel.Y -= CollisionDetection.EnemyGoUp(this.DetectionEnemyCoord, this.drawCoordEnemy, cropHeight,
                        cropWidth, this.moveSpeedEnemy);
                }

                if (isPlayerDown && (isPlayerRight || isPlayerLeft))
                {
                    this.coordGargamel.Y += CollisionDetection.EnemyGoDown(this.DetectionEnemyCoord, this.drawCoordEnemy, cropHeight,
                        cropWidth, this.moveSpeedEnemy);
                }

                if (isPlayerLeft && (isPlayerUp || isPlayerDown))
                {
                    this.coordGargamel.X -= CollisionDetection.EnemyGoLeft(this.DetectionEnemyCoord, this.drawCoordEnemy, cropHeight,
                        cropWidth, this.moveSpeedEnemy);
                }

                if (isPlayerRight && (isPlayerUp || isPlayerDown))
                {
                    this.coordGargamel.X += CollisionDetection.EnemyGoRight(this.DetectionEnemyCoord, this.drawCoordEnemy, cropHeight,
                        cropWidth, this.moveSpeedEnemy);
                }

                this.drawCoordEnemy.X = this.coordGargamel.X + CharacterManager.barbarian.CoordP().X;
                this.drawCoordEnemy.Y = this.coordGargamel.Y + CharacterManager.barbarian.CoordP().Y;

                /// Re-Write new position on the screen of enemy  
                CharacterManager.EnemiesPositionList[ID] = this.drawCoordEnemy;

                this.frameCounter += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

                if (this.frameCounter >= this.switchCounter)
                {
                    this.frameCounter = 0;

                    this.returnedValue = Animation.SpriteSheetAnimation(this.lastAbstractCoord, this.coordGargamel,
                        this.direction, this.cropFrame, cropWidth, cropHeight, this.cropStay, this.south, this.north, this.west, this.east, this.southWest,
                        this.southEast, this.northWest, this.northEast);

                    this.cropCurrentFrame = this.returnedValue.ImageCrop;
                    this.direction = this.returnedValue.Direction;

                    this.cropFrame++;

                    if (this.cropFrame == 4)
                    {
                        this.cropFrame = 0;
                    }
                }

            }
            else if (Character.GetIsInCastle)
            {

            }
            else if (!Character.GetIsInBattle)
            {

            }


            // Check if the mouse position is inside the rectangle
            // fix it !

            //var mouseState = Mouse.GetState();
            //var mousePosition = coordGargamel;
            //Rectangle area = new Rectangle(new Point((int)coordGargamel.X, (int)drawCoordEnemy.X), new Point((int)coordGargamel.Y, (int)drawCoordEnemy.Y));

            /// fix the problem !!!


            //if (area.Contains(mousePosition))
            //{
            //    if (mouseState.LeftButton == ButtonState.Pressed)
            //    {
            //        moveSpeedEnemy = 0f;
            //        //isAlive = false;

            //    }
            //}

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!Character.GetIsInBattle && !Character.GetIsInCastle)
            {
                spriteBatch.Draw(CharacterManager.Instance.GargamelImage, this.drawCoordEnemy, this.CropCurrentFrame, Color.White);
            }
            else if (Character.GetIsInCastle)
            {

            }
            else if (Character.GetIsInBattle)
            {

            }
        }

        public int ID
        {
            get
            {
                return this.id;
            }
            private set
            {
                this.id = value;
            }
        }

        public static float SearchingRadius
        {
            get { return 100f; }
        }
        
        public Vector2 DetectionEnemyCoord
        {
            get
            {
                return new Vector2(this.coordGargamel.X + CharacterManager.barbarian.CoordP().X,
                    this.coordGargamel.Y + CharacterManager.barbarian.CoordP().Y);
            }
        }
    }
}
