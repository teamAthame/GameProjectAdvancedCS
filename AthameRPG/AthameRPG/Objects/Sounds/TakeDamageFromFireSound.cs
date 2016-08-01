using Microsoft.Xna.Framework.Content;

namespace AthameRPG.Objects.Sounds
{
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
