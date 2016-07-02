using AthameRPG.Controls;
using Microsoft.Xna.Framework;
using AthameRPG.GameEngine;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AthameRPG.Characters
{
    public abstract class Enemy : Unit
    {
        protected const int ViewRadius = 150;
        protected const int EnemySearchRadius = 120;
        protected const int BattleRadius = 5;
        protected const string SmallLettersPath = "../Content/Fonts/SmallLetters";
        protected const string BigLettersPath = "../Content/Fonts/ArialBig";
        protected const string InvitationForBattle = "Click twice over Enemy for battle.";
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
        protected int viewRadius;
        protected bool inView;
        protected int enemySearchRadius;
        protected int indexCounterSupport;
        protected static SpriteFont spriteFontSmallLetters;
        protected static SpriteFont bigLetters;
        protected bool mouseOverEnemy;
        protected bool iSeePlayer;
        protected bool showInvitationText;
        protected Vector2 invitationTextCoord;
        
        private int id;//  NUMBER IN THE ENEMY LIST //

        //public Enemy(float startPositionX, float startPositionY, int atack, int health, int defence) : base(startPositionX, startPositionY, atack, health, defence)
        //{
            
        //}
        
        public Enemy(float startPositionX, float startPositionY, int id, int atack, int health, int defence)
            : base(startPositionX, startPositionY, atack, health, defence)
        {
            this.ID = id;
            this.viewRadius = ViewRadius;
            this.enemySearchRadius = EnemySearchRadius;
            this.cropCurrentFrame = new Rectangle(this.cropStay, this.south, cropWidth, cropHeight);
            this.CropCurrentFrame = cropCurrentFrame;
            //this.invitationTextCoord =
            //    new Vector2(ScreenManager.SCREEN_WIDTH - bigLetters.MeasureString(InvitationForBattle).X, 5);
        }

        public override void LoadContent(ContentManager content)
        {
            spriteFontSmallLetters = content.Load<SpriteFont>(SmallLettersPath);
            bigLetters = content.Load<SpriteFont>(BigLettersPath);
            this.invitationTextCoord =
                new Vector2(ScreenManager.SCREEN_WIDTH - spriteFontSmallLetters.MeasureString(InvitationForBattle).X, 5);
        }

        public override void UnloadContent()
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (!Character.GetIsInBattle && !Character.GetIsInCastle)
            {
                this.iSeePlayer = false;

                this.lastAbstractCoord = this.coordGargamel;

                float plTopSide = Character.DrawCoordPlayer.Y;
                float plBottomSide = Character.DrawCoordPlayer.Y + Character.PlayerCropHeight;
                float plLeftSide = Character.DrawCoordPlayer.X;
                float plRightSide = Character.DrawCoordPlayer.X + Character.PlayerCropWidth;

                float enemyTop = this.drawCoordEnemy.Y;
                float enemyBottom = this.drawCoordEnemy.Y + cropHeight;
                float enemyLeft = this.drawCoordEnemy.X;
                float enemyRight = this.drawCoordEnemy.X + cropWidth;

                bool isPlayerUp = CollisionDetection.IsNear(plBottomSide, this.drawCoordEnemy.Y, this.enemySearchRadius);
                bool isPlayerDown = CollisionDetection.IsNear(plTopSide, this.drawCoordEnemy.Y + cropHeight, this.enemySearchRadius);
                bool isPlayerLeft = CollisionDetection.IsNear(plRightSide, this.drawCoordEnemy.X, this.enemySearchRadius);
                bool isPlayerRight = CollisionDetection.IsNear(plLeftSide, this.drawCoordEnemy.X + cropWidth, this.enemySearchRadius);

                if (!CharacterManager.itIsPlayerTurn && this.availableMove > 0)
                {
                    
                    EnemyMoving(isPlayerUp, isPlayerRight, isPlayerLeft, isPlayerDown);
                    
                    this.availableMove -= CollisionDetection.CalculateDistanceTravelled(this.lastAbstractCoord,
                        this.coordGargamel);
                }


                this.drawCoordEnemy.X = this.coordGargamel.X + CharacterManager.barbarian.CoordP().X;
                this.drawCoordEnemy.Y = this.coordGargamel.Y + CharacterManager.barbarian.CoordP().Y;


                CheckForBattle(plTopSide, enemyBottom, plLeftSide, enemyRight, plRightSide, enemyLeft, plBottomSide, enemyTop, gameTime);


                // Re-Write new position of enemy on the screen
                CharacterManager.EnemiesPositionList[ID] = this.drawCoordEnemy;

                MakeCurrentAnimationFrame(gameTime);

                ShowEnemyArmy(plTopSide, plBottomSide, plLeftSide, plRightSide);

                if (this.lastAbstractCoord != this.coordGargamel)
                {
                    this.iSeePlayer = true;
                }
            }
            else if (Character.GetIsInCastle)
            {

            }
            else if (Character.GetIsInBattle)
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

        private void CheckForBattle(float plTopSide, float enemyBottom, float plLeftSide, float enemyRight, float plRightSide,
            float enemyLeft, float plBottomSide, float enemyTop, GameTime gameTime)
        {
            bool isUp = CollisionDetection.HaveCollisionWithCurrentRadius(plTopSide, enemyBottom, plLeftSide,
                enemyRight, plRightSide, enemyLeft, BattleRadius);


            bool isDown = CollisionDetection.HaveCollisionWithCurrentRadius(plBottomSide, enemyTop, plLeftSide,
                enemyRight, plRightSide, enemyLeft, BattleRadius);

            bool isLeft = CollisionDetection.HaveCollisionWithCurrentRadius(plLeftSide, enemyRight, plTopSide,
                enemyBottom, plBottomSide, enemyTop, BattleRadius);

            bool isRight = CollisionDetection.HaveCollisionWithCurrentRadius(plRightSide, enemyLeft, plTopSide,
                enemyBottom, plBottomSide, enemyTop, BattleRadius);

            if (isUp || isDown || isRight || isLeft)
            {
                
                if (CharacterManager.itIsPlayerTurn)
                {
                    this.showInvitationText = true;

                    if (CollisionDetection.IsMouseOverObject(this.drawCoordEnemy, cropWidth, cropHeight, gameTime))
                    {
                        if (MouseExtended.Current.WasDoubleClick(MouseButton.Left))
                        {
                            Character.isInBattle = true;
                            this.showInvitationText = false;
                        }
                    }
                }
                else
                {
                    Character.isInBattle = true;
                    this.showInvitationText = false;
                }
            }
        }


        public bool ISeePlayer
        {
            get { return this.iSeePlayer; }
        }

        private void ShowEnemyArmy(float plTopSide, float plBottomSide, float plLeftSide, float plRightSide)
        {
            // is enemy near us ... if is near we can see it's army

            this.mouseOverEnemy = MouseExtended.Current.CurrentState.X > this.drawCoordEnemy.X &&
                                  MouseExtended.Current.CurrentState.X < (this.drawCoordEnemy.X + cropWidth) &&
                                  MouseExtended.Current.CurrentState.Y > this.drawCoordEnemy.Y &&
                                  MouseExtended.Current.CurrentState.Y < (this.drawCoordEnemy.Y + cropHeight);

            this.inView =
                ((CollisionDetection.IsNear(plTopSide, this.drawCoordEnemy.Y + cropHeight, this.viewRadius)) ||
                 (CollisionDetection.IsNear(plBottomSide, this.drawCoordEnemy.Y, this.viewRadius))) &&
                ((CollisionDetection.IsNear(plLeftSide, this.drawCoordEnemy.X + cropWidth, this.viewRadius)) ||
                 (CollisionDetection.IsNear(plRightSide, this.drawCoordEnemy.X, this.viewRadius))) &&
                this.mouseOverEnemy; // && MouseExtended.Current.WasSingleClick(MouseButton.Right);
        }

        private void MakeCurrentAnimationFrame(GameTime gameTime)
        {
            this.frameCounter += (int) gameTime.ElapsedGameTime.TotalMilliseconds;

            if (this.frameCounter >= this.switchCounter)
            {
                this.frameCounter = 0;

                this.returnedValue = Animation.SpriteSheetAnimation(this.lastAbstractCoord, this.coordGargamel,
                    this.direction, this.cropFrame, cropWidth, cropHeight, this.cropStay, this.south, this.north, this.west,
                    this.east, this.southWest,
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

        private void EnemyMoving(bool isPlayerUp, bool isPlayerRight, bool isPlayerLeft, bool isPlayerDown)
        {
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
                this.coordGargamel.X += CollisionDetection.EnemyGoRight(this.DetectionEnemyCoord, this.drawCoordEnemy,
                    cropHeight,
                    cropWidth, this.moveSpeedEnemy);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!Character.GetIsInBattle && !Character.GetIsInCastle)
            {
                spriteBatch.Draw(CharacterManager.Instance.GargamelImage, this.drawCoordEnemy, this.CropCurrentFrame, Color.White);

                if (this.inView)
                {
                    this.indexCounterSupport = 25;
                    foreach (var creature in this.availableCreatures)
                    {
                        spriteBatch.DrawString(spriteFontSmallLetters, creature.Key.GetType().Name + ": " + creature.Value,
                        new Vector2(5, this.indexCounterSupport), Color.Red);
                        this.indexCounterSupport += 20;
                    }
                }
                if (this.showInvitationText)
                {
                    spriteBatch.DrawString(spriteFontSmallLetters, InvitationForBattle,
                        this.invitationTextCoord, Color.Red);

                    this.showInvitationText = false;
                    
                }
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
