namespace AthameRPG.Objects.Sounds
{
    using Microsoft.Xna.Framework.Content;

    public class AttackWithMeleSound : Sound
    {
        private const string PathSound = @"../Content/SoundEffects/AttackWithMele";

        public AttackWithMeleSound(ContentManager contentManager) : base(contentManager)
        {
            this.soundPath = PathSound;
            this.Init();
        }
    }
}
