namespace AthameRPG.Objects.Sounds
{
    using Microsoft.Xna.Framework.Content;

    public class FlySound : Sound
    {
        private const string PathSound = @"../Content/SoundEffects/fly";

        public FlySound(ContentManager contentManager) : base(contentManager)
        {
            this.soundPath = PathSound;
            this.Init();
        }
    }
}
