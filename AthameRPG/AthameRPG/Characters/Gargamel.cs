using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AthameRPG.GameEngine;

namespace AthameRPG.Characters
{
    public class Gargamel : Enemy
    {

        //private float moveSpeedEnemy = 1f;

        private const int DefaultWariorAttackPoints = 80;
        private const int DefaultWariorHealthPoints = 80;
        private const int DefaultWariorDefence = 40;

        // Animation values
        private const int DefaultGargamelDirectionCropStay = 0;
        private const int DefaultGargamelDirectionNorth = 440;
        private const int DefaultGargamelDirectionSouth = 15;
        private const int DefaultGargamelDirectionEast = 660;
        private const int DefaultGargamelDirectionWest = 224;
        private const int DefaultGargamelDirectionNorthEast = 550;
        private const int DefaultGargamelDirectionNorthWest = 330;
        private const int DefaultGargamelDirectionSouthEast = 770;
        private const int DefaultGargamelDirectionSouthWest = 122;
        
        public Gargamel(float startPositionX, float startPositionY, int id)
            : base(
                startPositionX, startPositionY, id, DefaultWariorAttackPoints, DefaultWariorHealthPoints,
                DefaultWariorDefence)
        {
            this.coordGargamel = new Vector2(startPositionX - (cropWidth/2f), startPositionY - (cropHeight/2f));
            this.direction = "NW";
            this.cropStay = DefaultGargamelDirectionCropStay;
            this.north = DefaultGargamelDirectionNorth;
            this.south = DefaultGargamelDirectionSouth;
            this.east = DefaultGargamelDirectionEast;
            this.west = DefaultGargamelDirectionWest;
            this.northEast = DefaultGargamelDirectionNorthEast;
            this.northWest = DefaultGargamelDirectionNorthWest;
            this.southEast = DefaultGargamelDirectionSouthEast;
            this.southWest = DefaultGargamelDirectionSouthWest;
        }

        //public Vector2 DetectionEnemyCoord
        //{
        //    get
        //    {
        //        return new Vector2(this.coordGargamel.X + CharacterManager.barbarian.CoordP().X,
        //            this.coordGargamel.Y + CharacterManager.barbarian.CoordP().Y);
        //    }
        //}

        //public override void LoadContent()
        //{
            
        //}

        //public override void UnloadContent()
        //{
            
        //}

        //public override void Update(GameTime gameTime)
        //{
        //    if (!Character.GetIsInBattle && !Character.GetIsInCastle)
        //    {
        //        this.lastAbstractCoord = this.coordGargamel;
        //        float plTopSide = Character.DrawCoordPlayer.Y;
        //        float plBottomSide = Character.DrawCoordPlayer.Y + Character.PlayerCropHeight;
        //        float plLeftSide = Character.DrawCoordPlayer.X;
        //        float plRightSide = Character.DrawCoordPlayer.X + Character.PlayerCropWidth;

        //        bool isPlayerDown = CollisionDetection.IsNear(plTopSide, this.drawCoordEnemy.Y + cropHeight);
        //        bool isPlayerUp = CollisionDetection.IsNear(plBottomSide, this.drawCoordEnemy.Y);
        //        bool isPlayerLeft = CollisionDetection.IsNear(plRightSide, this.drawCoordEnemy.X);
        //        bool isPlayerRight = CollisionDetection.IsNear(plLeftSide, this.drawCoordEnemy.X + cropWidth);

        //        if (isPlayerUp && (isPlayerRight || isPlayerLeft))
        //        {
        //            this.coordGargamel.Y -= CollisionDetection.EnemyGoUp(this.DetectionEnemyCoord, this.drawCoordEnemy, cropHeight,
        //                cropWidth, this.moveSpeedEnemy);
        //        }

        //        if (isPlayerDown && (isPlayerRight || isPlayerLeft))
        //        {
        //            this.coordGargamel.Y += CollisionDetection.EnemyGoDown(this.DetectionEnemyCoord, this.drawCoordEnemy, cropHeight,
        //                cropWidth, this.moveSpeedEnemy);
        //        }

        //        if (isPlayerLeft && (isPlayerUp || isPlayerDown))
        //        {
        //            this.coordGargamel.X -= CollisionDetection.EnemyGoLeft(this.DetectionEnemyCoord, this.drawCoordEnemy, cropHeight,
        //                cropWidth, this.moveSpeedEnemy);
        //        }

        //        if (isPlayerRight && (isPlayerUp || isPlayerDown))
        //        {
        //            this.coordGargamel.X += CollisionDetection.EnemyGoRight(this.DetectionEnemyCoord, this.drawCoordEnemy, cropHeight,
        //                cropWidth, this.moveSpeedEnemy);
        //        }

        //        this.drawCoordEnemy.X = this.coordGargamel.X + CharacterManager.barbarian.CoordP().X;
        //        this.drawCoordEnemy.Y = this.coordGargamel.Y + CharacterManager.barbarian.CoordP().Y;

        //        /// Re-Write new position on the screen of enemy  
        //        CharacterManager.EnemiesPositionList[ID] = this.drawCoordEnemy;

        //        this.frameCounter += (int) gameTime.ElapsedGameTime.TotalMilliseconds;

        //        if (this.frameCounter >= this.switchCounter)
        //        {
        //            this.frameCounter = 0;

        //            this.returnedValue = Animation.SpriteSheetAnimation(this.lastAbstractCoord, this.coordGargamel,
        //                this.direction, this.cropFrame, cropWidth, cropHeight, this.cropStay, this.south, this.north, this.west, this.east, this.southWest,
        //                this.southEast, this.northWest, this.northEast);

        //            this.cropCurrentFrame = this.returnedValue.ImageCrop;
        //            this.direction = this.returnedValue.Direction;

        //            this.cropFrame++;

        //            if (this.cropFrame == 4)
        //            {
        //                this.cropFrame = 0;
        //            }
        //        }

        //    }
        //    else if (Character.GetIsInCastle)
        //    {

        //    }
        //    else if (!Character.GetIsInBattle)
        //    {

        //    }


        //    // Check if the mouse position is inside the rectangle
        //    // fix it !

        //    //var mouseState = Mouse.GetState();
        //    //var mousePosition = coordGargamel;
        //    //Rectangle area = new Rectangle(new Point((int)coordGargamel.X, (int)drawCoordEnemy.X), new Point((int)coordGargamel.Y, (int)drawCoordEnemy.Y));

        //    /// fix the problem !!!


        //    //if (area.Contains(mousePosition))
        //    //{
        //    //    if (mouseState.LeftButton == ButtonState.Pressed)
        //    //    {
        //    //        moveSpeedEnemy = 0f;
        //    //        //isAlive = false;

        //    //    }
        //    //}

        //}

        //public override void Draw(SpriteBatch spriteBatch)
        //{
        //    if (!Character.GetIsInBattle && !Character.GetIsInCastle)
        //    {
        //        spriteBatch.Draw(CharacterManager.Instance.GargamelImage, this.drawCoordEnemy, this.CropCurrentFrame, Color.White);
        //    }
        //    else if (Character.GetIsInCastle)
        //    {

        //    }
        //    else if (Character.GetIsInBattle)
        //    {

        //    }
        //}
    }
}
