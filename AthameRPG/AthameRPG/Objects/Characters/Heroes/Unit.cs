namespace AthameRPG.Objects.Characters.Heroes
{
    using System.Collections.Generic;
    using System.Linq;
    using AthameRPG.Enums;
    using AthameRPG.GameEngine.Graphics;
    using AthameRPG.Objects.Characters.WarUnits;
    using AthameRPG.Contracts;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    public abstract class Unit : ISoundable
    {
        public virtual event OnEvent OnEvent;

        private const int StartingIndexFrame = 0;

        protected static bool isInCastle;
        protected static bool isInBattle;
        protected const int MinFrameSwitch = 100;
        
        protected int frameCounter;
        protected int soundFrameSwitch;
        protected int maxSoundFrameSwitch;
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
            this.cropFrame = StartingIndexFrame;
            this.returnedValue = new AnimationReturnedValue();
            this.AtackPoints = atack;
            this.HealthPoints = health;
            this.DefencePoints = defence;
            this.SoundStatus = SoundStatus.Walk;

            this.LoadDefaultStats();
        }

        protected abstract void LoadDefaultStats();

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

        public virtual void Restart()
        {
            this.LoadDefaultStartArmy();
            this.availableCreatures.Clear();
        }

        protected abstract void LoadDefaultStartArmy();

        public virtual SoundStatus SoundStatus { get; private set; }
    }
}