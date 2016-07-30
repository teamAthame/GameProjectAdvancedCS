using Microsoft.Xna.Framework.Content;

namespace AthameRPG.Objects.Sounds
{
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
