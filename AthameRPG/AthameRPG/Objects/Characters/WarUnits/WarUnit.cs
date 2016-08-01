using System;
using System.Collections.Generic;
using AthameRPG.Contracts;
using AthameRPG.Controls;
using AthameRPG.Enums;
using AthameRPG.GameEngine.Collisions;
using AthameRPG.GameEngine.Graphics;
using AthameRPG.GameEngine.Loaders;
using AthameRPG.GameEngine.Managers;
using AthameRPG.Objects.Weapons;
using AthameRPG.Objects.Weapons.Arrows;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AthameRPG.Objects.Characters.WarUnits
{
    public abstract class WarUnit : ISoundable, IDraw //: IComparable<WarUnit>
    {
        public virtual event OnEvent OnEvent;
        
        protected const int MinFrameSwitch = 100;
        protected const float smallLetterCoordX = 395;
        protected const float smallLetterCoordY = 580;
        private const int SearchAnyEnemyRadius = 1000;
        private const int DefaultAttackingFrames = 8;

        protected int soundFrameSwitch;
        protected int maxSoundFrameSwitch;

        protected StrenghtLevel strengthLevel;
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
        private bool amIAttacking;
        private int attackingFrames;
        

        protected Arrow arrow;
        protected WarUnit attacker;
        protected Weapon weapon;
        protected Arrow attackerArrow;


        protected WarUnit()
        {
            this.playerUnit = false;
            this.switchCounter = MinFrameSwitch;
            this.warUnitEffect = SpriteEffects.FlipHorizontally;
            this.smallLettersDrawCoord = new Vector2(smallLetterCoordX, smallLetterCoordY);
            this.inBattleTurn = true;
            this.isAlive = true;
            

            this.LoadDefaultUnitStats();
        }

        protected WarUnit(bool playerUnit)
        {
            this.playerUnit = playerUnit;
            this.switchCounter = MinFrameSwitch;
            this.smallLettersDrawCoord = new Vector2(smallLetterCoordX, smallLetterCoordY);
            this.inBattleTurn = true;
            this.isAlive = true;

            this.LoadDefaultUnitStats();
        }

        public bool CanIMoveX { get; protected set; }
        public bool CanIMoveY { get; protected set; }

        public SoundStatus SoundStatus { get; protected set; }

        protected abstract void SetSoundMoveStatus();

        protected abstract void SetSoundAttackStatus();

        protected abstract void SetSoundTakeDamageStatus(SoundStatus attackerSoundStatus);

        public bool AmIAttacking
        {
            get { return this.amIAttacking; }
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

        public Vector2 DrawCoord
        {
            get { return this.warUnitDrawCoord; }
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

        protected virtual void SendSoundQuery(GameTime gameTime, Vector2 oldPosition, Vector2 newPosition)
        {
            bool amIMoving = oldPosition != newPosition;

            if (amIMoving)
            {
                this.soundFrameSwitch += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

                if (this.soundFrameSwitch >= this.maxSoundFrameSwitch)
                {
                    this.soundFrameSwitch = 0;

                    if (this.OnEvent != null)
                    {
                        this.OnEvent(this);
                    }
                }
            }
        }

        public abstract void SetStartPositionInBattleLikePlayer();

        public abstract void SetStartPositionInBattleLikeEnemy();

        protected abstract float GetDefaultMove();

        protected virtual void LoadDefaultUnitStats()
        {
            this.Color = Color.White;
            this.attackingFrames = DefaultAttackingFrames;
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

        //public Vector2 WarUnitDrawCoord
        //{
        //    get { return this.warUnitDrawCoord; }
        //}

        public virtual void LoadContent(ContentManager content)
        {
            this.warUnitImage = content.Load<Texture2D>(this.imagePath);

            this.cropCurrentFrame = new Rectangle(120, 0, 69, 100);
        }

        public virtual void Update(GameTime gameTime)
        {
            this.SetSoundMoveStatus();
            this.SendSoundQuery(gameTime, this.lastDrawCoord, this.warUnitDrawCoord);

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
                    this.cropStay, this.cropMove, this.cropAttack, this.cropHit, this.cropFrame, this.AmIAttacking);

                this.cropCurrentFrame = this.newAnimationFrame.ImageCrop;

                this.cropFrame++;

                if (this.cropFrame == 4)
                {
                    this.cropFrame = 0;
                }

                if (this.AmIAttacking)
                {
                    this.attackingFrames--;

                    if (this.attackingFrames == -1)
                    {
                        this.amIAttacking = false;
                        this.attackingFrames = DefaultAttackingFrames;
                    }
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
                    spriteBatch.DrawString(FontLoader.SmallSizeFont,
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

            this.IsAnotherSelectedUnit();

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
                this.SetSoundTakeDamageStatus(this.attacker.SoundStatus);

                if (this.OnEvent != null)
                {
                    this.OnEvent(this);
                }

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
                            // play sound
                            this.SetSoundAttackStatus();
                            if (this.OnEvent != null)
                            {
                                this.OnEvent(this);
                            }

                            this.arrow.FlyTo(this.DrawCoord, enemy.warUnitDrawCoord, this.warUnitImage);
                            enemy.isAttacked = true;
                            enemy.SetAttacker(this, arrow);
                            this.amIAttacking = true;
                            this.availableMove = 0;

                            enemy.IsAttackable = false;
                        }
                        else
                        {
                            //bool inFrontOfUs =
                            //    CollisionDetection.IsNear(this.WarUnitDrawCoord.X + this.CropWidth,
                            //        enemy.WarUnitDrawCoord.X, this.MinAttackDistance)
                            //    && CollisionDetection.IsNear(this.WarUnitDrawCoord.Y, enemy.WarUnitDrawCoord.Y,
                            //        this.MinAttackDistance);

                            //bool behindUs =
                            //    CollisionDetection.IsNear(this.WarUnitDrawCoord.X,
                            //        enemy.WarUnitDrawCoord.X + enemy.CropHeight, this.MinAttackDistance)
                            //    &&
                            //    CollisionDetection.IsNear(this.WarUnitDrawCoord.Y, enemy.WarUnitDrawCoord.Y,
                            //        this.MinAttackDistance);

                            //if (behindUs || inFrontOfUs)
                            //{
                            //    MapManager.Instance.Battlefield.TryToAttackEnemyUnit(this, enemy, this.weapon);
                            //    this.amIAttacking = true;
                            //    enemy.IsAttackable = false;
                            //}
                            //else
                            //{
                            //    this.wantedPosition.X = MouseExtended.Current.CurrentState.X - (cropWidth / 2);
                            //    this.wantedPosition.Y = MouseExtended.Current.CurrentState.Y - (cropHeight / 2);

                            //    this.Moving();
                            //}
                            if (CollisionDetection.IsBehindOrInFrontUsForAttack(this, enemy))
                            {
                                // play sound
                                this.SetSoundAttackStatus();
                                if (this.OnEvent != null)
                                {
                                    this.OnEvent(this);
                                }

                                // play sound
                                enemy.SetSoundTakeDamageStatus(this.SoundStatus);
                                if (enemy.OnEvent != null)
                                {
                                    enemy.OnEvent(enemy);
                                }

                                MapManager.Instance.Battlefield.TryToAttackEnemyUnit(this, enemy, this.weapon);
                                this.amIAttacking = true;
                                enemy.IsAttackable = false;
                            }
                            else
                            {
                                this.SetMoveToEnemy(enemy);
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
                IReadOnlyDictionary<WarUnit, decimal> playerUnits = MapManager.Instance.Battlefield.PlayerUnits;
                IReadOnlyDictionary<WarUnit, decimal> enemyUnits = MapManager.Instance.Battlefield.EnemyUnits;

                Vector2 oldPosition = this.DrawCoord;

                if (this.warUnitDrawCoord.X < this.wantedPosition.X)
                {
                    //this.warUnitDrawCoord.X += this.moveSpeed;
                    float newMoveSpeed = CollisionDetection
                        .WarUnitGoRight(this, this.moveSpeed, playerUnits, enemyUnits);
                    this.warUnitDrawCoord.X += newMoveSpeed;

                    if (newMoveSpeed == 0)
                    {
                        this.CanIMoveX = false;
                    }
                }
                if (this.warUnitDrawCoord.X > this.wantedPosition.X)
                {
                    //this.warUnitDrawCoord.X -= this.moveSpeed;
                    float newMoveSpeed = CollisionDetection
                        .WarUnitGoLeft(this, this.moveSpeed, playerUnits, enemyUnits);
                    this.warUnitDrawCoord.X -= newMoveSpeed;

                    if (newMoveSpeed == 0)
                    {
                        this.CanIMoveX = false;
                    }
                }
                if (this.warUnitDrawCoord.Y > this.wantedPosition.Y)
                {
                    //this.warUnitDrawCoord.Y -= this.moveSpeed;
                    float newMoveSpeed = CollisionDetection
                        .WarUnitGoUp(this, this.moveSpeed, playerUnits, enemyUnits);
                    this.warUnitDrawCoord.Y -= newMoveSpeed;

                    if (newMoveSpeed == 0)
                    {
                        this.CanIMoveY = false;
                    }
                }
                if (this.warUnitDrawCoord.Y < this.wantedPosition.Y)
                {
                    // this.warUnitDrawCoord.Y += this.moveSpeed;
                    float newMoveSpeed = CollisionDetection
                         .WarUnitGoDown(this, this.moveSpeed, playerUnits, enemyUnits);
                    this.warUnitDrawCoord.Y += newMoveSpeed;

                    if (newMoveSpeed == 0)
                    {
                        this.CanIMoveX = false;
                    }
                }

                this.availableMove -= CollisionDetection.CalculateDistanceTravelled(this.lastDrawCoord,
                    this.DrawCoord);

                this.CheckWhyCannotMove(oldPosition, playerUnits);
            }
        }

        private void CheckWhyCannotMove(Vector2 oldPosition, IReadOnlyDictionary<WarUnit, decimal> playerUnits)
        {
            if (this.DrawCoord == oldPosition && !this.playerUnit && (!this.CanIMoveX || !this.CanIMoveY))
            {
                this.LastTryForAttack(playerUnits);

                this.availableMove = 0;
            }
        }

        private void LastTryForAttack(IReadOnlyDictionary<WarUnit, decimal> playerUnits)
        {
            foreach (var target in playerUnits.Keys)
            {
                if (CollisionDetection.IsBehindOrInFrontUsForAttack(this, target))
                {
                    this.amIAttacking = true;
                    this.EnemyTryAttackPlayer(this, target, this.weapon);

                    this.SetSoundFromEnemyAttack();
                    break;
                }
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

        public StrenghtLevel GetStrengthLevel
        {
            get { return this.strengthLevel; }
        }

        //enemy behavior
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

                            if (CollisionDetection.IsBehindOrInFrontUsForAttack(this, this.supportUnit))
                            {
                                this.amIAttacking = true;
                                this.EnemyTryAttackPlayer(this, this.supportUnit, this.weapon);

                                this.SetSoundFromEnemyAttack();
                            }
                            else
                            {
                                this.SetMoveToEnemy(this.supportUnit);
                                this.Moving();
                            }

                            //
                            //this.SetMoveToEnemy(this.supportUnit);
                            //this.Moving();
                            //this.amIAttacking = true;
                            //this.EnemyTryAttackPlayer(this, this.supportUnit, this.weapon);
                        }

                    }
                    else if (!hasArcherFriend && hasArcherEnemy)
                    {
                        this.SearchForArcher();
                    }
                    else if (!hasArcherFriend && !hasArcherEnemy)
                    {
                        this.TryToAttackNearestTarget();
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
                        this.arrow.FlyTo(this.DrawCoord, this.supportUnit.warUnitDrawCoord, this.warUnitImage);
                        
                        // play sound
                        this.SetSoundAttackStatus();
                        if (this.OnEvent != null)
                        {
                            this.OnEvent(this);
                        }

                        this.supportUnit.isAttacked = true;
                        this.supportUnit.SetAttacker(this, arrow);
                        this.amIAttacking = true;
                        this.availableMove = 0;
                    }
                    
                }
            }

        }

        private void TryToAttackNearestTarget()
        {
            this.supportUnit = MapManager.Instance.Battlefield.TryTakeNearestPlayerUnit(this,
                SearchAnyEnemyRadius);

            if (this.supportUnit == null)
            {
                this.SetProtectedMove();
                this.Moving();
            }
            else
            {
                if (CollisionDetection.IsBehindOrInFrontUsForAttack(this, this.supportUnit))
                {
                    this.amIAttacking = true;
                    this.EnemyTryAttackPlayer(this, this.supportUnit, this.weapon);

                    this.SetSoundFromEnemyAttack();
                }
                else
                {
                    this.SetMoveToEnemy(this.supportUnit);
                    this.Moving();
                }
            }


        }

        private void SetSoundFromEnemyAttack()
        {
            // play sound
            this.SetSoundAttackStatus();
            if (this.OnEvent != null)
            {
                this.OnEvent(this);
            }

            //play sound
            this.supportUnit.SetSoundTakeDamageStatus(this.SoundStatus);
            if (this.supportUnit.OnEvent != null)
            {
                this.supportUnit.OnEvent(this.supportUnit);
            }
        }

        private void SearchForArcher()
        {
            this.supportUnit = MapManager.Instance.Battlefield.TryTakeEnemyUnit(x => x.amIArcherOrMage == true);

            if (this.supportUnit == null)
            {
                this.inBattleTurn = false;
                this.TryToAttackNearestTarget();
            }
            
            if (CollisionDetection.IsBehindOrInFrontUsForAttack(this, this.supportUnit) && this.inBattleTurn)
            {
                this.amIAttacking = true;
                this.EnemyTryAttackPlayer(this, this.supportUnit, this.weapon);
                this.SetSoundFromEnemyAttack();
            }
            else
            {
                this.SetMoveToEnemy(this.supportUnit);
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

            //if (CollisionDetection.IsNear(attacker.WarUnitDrawCoord.X, defender.WarUnitDrawCoord.X,
            //    this.minAttackDistance)
            //    &&
            //    CollisionDetection.IsNear(attacker.WarUnitDrawCoord.Y, defender.WarUnitDrawCoord.Y,
            //        this.minAttackDistance)
            //    && this.inBattleTurn)
            //{
            //    if (attacker.inBattleTurn)
            //    {
            //        decimal attackerQuantity = MapManager.Instance.Battlefield.TryTakeFriendUnitQuantity(attacker);
            //        int attackerDamage = (weaponDamage + attacker.Damage)*(int)attackerQuantity;

            //        decimal enemyQuantity = MapManager.Instance.Battlefield.TryTakeEnemmyUnitQuantity(defender);
            //        int defenderDamage = (int) (defender.Damage*enemyQuantity);

            //        MapManager.Instance.Battlefield.AttackPlayerUnit(defender, attackerDamage, attacker, defenderDamage);
            //    }

            //    attacker.inBattleTurn = false;

            //    this.HaveActionForCurrentTurn = false;
            //}

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
            else if (!attacker.AmIArcherOrMage)
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
                this.wantedPosition = new Vector2(this.DrawCoord.X - 90, this.DrawCoord.Y);
                this.HaveActionForCurrentTurn = false;
            }
            else
            {
                if (this.wantedPosition == this.DrawCoord)
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

            if (CollisionDetection.IsNear(this.DrawCoord.Y, this.supportUnit.DrawCoord.Y, 10)
                &&
                CollisionDetection.IsNear(this.DrawCoord.X + this.cropWidth,
                    this.supportUnit.DrawCoord.X, 10)
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
            this.wantedPosition.X = unit.DrawCoord.X - this.cropWidth;
            this.wantedPosition.Y = unit.warUnitDrawCoord.Y;
        }

        protected void SetMoveToEnemy(WarUnit enemy)
        {
            if (this.DrawCoord.X > enemy.DrawCoord.X)
            {
                this.wantedPosition = new Vector2(enemy.DrawCoord.X + enemy.CropWidth, enemy.DrawCoord.Y);
            }
            else
            {
                this.wantedPosition = new Vector2(enemy.DrawCoord.X - this.cropWidth, enemy.warUnitDrawCoord.Y);
            }
            //this.wantedPosition = new Vector2(enemy.WarUnitDrawCoord.X, enemy.WarUnitDrawCoord.Y);

        }

        public void ReFillAvailableMove()
        {
            this.availableMove = this.GetDefaultMove();
            this.CanIMoveX = true;
            this.CanIMoveY = true;
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
