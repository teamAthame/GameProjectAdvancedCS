using System;
using AthameRPG.Controls;
using AthameRPG.GameEngine;
using AthameRPG.Objects.Weapons;
using AthameRPG.Objects.Weapons.Arrows;
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
        private Color color;


        protected int frameCounter;
        protected int switchCounter;
        protected int cropFrame;
        
        protected int cropWidth;
        protected int cropHeight;

        protected Rectangle[] cropStay;
        protected Rectangle[] cropMove;
        protected Rectangle[] cropAttack;
        protected Rectangle[] cropHit;
        
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
        protected int attackAnywayDistance;
        protected int protectedStep;
        protected bool IsAttackable;
        protected bool isAttacked;

        protected Arrow arrow;
        protected WarUnit attacker;
        protected Weapon weapon;
        protected Arrow attackerArrow;


        public WarUnit()
        {
            this.playerUnit = false;
            this.switchCounter = MinFrameSwitch;
            this.warUnitEffect = SpriteEffects.FlipHorizontally;
            this.smallLettersDrawCoord = new Vector2(smallLetterCoordX, smallLetterCoordY);
            this.inBattleTurn = true;
            this.isAlive = true;

            this.LoadDefaultUnitStats();
        }

        public WarUnit(bool playerUnit)
        {
            this.playerUnit = playerUnit;
            this.switchCounter = MinFrameSwitch;
            this.smallLettersDrawCoord = new Vector2(smallLetterCoordX, smallLetterCoordY);
            this.inBattleTurn = true;
            this.isAlive = true;

            this.LoadDefaultUnitStats();
        }
        
        public Color Color
        {
            get { return this.color; }
            protected set { this.color = value; }
        }

        public bool AmIArcherOrMage
        {
            get { return this.amIArcherOrMage; }
        }

        public int CropWidth
        {
            get { return this.cropWidth; }
        }

        public int CropHeight
        {
            get { return this.cropHeight; }
        }

        public bool IsArrowHasTarget
        {
            get
            {
                if (this.arrow != null)
                {
                    return this.arrow.HasTarget;
                }
                else
                {
                    return false;
                }
            }
        }

        public int MinAttackDistance
        {
            get { return this.minAttackDistance; }
        }

        public abstract void SetStartPositionInBattleLikePlayer();

        public abstract void SetStartPositionInBattleLikeEnemy();

        protected abstract float GetDefaultMove();

        protected virtual void LoadDefaultUnitStats()
        {
            this.Color = Color.White;
            
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

        public bool CanBeSeleted { get; set; }

        public Vector2 WarUnitDrawCoord
        {
            get { return this.warUnitDrawCoord; }
        }

        public virtual void LoadContent(ContentManager content)
        {
            this.warUnitImage = content.Load<Texture2D>(this.imagePath);

            this.spriteFontSmallLetters = content.Load<SpriteFont>(SmallLettersPath);

            this.cropCurrentFrame = new Rectangle(120, 0, 69, 100);

        }

        public virtual void Update(GameTime gameTime)
        {
            if (this.playerUnit)
            {
                this.lastDrawCoord = this.warUnitDrawCoord;
            }

            this.CheckForHitFromArrow();

            this.SelectAndMove(gameTime);

            this.frameCounter += (int) gameTime.ElapsedGameTime.TotalMilliseconds;

            if (this.frameCounter >= this.switchCounter)
            {
                this.FlashIfCanBeSelectedInBattle();

                this.UpdateArrow(gameTime);

                this.frameCounter = 0;

                //this.newAnimationFrame = Animation.BattlefieldAnimation(this.lastDrawCoord, this.warUnitDrawCoord,
                //    this.cropStayRow, this.cropMovingRow, this.cropAttackRow, this.cropFrame, this.cropStayWidth,
                //    this.cropStayHeight, this.cropMoveWidth, this.cropMoveHeight, this.cropAttackWidth, this.cropAttackHeight,
                //    this.correctionStayCropByX, this.correctionMoveByX, this.correctionAttackByX);

                this.newAnimationFrame = Animation.BattlefieldAnimation(this.lastDrawCoord, this.warUnitDrawCoord,
                    this.cropStay, this.cropMove, this.cropAttack, this.cropHit, this.cropFrame);

                this.cropCurrentFrame = this.newAnimationFrame.ImageCrop;

                this.cropFrame++;

                if (this.cropFrame == 4)
                {
                    this.cropFrame = 0;
                }
            }

            if (!this.playerUnit)
            {
                this.lastDrawCoord = this.warUnitDrawCoord;
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

        private void CheckForHitFromArrow()
        {
            if (this.isAttacked)
            {
                this.WaitForHit();
            }
        }

        private void UpdateArrow(GameTime gameTime)
        {
            if (this.arrow != null && this.arrow.HasTarget)
            {
                this.arrow.Update(gameTime);
            }
        }

        private void FlashIfCanBeSelectedInBattle()
        {
            if (this.CanBeSeleted)
            {
                if (this.Color == Color.White)
                {
                    this.Color = Color.Red;
                }
                else
                {
                    this.Color = Color.White;
                }
            }
            else
            {
                this.Color = Color.White;
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (this.playerUnit)
            {

                if (this.isChoosen)
                {
                    spriteBatch.Draw(this.warUnitImage, this.warUnitDrawCoord, this.cropCurrentFrame, Color.Red);
                    spriteBatch.DrawString(this.spriteFontSmallLetters,
                        string.Format("{0:F0}", this.availableMove > 0 ? this.availableMove : 0),
                        this.smallLettersDrawCoord, Color.Red);
                }
                else
                {
                    spriteBatch.Draw(this.warUnitImage, this.warUnitDrawCoord, this.cropCurrentFrame, this.Color);
                }

            }
            else
            {
                //spriteBatch.Draw(this.warUnitImage, this.cropCurrentFrame, new Rectangle?(), Color.White, 0.0f, this.warUnitDrawCoord, this.warUnitEffect, 0.0f );

                if (this.IsAttackable)
                {
                    spriteBatch.Draw(this.warUnitImage, // The Texture2D
                        new Rectangle((int) this.warUnitDrawCoord.X, (int) this.warUnitDrawCoord.Y, this.cropWidth,
                            this.cropHeight),
                        // This rectangle positions the texture on the screen and scales it can also be a Vector2
                        this.cropCurrentFrame,
                        Color.Blue,
                        0f,
                        new Vector2(0, 0), // why this 0,0 i don't know... with this.warUnitDrawCoord it doesn't work 
                        this.warUnitEffect,
                        0f);
                }
                else
                {
                    spriteBatch.Draw(this.warUnitImage, // The Texture2D
                        new Rectangle((int) this.warUnitDrawCoord.X, (int) this.warUnitDrawCoord.Y, this.cropWidth,
                            this.cropHeight),
                        // This rectangle positions the texture on the screen and scales it can also be a Vector2
                        this.cropCurrentFrame,
                        this.Color,
                        0f,
                        new Vector2(0, 0), // why this 0,0 i don't know... with this.warUnitDrawCoord it doesn't work 
                        this.warUnitEffect,
                        0f);
                }
            }

            if (this.arrow != null && this.arrow.HasTarget)
            {
                this.arrow.Draw(spriteBatch);
            }
            
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

        private void SelectAndMove(GameTime gameTime)
        {
            this.oldMouseState = this.newMouseState;
            this.newMouseState = Mouse.GetState();

            IsAnotherSelectedUnit();

            if (CollisionDetection.IsMouseOverObject(this.warUnitDrawCoord, cropWidth, cropHeight, gameTime)
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
                this.SetMoveTo();

                this.Moving();

                this.PlayerAttack(gameTime);
            }
        }

        private void SetMoveTo()
        {
            if (MouseExtended.Current.WasDoubleClick(MouseButton.Left))
            {
                this.wantedPosition.X = MouseExtended.Current.CurrentState.X - (cropWidth/2);
                this.wantedPosition.Y = MouseExtended.Current.CurrentState.Y - (cropHeight/2);
            }
        }

        private void WaitForHit()
        {
            if (CollisionDetection.HaveCollisonBetweenTwoObj(this, this.attackerArrow))
            {
                if (this.attacker.playerUnit)
                {
                    MapManager.Instance.Battlefield.TryToAttackEnemyUnit(this.attacker, this, this.attackerArrow);
                    
                }
                else
                {
                    this.EnemyTryAttackPlayer(this.attacker, this, this.attackerArrow);
                }
                this.isAttacked = false;
            }
   
        }

        private void SetAttacker(WarUnit attacker, Arrow attackerArrow)
        {
            this.attacker = attacker;
            this.attackerArrow = attackerArrow;
        }

        private void PlayerAttack(GameTime gameTime)
        {
            var enemies = MapManager.Instance.Battlefield.TryTakeEnemyArmy();

            foreach (var enemy in enemies.Keys)
            {
                if (CollisionDetection.IsMouseOverObject(enemy.warUnitDrawCoord,
                    enemy.cropWidth, enemy.cropHeight, gameTime) 
                    && this.CanIAttack(enemy) && this.inBattleTurn)
                {
                    enemy.IsAttackable = true;

                    if (MouseExtended.Current.WasDoubleClick(MouseButton.Left))
                    {
                        if (this.AmIArcherOrMage)
                        {
                            // add arrow/weapon to damage
                            /*
                             * enemy.isAttacked -> this method will execute when have collision ... 
                             * isAttacked
                             * ExecuteAttack
                             */
                            
                            //MapManager.Instance.Battlefield.TryToAttackEnemyUnit(this, enemy);

                            this.arrow.FlyTo(this.WarUnitDrawCoord, enemy.warUnitDrawCoord, this.warUnitImage);
                            enemy.isAttacked = true;
                            enemy.SetAttacker(this, arrow);
                            //enemy.WaitForHit();
                            this.availableMove = 0;

                            enemy.IsAttackable = false;
                        }
                        else
                        {
                            bool inFrontOfUs =
                                CollisionDetection.IsNear(this.WarUnitDrawCoord.X + this.CropWidth,
                                    enemy.WarUnitDrawCoord.X, this.MinAttackDistance)
                                && CollisionDetection.IsNear(this.WarUnitDrawCoord.Y, enemy.WarUnitDrawCoord.Y,
                                    this.MinAttackDistance);

                            bool behindUs =
                                CollisionDetection.IsNear(this.WarUnitDrawCoord.X,
                                    enemy.WarUnitDrawCoord.X + enemy.CropHeight, this.MinAttackDistance)
                                &&
                                CollisionDetection.IsNear(this.WarUnitDrawCoord.Y, enemy.WarUnitDrawCoord.Y,
                                    this.MinAttackDistance);

                            if (behindUs || inFrontOfUs)
                            {
                                MapManager.Instance.Battlefield.TryToAttackEnemyUnit(this, enemy, this.weapon);
                                enemy.IsAttackable = false;
                            }
                            else
                            {
                                this.wantedPosition.X = MouseExtended.Current.CurrentState.X - (cropWidth / 2);
                                this.wantedPosition.Y = MouseExtended.Current.CurrentState.Y - (cropHeight / 2);

                                this.Moving();
                            }
                            
                        }
                    }
                }
                else
                {
                    enemy.IsAttackable = false;
                }
            }

            ///////////////////////////////////////////
            

            //// check if WE can reach nearest enemy 
            //this.supportUnit = MapManager.Instance.Battlefield.TryTakeNearestEnemyUnit(this,
            //    supportRange);

            //if (this.supportUnit != null)
            //{
            //    bool inFrontOfUs =
            //        CollisionDetection.IsNear(this.WarUnitDrawCoord.X + this.CropWidth,
            //            this.supportUnit.WarUnitDrawCoord.X, this.MinAttackDistance)
            //        && CollisionDetection.IsNear(this.WarUnitDrawCoord.Y, this.supportUnit.WarUnitDrawCoord.Y,
            //            this.MinAttackDistance);

            //    bool behindUs =
            //        CollisionDetection.IsNear(this.WarUnitDrawCoord.X,
            //            this.supportUnit.WarUnitDrawCoord.X + this.supportUnit.CropHeight, this.MinAttackDistance)
            //        &&
            //        CollisionDetection.IsNear(this.WarUnitDrawCoord.Y, this.supportUnit.WarUnitDrawCoord.Y,
            //            this.MinAttackDistance);

            //    if (behindUs || inFrontOfUs)
            //    {
            //        if (MouseExtended.Current.WasDoubleClick(MouseButton.Left))
            //        {
            //            MapManager.Instance.Battlefield.TryToAttackEnemyUnit(this, this.supportUnit);
            //        }
            //    }

            //    if (CollisionDetection.IsMouseOverObject(this.supportUnit.warUnitDrawCoord,
            //        this.supportUnit.cropStayWidth, this.supportUnit.cropStayHeight, gameTime))
            //    {
            //        this.supportUnit.IsAttackable = true;
            //    }
            //    else
            //    {
            //        this.supportUnit.IsAttackable = false;
            //    }


            //}
        }
        
        private bool CanIAttack(WarUnit enemy)
        {
            int supportRange = 0;

            if (this.amIArcherOrMage)
            {
                supportRange = this.MinAttackDistance;
            }
            else
            {
                supportRange = (int)this.availableMove;
            }

            double distanceBetweenAttackerEnemy = CollisionDetection.CalculateDistanceTravelled(this.warUnitDrawCoord, enemy.warUnitDrawCoord);

            if (distanceBetweenAttackerEnemy <= supportRange + this.cropWidth / 2)
            {
                return true;
            }
            else
            {
                return false;
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

                this.availableMove -= CollisionDetection.CalculateDistanceTravelled(this.lastDrawCoord,
                    this.WarUnitDrawCoord);
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
            get { return this.strengthLevel; }
        }

        public void MoveInBattle()
        {

            Predicate<WarUnit> isThereAnArcher = x => x.amIArcherOrMage == true;

            bool hasArcherFriend = false;
            bool hasArcherEnemy = MapManager.Instance.Battlefield.CheckPlayerArmy(isThereAnArcher);
            bool isCloseEnough = false;

            if (!this.amIArcherOrMage)
            {
                hasArcherFriend = MapManager.Instance.Battlefield.CheckEnemyArmy(isThereAnArcher);

                if (this.availableMove > 0)
                {
                    if (hasArcherFriend)
                    {
                        this.supportUnit = MapManager.Instance.Battlefield.TryTakeNearestPlayerUnit(this,
                            this.attackAnywayDistance);

                        if (this.supportUnit == null)
                        {
                            this.ProtectFriendArcher();
                        }
                        else
                        {
                            // move to ... moving to ....  if TARGET is close enough I will attack it 
                            this.SetMoveToEnemy(this.supportUnit);
                            this.Moving();
                            this.EnemyTryAttackPlayer(this, this.supportUnit, this.weapon);
                        }

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
            else
            {
                this.supportUnit = MapManager.Instance.Battlefield.TryTakeNearestPlayerUnit(this, this.MinAttackDistance);

                if (this.supportUnit == null)
                {
                    this.SetProtectedMove();
                    this.Moving();
                }
                else
                {
                    // check if there is another attack with arrow ... when other attack end then this will continue
                    if (MapManager.Instance.Battlefield.CanShoot())
                    {
                        this.arrow.FlyTo(this.WarUnitDrawCoord, this.supportUnit.warUnitDrawCoord, this.warUnitImage);
                        //this.EnemyTryAttackPlayer(this, this.supportUnit);

                        this.supportUnit.isAttacked = true;
                        this.supportUnit.SetAttacker(this, arrow);
                        //this.supportUnit.WaitForHit();
                        this.availableMove = 0;
                    }
                    
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
            
            // check if can reach enemy with current move
            if (
                CollisionDetection.IsNear(this.WarUnitDrawCoord.X,
                    this.supportUnit.WarUnitDrawCoord.X + this.supportUnit.cropWidth, (int) this.availableMove) &&
                this.inBattleTurn == true)
            {
                this.SetMoveToEnemy(this.supportUnit);
                this.Moving();
                this.EnemyTryAttackPlayer(this, this.supportUnit, this.weapon);
            }
            else
            {
                this.SetProtectedMove();
                this.Moving();
            }
        }

        private void EnemyTryAttackPlayer(WarUnit attacker, WarUnit defender, Weapon weapon)
        {
            int weaponDamage = 0;

            if (weapon != null)
            {
                weaponDamage = weapon.Damage;
            }

            if (CollisionDetection.IsNear(attacker.WarUnitDrawCoord.X, defender.WarUnitDrawCoord.X,
                this.minAttackDistance)
                &&
                CollisionDetection.IsNear(attacker.WarUnitDrawCoord.Y, defender.WarUnitDrawCoord.Y,
                    this.minAttackDistance)
                && this.inBattleTurn)
            {
                if (attacker.inBattleTurn)
                {
                    decimal attackerQuantity = MapManager.Instance.Battlefield.TryTakeFriendUnitQuantity(attacker);
                    int attackerDamage = (weaponDamage + attacker.Damage)*(int)attackerQuantity;

                    decimal enemyQuantity = MapManager.Instance.Battlefield.TryTakeEnemmyUnitQuantity(defender);
                    int defenderDamage = (int) (defender.Damage*enemyQuantity);

                    MapManager.Instance.Battlefield.AttackPlayerUnit(defender, attackerDamage, attacker, defenderDamage);
                }

                attacker.inBattleTurn = false;

                this.HaveActionForCurrentTurn = false;
            }

            if (attacker.AmIArcherOrMage)
            {
                if (attacker.inBattleTurn)
                {
                    decimal attackerQuantity = MapManager.Instance.Battlefield.TryTakeFriendUnitQuantity(attacker);
                    int attackerDamage = (weaponDamage + attacker.Damage) * (int)attackerQuantity;

                    decimal enemyQuantity = MapManager.Instance.Battlefield.TryTakeEnemmyUnitQuantity(defender);
                    int defenderDamage = (int)(defender.Damage * enemyQuantity);

                    MapManager.Instance.Battlefield.AttackPlayerUnit(defender, attackerDamage, attacker, defenderDamage);
                }

                attacker.inBattleTurn = false;

                this.HaveActionForCurrentTurn = false;
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
                &&
                CollisionDetection.IsNear(this.WarUnitDrawCoord.X + this.cropWidth,
                    this.supportUnit.WarUnitDrawCoord.X, 10)
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

            //this.wantedPosition = new Vector2(unit.WarUnitDrawCoord.X - this.cropWidth, unit.warUnitDrawCoord.Y);
            this.wantedPosition.X = unit.WarUnitDrawCoord.X - this.cropWidth;
            this.wantedPosition.Y = unit.warUnitDrawCoord.Y;
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
