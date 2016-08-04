namespace AthameRPG.Objects.Sounds
{
    using AthameRPG.Attributes;
    using AthameRPG.Contracts;
    using Microsoft.Xna.Framework.Audio;
    using Microsoft.Xna.Framework.Content;

    [SoundAttribute]
    public abstract class Sound : ISound
    {
        private ContentManager contentManager;
        private SoundEffect soundEffect;
        protected string soundPath;

        protected Sound(ContentManager contentManager)
        {
            this.contentManager = contentManager;
            //this.Init();
        }

        protected void Init()
        {
            this.soundEffect = this.contentManager.Load<SoundEffect>(this.soundPath);
        }

        public virtual void Play()
        {
            this.soundEffect.Play();
        }
    }
}
