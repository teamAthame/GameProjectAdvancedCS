namespace AthameRPG.Objects.Sounds
{
    using Microsoft.Xna.Framework.Content;

    public class TakeDamageSound : Sound
    {
        private const string PathSound = @"../Content/SoundEffects/takeDamage";

        public TakeDamageSound(ContentManager contentManager) : base(contentManager)
        {
            this.soundPath = PathSound;
            this.Init();
        }
    }
}
