using Microsoft.Xna.Framework.Content;

namespace AthameRPG.Objects.Sounds
{
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
