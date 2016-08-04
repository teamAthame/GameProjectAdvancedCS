namespace AthameRPG.Objects.Weapons.Arrows
{
    using System;
    using AthameRPG.GameEngine.Collisions;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public abstract class Arrow : Weapon
    {
        private const double MinDistanceToDraw = 20;
        private const int StepDivider = 50;
        private const int DrawCorrection = 30;

        private int cropWidth = 42; 
        private int cropHeight = 12;

        private Vector2 drawCoords;
        private Vector2 targetCoords;
        private Vector2 midPoint;

        private Texture2D texture2D;
        private SpriteEffects drawEffect;
        private Rectangle cropArrow;
        
        protected float stepX;
        protected float stepY;
        protected double distance;

        protected Arrow()
        {
            this.cropArrow = new Rectangle(330,143,42,12);
        }
        
        public virtual bool HasTarget { get; protected set; }

        public Vector2 DrawCoords
        {
            get { return this.drawCoords; }
        }

        public int CropWidth
        {
            get { return this.cropWidth; }
        }

        public int CropHeight
        {
            get { return this.cropHeight; }
        }

        public virtual void FlyTo(Vector2 attacker, Vector2 target, Texture2D texture)
        {
            this.texture2D = texture;
            this.HasTarget = true;
            this.drawCoords = attacker;
            this.targetCoords = target;
            this.midPoint = attacker;

            //this.cropWidth; = attacker arrow width
            //this.cropHeight; = attacker arrow height
            
            this.SetDrawEffect();
            this.CalculateStepForTheNextPoint(this.drawCoords, this.targetCoords);
        }

        private void SetDrawEffect()
        {
            if (this.drawCoords.X > this.targetCoords.X)
            {
                this.drawEffect = SpriteEffects.FlipHorizontally;
            }
            else
            {
                this.drawEffect = SpriteEffects.None;
            }
        }

        private void CalculateStepForTheNextPoint(Vector2 attackerCoords, Vector2 targetCoords)
        {
            this.stepX =Math.Abs(attackerCoords.X - targetCoords.X)/ StepDivider;
            this.stepY = Math.Abs(attackerCoords.Y - targetCoords.Y)/ StepDivider;
        }

        public virtual void Update(GameTime gameTime)
        {
            
            this.distance = CollisionDetection.CalculateDistanceTravelled(this.drawCoords, this.targetCoords);

            if (this.distance > MinDistanceToDraw)
            {
                this.CalculateNewDrawPosition();
                

                this.distance = CollisionDetection.CalculateDistanceTravelled(this.drawCoords, this.midPoint);

                while (this.distance < MinDistanceToDraw)
                {
                    this.CalculateNewDrawPosition();

                    this.distance = CollisionDetection.CalculateDistanceTravelled(this.drawCoords, this.midPoint);
                }
                this.drawCoords = this.midPoint;
            }
            else
            {
                this.drawCoords = this.targetCoords;
                this.HasTarget = false;
                // target is hit.... 
            }
        }

        private void CalculateNewDrawPosition()
        {
            if (this.drawCoords.X > this.targetCoords.X)
            {
                this.midPoint.X -= this.stepX;

                if (this.drawCoords.Y > this.targetCoords.Y)
                {
                    this.midPoint.Y -= this.stepY;
                }
                else
                {
                    this.midPoint.Y += this.stepY;
                }
            }
            else
            {
                this.midPoint.X += this.stepX;

                if (this.drawCoords.Y > this.targetCoords.Y)
                {
                    this.midPoint.Y -= this.stepY;
                }
                else
                {
                    this.midPoint.Y += this.stepY;
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.texture2D,
                        new Rectangle((int)this.drawCoords.X, (int)this.drawCoords.Y + DrawCorrection, cropWidth, cropHeight),
                        this.cropArrow,
                        Color.White,
                        0f,
                        new Vector2(0, 0), 
                        this.drawEffect,
                        0f);
        }
    }
}
