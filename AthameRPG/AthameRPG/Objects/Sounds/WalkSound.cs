namespace AthameRPG.Objects.Sounds
{
    using Microsoft.Xna.Framework.Content;

    public class WalkSound : Sound
    {
        private const string PathSound = @"../Content/SoundEffects/walk";

        public WalkSound(ContentManager contentManager) : base(contentManager)
        {
            this.soundPath = PathSound;
            this.Init();
        }
    }
}
