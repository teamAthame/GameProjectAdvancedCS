namespace AthameRPG.GameEngine.Graphics
{
    using Microsoft.Xna.Framework;

    public class AnimationReturnedValue
    {
        private string direction;
        private Rectangle imageCrop;

        public AnimationReturnedValue()
        {
            
        }

        public AnimationReturnedValue(string direction, Rectangle imageCrop)
        {
            this.Direction = direction;
            this.ImageCrop = imageCrop;
        }

        public string Direction
        {
            get
            {
                return this.direction;
                
            }
            private set { this.direction = value; }
        }

        public Rectangle ImageCrop { get;private set; }
    }
}
