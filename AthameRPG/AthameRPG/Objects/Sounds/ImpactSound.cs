namespace AthameRPG.Objects.Sounds
{
    using Microsoft.Xna.Framework.Content;

    public class ImpactSound : Sound
    {
        private const string PathSound = @"../Content/SoundEffects/Impact";

        public ImpactSound(ContentManager contentManager) : base(contentManager)
        {
            this.soundPath = PathSound;
            this.Init();
        }
    }
}
