using Microsoft.Xna.Framework.Content;

namespace AthameRPG.Objects.Sounds
{
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
