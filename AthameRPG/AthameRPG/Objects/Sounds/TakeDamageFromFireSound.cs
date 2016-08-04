namespace AthameRPG.Objects.Sounds
{
    using Microsoft.Xna.Framework.Content;

    public class TakeDamageFromFireSound : Sound
    {
        private const string PathSound = @"../Content/SoundEffects/TakeDamageFromFire";

        public TakeDamageFromFireSound(ContentManager contentManager) : base(contentManager)
        {
            this.soundPath = PathSound;
            this.Init();
        }
    }
}
