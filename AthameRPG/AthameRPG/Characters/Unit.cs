

using System.Collections.Generic;
using AthameRPG.Characters.WarUnits;
using AthameRPG.GameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AthameRPG.Characters
{
    public abstract class Unit 
    {

        protected int frameCounter;
        protected int switchCounter;
        protected string direction;
        protected int cropFrame;
        protected AnimationReturnedValue returnedValue;
        protected Vector2 lastAbstractCoord;
        protected Rectangle cropCurrentFrame;
        protected Dictionary<WarUnit, int> availableCreatures;
        protected double availableMove;

        protected bool isAlive;

        protected float startPositionX, startPositionY;

        public Unit(float startPositionX, float startPositionY, int atack, int health, int defence)
        {
            this.availableCreatures = new Dictionary<WarUnit, int>();
            this.StartPositionX = startPositionX;
            this.StartPositionY = startPositionY;
            this.IsAlive = true;
            this.switchCounter = 100;
            this.cropFrame = 0;
            this.returnedValue = new AnimationReturnedValue();
            this.CropCurrentFrame = cropCurrentFrame;
            this.AtackPoints = atack;
            this.HealthPoints = health;
            this.DefencePoints = defence;
        }

        public int AtackPoints { get; set; }
        public int DefencePoints { get; set; }
        public int HealthPoints { get; set; }

        public IReadOnlyDictionary<WarUnit,int> AvailableCreatures
        {
            get { return this.availableCreatures; }
        }

        public void AddCreature(WarUnit warUnit)
        {
            if (!this.availableCreatures.ContainsKey(warUnit))
            {
                this.availableCreatures.Add(warUnit, 0);
            }
            this.availableCreatures[warUnit]++;
        }
        
        public Rectangle CropCurrentFrame
        {
            get
            {
                return this.cropCurrentFrame;
            }
            protected set
            {
                //this.cropCurrentFramePlayer = new Rectangle(0, 0, cropWidth, cropHeight);
                this.cropCurrentFrame = value;
            }
        }

        public bool IsAlive
        {
            get
            {
                return this.isAlive;
            }
            private set
            {
                this.isAlive = value;
            }
        }

        public void KillTarget()
        {
            IsAlive = false;
        }

        public float StartPositionX
        {
            get
            {
                return this.startPositionX;
            }
            protected set
            {
                this.startPositionX = value;
            }
        }

        public float StartPositionY
        {
            get
            {
                return this.startPositionY;
            }
            protected set
            {
                this.startPositionY = value;
            }
        }

        public abstract void LoadContent(ContentManager content);

        public abstract void UnloadContent();

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch);
        
    }
}
