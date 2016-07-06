using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using AthameRPG.Controls;
using AthameRPG.GameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AthameRPG.Characters.WarUnits
{
    public abstract class WarUnit //: IComparable<WarUnit>
    {
        protected const int MinFrameSwitch = 100;
        protected const string SmallLettersPath = "../Content/Fonts/SmallLetters";
        protected const float smallLetterCoordX = 395;
        protected const float smallLetterCoordY = 580;

        //public static bool isPlayerTurnInBattle;

        protected int strengthLevel;
        protected Texture2D warUnitImage;
        protected string imagePath;
        protected Vector2 warUnitDrawCoord;
        protected Vector2 lastDrawCoord;
        protected Vector2 wantedPosition;
        protected Rectangle cropCurrentFrame;
        protected SpriteEffects warUnitEffect;
        //protected SpriteEffects battlefieldEffect;
        protected bool playerUnit;
        protected bool amIArcherOrMage;
        internal bool isChoosen;
        internal bool inBattleTurn;
        internal bool HaveActionForCurrentTurn;
        

        protected int frameCounter;
        protected int switchCounter;
        protected int cropFrame;
        protected int cropStayRow;
        protected int cropMovingRow;
        protected int cropAttackRow;
        protected int cropStayWidth;
        protected int cropStayHeight;
        protected int cropMoveWidth;
        protected int cropMoveHeight;
        protected int cropAttackWidth;
        protected int cropAttackHeight;
        protected int correctionStayCropByX;

        
        protected SpriteFont spriteFontSmallLetters;
        protected Vector2 smallLettersDrawCoord;
        protected AnimationReturnedValue newAnimationFrame;
        protected MouseState newMouseState;
        protected MouseState oldMouseState;
        protected WarUnit supportUnit;

        protected float moveSpeed;
        protected double availableMove;
        protected int health;
        protected bool isAlive;
        protected int damage;
        protected int minAttackDistance;




        public WarUnit()
        {
            this.playerUnit = false;
            this.switchCounter = MinFrameSwitch;
            this.warUnitEffect = SpriteEffects.FlipHorizontally;
            this.smallLettersDrawCoord = new Vector2(smallLetterCoordX, smallLetterCoordY);
            this.inBattleTurn = true;
            this.isAlive = true;
            
        }

        public WarUnit(bool playerUnit)
        {
            this.playerUnit = playerUnit;
            this.switchCounter = MinFrameSwitch;
            this.smallLettersDrawCoord = new Vector2(smallLetterCoordX, smallLetterCoordY);
            this.inBattleTurn = true;
            this.isAlive = true;
        }

        public abstract int GetDefaultHeаlth();

        public int Damage
        {
            get { return this.damage; }
        }

        public int Health
        {
            get { return this.health; }
        }

        public bool IsAlive
        {
            get { return this.isAlive; }
        }

        public int DecreaseHealth(int damage)
        {
            this.health -= damage;

            if (this.health < 0)
            {
                this.health = this.GetDefaultHeаlth() - Math.Abs(this.health);

                return 1;
            }
            return 0;
        }

        public bool CanBeSeleted { get; set; }

        public Vector2 WarUnitDrawCoord
        {
            get { return this.warUnitDrawCoord; }
        }

        public virtual void LoadContent(ContentManager content)
        {
            this.warUnitImage = content.Load<Texture2D>(this.imagePath);

            this.spriteFontSmallLetters = content.Load<SpriteFont>(SmallLettersPath);

            this.cropCurrentFrame = new Rectangle(120,0,69,100);
             
        }

        public virtual void Update(GameTime gameTime)
        {
           
            this.lastDrawCoord = this.warUnitDrawCoord;

            this.SelectAndMove(gameTime);
            
            this.frameCounter += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (this.frameCounter >= this.switchCounter)
            {
                this.frameCounter = 0;
                
                this.newAnimationFrame = Animation.BattlefieldAnimation(this.lastDrawCoord, this.warUnitDrawCoord,
                        this.cropStayRow, this.cropMovingRow, this.cropAttackRow, this.cropFrame, cropStayWidth, cropStayHeight, this.correctionStayCropByX);

                this.cropCurrentFrame = this.newAnimationFrame.ImageCrop;

                this.cropFrame++;

                if (this.cropFrame == 4)
                {
                    this.cropFrame = 0;
                }
            }

            if (!this.inBattleTurn)
            {
                this.isChoosen = false;
            }

            if (this.availableMove <= 0)
            {
                this.isChoosen = false;
                this.inBattleTurn = false;
            }
        }
        
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (this.playerUnit)
            {
                
                if (this.isChoosen)
                {
                    spriteBatch.Draw(this.warUnitImage, this.warUnitDrawCoord, this.cropCurrentFrame, Color.Red);
                    spriteBatch.DrawString(this.spriteFontSmallLetters, string.Format("{0:F0}", this.availableMove > 0 ? this.availableMove : 0),
                    this.smallLettersDrawCoord, Color.Red);
                }
                else
                {
                    spriteBatch.Draw(this.warUnitImage, this.warUnitDrawCoord, this.cropCurrentFrame, Color.White);
                }

            }
            else
            {
                //spriteBatch.Draw(this.warUnitImage, this.cropCurrentFrame, new Rectangle?(), Color.White, 0.0f, this.warUnitDrawCoord, this.warUnitEffect, 0.0f );
                
                spriteBatch.Draw(this.warUnitImage, // The Texture2D
                 new Rectangle((int)this.warUnitDrawCoord.X, (int)this.warUnitDrawCoord.Y, this.cropStayWidth, this.cropStayHeight), // This rectangle positions the texture on the screen and scales it can also be a Vector2
                 this.cropCurrentFrame, 
                 Color.White,
                 0f,
                 new Vector2(0,0), // why this 0,0 i don't know... with this.warUnitDrawCoord it doesn't work 
                 this.warUnitEffect,
                 0f);
            }
        }

        private void SelectAndMove(GameTime gameTime)
        {
            this.oldMouseState = this.newMouseState;
            this.newMouseState = Mouse.GetState();

            IsAnotherSelectedUnit();

            if (CollisionDetection.IsMouseOverObject(this.warUnitDrawCoord, cropStayWidth, cropStayHeight, gameTime)
                && this.newMouseState.RightButton == ButtonState.Pressed &&
                this.oldMouseState.RightButton == ButtonState.Released
                && this.playerUnit && this.CanBeSeleted)
            {
                if (!this.isChoosen && this.IsAnotherSelectedUnit())
                {
                    this.isChoosen = true;
                    this.wantedPosition = this.warUnitDrawCoord;

                }
                else
                {
                    this.isChoosen = false;
                }
            }

            if (this.isChoosen)
            {
                // this.newMouseState.LeftButton == ButtonState.Pressed && this.oldMouseState.LeftButton == ButtonState.Released
                // MouseExtended.Current.WasDoubleClick(MouseButton.Left)
                if (MouseExtended.Current.WasDoubleClick(MouseButton.Left))
                {
                    this.wantedPosition.X = MouseExtended.Current.CurrentState.X - (cropStayWidth / 2);
                    this.wantedPosition.Y = MouseExtended.Current.CurrentState.Y - (cropStayHeight / 2);
                }

                this.Moving();
            }
        }

        private void Moving()
        {
            if (this.availableMove > 0)
            {
                if (this.warUnitDrawCoord.X < this.wantedPosition.X)
                {
                    this.warUnitDrawCoord.X += this.moveSpeed;
                }
                if (this.warUnitDrawCoord.X > this.wantedPosition.X)
                {
                    this.warUnitDrawCoord.X -= this.moveSpeed;
                }
                if (this.warUnitDrawCoord.Y > this.wantedPosition.Y)
                {
                    this.warUnitDrawCoord.Y -= this.moveSpeed;
                }
                if (this.warUnitDrawCoord.Y < this.wantedPosition.Y)
                {
                    this.warUnitDrawCoord.Y += this.moveSpeed;
                }

                this.availableMove -= CollisionDetection.CalculateDistanceTravelled(this.lastDrawCoord, this.WarUnitDrawCoord);
            }
        }

        private bool IsAnotherSelectedUnit()
        {
            foreach (var availableCreature in CharacterManager.barbarian.AvailableCreatures)
            {
                if (availableCreature.Key.isChoosen)
                {
                    return false;
                }
            }
            return true;
        }

        public int GetStrengthLevel
        {
            get {return this.strengthLevel; }
        }

        public void MoveInBattle() // attack ? ? ?
        {
            Predicate<WarUnit> isThereAnArcher = x => x.amIArcherOrMage == true;

            bool hasArcherFriend = false;
            bool hasArcherEnemy = MapManager.Instance.Battlefield.CheckPlayerArmy(isThereAnArcher);
            bool isCloseEnough = false;

            if (!this.amIArcherOrMage)
            {
                hasArcherFriend = MapManager.Instance.Battlefield.CheckEnemyArmy(isThereAnArcher);
            }
            else
            {
                // attack enemy archer
            }  
            


            if (this.availableMove > 0)
            {
                if (hasArcherFriend)
                {
                    ProtectFriendArcher();
                }
                else if (!hasArcherFriend && hasArcherEnemy)
                {
                    SearchForArcher();
                }
                else if (!hasArcherFriend && !hasArcherEnemy)
                {
                    // search enemy in some radius
                    this.SetProtectedMove();
                    this.Moving();
                }

            }

        }

        private void SearchForArcher()
        {
            this.supportUnit = MapManager.Instance.Battlefield.TryTakeEnemyUnit(x => x.amIArcherOrMage == true);

            if (this.supportUnit == null)
            {
                this.inBattleTurn = false;
            }
            // проверява дали може да стигне противника с оставащият му ход 
            if (
                CollisionDetection.IsNear(this.WarUnitDrawCoord.X,
                    this.supportUnit.WarUnitDrawCoord.X + this.supportUnit.cropStayWidth, (int) this.availableMove) &&
                this.inBattleTurn == true)
            {
                this.SetMoveToEnemy(this.supportUnit);
                this.Moving();
                this.EnemyTryAttackPlayer(this, this.supportUnit);
            }
            else
            {
                this.SetProtectedMove();
                this.Moving();
            }
        }

        private void EnemyTryAttackPlayer(WarUnit attacker, WarUnit defender)
        {
            // default attack min distance

            if (CollisionDetection.IsNear(attacker.WarUnitDrawCoord.X, defender.WarUnitDrawCoord.X, this.minAttackDistance) 
                && CollisionDetection.IsNear(attacker.WarUnitDrawCoord.Y, defender.WarUnitDrawCoord.Y, this.minAttackDistance) )
            {
                decimal attackerQuantity = MapManager.Instance.Battlefield.TryTakeFriendUnitQuantity(attacker);
                decimal enemyQuantity = MapManager.Instance.Battlefield.TryTakeEnemmyUnitQuantity(defender);

                if (attacker.inBattleTurn)
                {
                    MapManager.Instance.Battlefield.AttackPlayerUnit(defender, (int)(attacker.Damage * attackerQuantity), attacker, (int)(defender.Damage * enemyQuantity));
                }
                
                attacker.inBattleTurn = false;
            }
            
        }
        
        private void SetProtectedMove()
        {
            // make default protected move;
            if (this.HaveActionForCurrentTurn)
            {
                this.wantedPosition = new Vector2(this.WarUnitDrawCoord.X - 90, this.WarUnitDrawCoord.Y);
                this.HaveActionForCurrentTurn = false;
            }
            else
            {
                if (this.wantedPosition == this.WarUnitDrawCoord)
                {
                    this.inBattleTurn = false;
                }
            }
            
        }

        private void ProtectFriendArcher()
        {
            this.supportUnit = MapManager.Instance.Battlefield.TryTakeFriendUnit(x => x.amIArcherOrMage == true);

            if (this.supportUnit == null)
            {
                this.inBattleTurn = false;
            }
            // make default radius;

            if (CollisionDetection.IsNear(this.WarUnitDrawCoord.Y, this.supportUnit.WarUnitDrawCoord.Y, 10)
                && CollisionDetection.IsNear(this.WarUnitDrawCoord.X + this.cropStayWidth, this.supportUnit.WarUnitDrawCoord.X, 10)
                && this.inBattleTurn == true)
            {
                this.inBattleTurn = false;
            }
            else
            {
                this.ProtectFriend(this.supportUnit);
                this.Moving();
            }
        }

        protected void ProtectFriend(WarUnit unit)
        {
            //if (this.WarUnitDrawCoord.X > unit.WarUnitDrawCoord.X)
            //{
            //    this.wantedPosition = new Vector2(unit.WarUnitDrawCoord.X + unit.cropStayWidth, unit.WarUnitDrawCoord.Y);
            //}
            //else
            //{
            //    this.wantedPosition = new Vector2(unit.WarUnitDrawCoord.X, unit.warUnitDrawCoord.Y);
            //}

            this.wantedPosition = new Vector2(unit.WarUnitDrawCoord.X - this.cropStayWidth, unit.warUnitDrawCoord.Y);
        }

        protected void SetMoveToEnemy(WarUnit warUnit)
        {
            //if (this.WarUnitDrawCoord.X > warUnit.WarUnitDrawCoord.X)
            //{
            //    this.wantedPosition = new Vector2(warUnit.WarUnitDrawCoord.X, warUnit.WarUnitDrawCoord.Y);
            //}
            //else
            //{
            //    this.wantedPosition = new Vector2(warUnit.WarUnitDrawCoord.X , warUnit.warUnitDrawCoord.Y);
            //}
            this.wantedPosition = new Vector2(warUnit.WarUnitDrawCoord.X, warUnit.WarUnitDrawCoord.Y);

        }
        
        public void ReFillAvailableMove()
        {
            this.availableMove = this.GetDefaultMove();
        }

        public abstract void SetStartPositionInBattleLikePlayer();
        public abstract void SetStartPositionInBattleLikeEnemy();

        protected abstract float GetDefaultMove();

        //public static bool operator ==(WarUnit firstUnit, WarUnit secondUnit)
        //{
        //    return firstUnit.GetType().Name == secondUnit.GetType().Name;
        //}

        //public static bool operator !=(WarUnit firstUnit, WarUnit secondUnit)
        //{
        //    return firstUnit.GetType().Name != secondUnit.GetType().Name;
        //}

        //public int CompareTo(WarUnit other)
        //{
        //    int result = 0;

        //    if (this.GetType().Name == other.GetType().Name)
        //    {
        //        return this.GetType().Name.CompareTo(other.GetType().Name);
        //    }

        //    return other.GetType().Name.CompareTo(this.GetType().Name);

        //}
    }
}
