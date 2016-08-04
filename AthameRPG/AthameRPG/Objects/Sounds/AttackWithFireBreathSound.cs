namespace AthameRPG.Objects.Sounds
{
    using Microsoft.Xna.Framework.Content;

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
