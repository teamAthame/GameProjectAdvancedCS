using System.Collections.Generic;
using System.Linq;
using AthameRPG.Characters.WarUnits;
using AthameRPG.GameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AthameRPG.Characters.Heroes
{
    public abstract class Unit
    {
        protected static bool isInCastle;
        protected static bool isInBattle;
        protected const int MinFrameSwitch = 100;

        protected int frameCounter;
        protected int switchCounter;
        protected string direction;
        protected int cropFrame;
        protected AnimationReturnedValue returnedValue;
        protected Vector2 lastAbstractCoord;
        protected Rectangle cropCurrentFrame;
        protected Dictionary<WarUnit, decimal> availableCreatures;
        protected double availableMove;
        protected double defaultPlayerMove;
        protected WarUnit supportUnitForAdding;

        protected bool isAlive;

        protected float startPositionX, startPositionY;

        protected Unit(float startPositionX, float startPositionY, int atack, int health, int defence)
        {
            this.availableCreatures = new Dictionary<WarUnit, decimal>();
            this.StartPositionX = startPositionX;
            this.StartPositionY = startPositionY;
            this.IsAlive = true;
            this.switchCounter = MinFrameSwitch;
            this.cropFrame = 0;
            this.returnedValue = new AnimationReturnedValue();
            //this.CropCurrentFrame = cropCurrentFrame;
            this.AtackPoints = atack;
            this.HealthPoints = health;
            this.DefencePoints = defence;
        }

        public virtual int AtackPoints { get; set; }
        public virtual int DefencePoints { get; set; }
        public virtual int HealthPoints { get; set; }

        public virtual void LoadContent(ContentManager content)
        {
            foreach (var creatures in this.availableCreatures)
            {
                creatures.Key.LoadContent(content);
            }
        }

        public abstract void UnloadContent();

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch);

        public virtual void ReFillMovement()
        {
            this.availableMove = this.defaultPlayerMove;
        }

        public virtual double AvailableMove
        {
            get { return this.availableMove; }
        }

        public Dictionary<WarUnit, decimal> AvailableCreatures
        {
            get { return this.availableCreatures; }
            set { this.availableCreatures = value; }
        }

        public virtual void AddCreature(WarUnit newUnit)
        {

            this.supportUnitForAdding =
            this.availableCreatures.Keys.FirstOrDefault(x => x.GetStrengthLevel == newUnit.GetStrengthLevel);

            if (this.supportUnitForAdding == null)
            {
                this.availableCreatures.Add(newUnit, 1);
            }
            else
            {
                this.availableCreatures[supportUnitForAdding]++;
            }

            //if (!this.availableCreatures.ContainsKey(newUnit))
            //{
            //    this.availableCreatures.Add(newUnit,0);
            //}
            //this.availableCreatures[newUnit]++;
        }

        public Rectangle CropCurrentFrame
        {
            get { return this.cropCurrentFrame; }
            protected set
            {
                //this.cropCurrentFramePlayer = new Rectangle(0, 0, cropWidth, cropHeight);
                this.cropCurrentFrame = value;
            }
        }

        public virtual bool IsAlive
        {
            get { return this.isAlive; }
            private set { this.isAlive = value; }
        }

        public virtual void KillTarget()
        {
            IsAlive = false;
        }

        public virtual float StartPositionX
        {
            get { return this.startPositionX; }
            protected set { this.startPositionX = value; }
        }

        public virtual float StartPositionY
        {
            get { return this.startPositionY; }
            protected set { this.startPositionY = value; }
        }
    }
}