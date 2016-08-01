using Microsoft.Xna.Framework.Content;

namespace AthameRPG.Objects.Sounds
{
    public class AttackWithFireBreathSound : Sound
    {
        private const string PathSound = @"../Content/SoundEffects/AttackWithFireBreath";

        public AttackWithFireBreathSound(ContentManager contentManager) : base(contentManager)
        {
            this.soundPath = PathSound;
            this.Init();
        }
    }
}
