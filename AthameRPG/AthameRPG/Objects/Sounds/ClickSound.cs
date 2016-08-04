namespace AthameRPG.Objects.Sounds
{
    using Microsoft.Xna.Framework.Content;

    public class ClickSound : Sound
    {
        private const string PathSound = @"../Content/SoundEffects/click";

        public ClickSound(ContentManager contentManager) : base(contentManager)
        {
            this.soundPath = PathSound;
            this.Init();
        }
    }
}
