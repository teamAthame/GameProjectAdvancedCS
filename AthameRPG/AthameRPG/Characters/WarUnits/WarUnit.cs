using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AthameRPG.Controls;
using AthameRPG.GameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AthameRPG.Characters.WarUnits
{
    public abstract class WarUnit
    {
        protected const int MinFrameSwitch = 100;
        
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
        protected bool choosen;

        protected int cropStayRow;
        protected int cropMovingRow;
        protected int cropAttackRow;
        protected int frameCounter;
        protected int switchCounter;
        protected int cropFrame;
        protected AnimationReturnedValue newAnimationFrame;
        protected MouseState newMouseState;
        protected MouseState oldMouseState;
        protected int cropStayWidth = 69;
        protected int cropStayHeight = 97;
        protected int cropMoveWidth = 0;
        protected int cropMoveHeight = 0;
        protected int cropAttackWidth = 0;
        protected int cropAttackHeight = 0;
        protected float moveSpeed;


        public WarUnit()
        {
            this.playerUnit = false;
            this.switchCounter = MinFrameSwitch;
            this.warUnitEffect = SpriteEffects.FlipHorizontally;
        }

        public WarUnit(bool playerUnit)
        {
            this.playerUnit = playerUnit;
            this.switchCounter = MinFrameSwitch;
        }

        public Vector2 WarUnitDrawCoord
        {
            get { return this.warUnitDrawCoord; }
        }

        public virtual void LoadContent(ContentManager content)
        {
            this.warUnitImage = content.Load<Texture2D>(this.imagePath);
            
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
                //

                this.newAnimationFrame = Animation.BattlefieldAnimation(this.lastDrawCoord, this.warUnitDrawCoord,
                        this.cropStayRow, this.cropMovingRow, this.cropAttackRow, this.cropFrame, cropStayWidth, cropStayHeight);

                this.cropCurrentFrame = this.newAnimationFrame.ImageCrop;

                this.cropFrame++;

                if (this.cropFrame == 4)
                {
                    this.cropFrame = 0;
                }

                //if (this.playerUnit)
                //{
                //    this.newAnimationFrame = Animation.BattlefieldAnimation(this.lastDrawCoord, this.warUnitDrawCoord,
                //        this.cropStayRow, this.cropMovingRow, this.cropAttackRow, this.cropFrame, cropStayWidth, cropStayHeight);

                //    this.cropCurrentFrame = this.newAnimationFrame.ImageCrop;

                //    this.cropFrame++;

                //    if (this.cropFrame == 4)
                //    {
                //        this.cropFrame = 0;
                //    }
                //}
                //else
                //{
                //    this.newAnimationFrame = Animation.BattlefieldAnimation(this.lastDrawCoord, this.warUnitDrawCoord,
                //        this.cropStayRow, this.cropMovingRow, this.cropAttackRow, this.cropFrame, cropStayWidth, cropStayHeight);

                //    this.cropCurrentFrame = this.newAnimationFrame.ImageCrop;

                //    this.cropFrame++;

                //    if (this.cropFrame == 4)
                //    {
                //        this.cropFrame = 0;
                //    }
                //}
            }

            
        }
        
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (this.playerUnit)
            {
                
                if (this.choosen)
                {
                    spriteBatch.Draw(this.warUnitImage, this.warUnitDrawCoord, this.cropCurrentFrame, Color.Red);
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
                && this.playerUnit)
            {
                if (!this.choosen && this.IsAnotherSelectedUnit())
                {
                    this.choosen = true;
                    this.wantedPosition = this.warUnitDrawCoord;

                }
                else
                {
                    this.choosen = false;
                }
            }

            if (this.choosen)
            {
                // this.newMouseState.LeftButton == ButtonState.Pressed && this.oldMouseState.LeftButton == ButtonState.Released
                // MouseExtended.Current.WasDoubleClick(MouseButton.Left)
                if (MouseExtended.Current.WasDoubleClick(MouseButton.Left))
                {
                    this.wantedPosition.X = MouseExtended.Current.CurrentState.X - (cropStayWidth / 2);
                    this.wantedPosition.Y = MouseExtended.Current.CurrentState.Y - (cropStayHeight / 2);
                }

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
            }
        }

        private bool IsAnotherSelectedUnit()
        {
            foreach (var availableCreature in CharacterManager.barbarian.AvailableCreatures)
            {
                if (availableCreature.Key.choosen)
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
    }
}
