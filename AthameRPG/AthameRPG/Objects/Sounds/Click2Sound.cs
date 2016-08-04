namespace AthameRPG.Objects.Sounds
{
    using Microsoft.Xna.Framework.Content;

    public class Click2Sound : Sound
    {
        private const string PathSound = @"../Content/SoundEffects/Click2";

        public Click2Sound(ContentManager contentManager) : base(contentManager)
        {
            this.soundPath = PathSound;
            this.Init();
        }
    }
}
