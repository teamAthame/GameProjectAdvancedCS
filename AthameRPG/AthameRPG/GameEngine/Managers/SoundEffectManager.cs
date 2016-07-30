using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AthameRPG.Attributes;
using AthameRPG.Contracts;
using AthameRPG.Objects.Sounds;
using Microsoft.Xna.Framework.Content;

namespace AthameRPG.GameEngine.Managers
{
    //public delegate void OnClick(ISoundable currentSender);

    public class SoundEffectManager
    {
        public event OnClick OnClick;

        private const string Suffix = "sound";

        private Dictionary<string, ISound> soundCollection;
        private ContentManager contentManager;
        private ISound sound;

        public SoundEffectManager(ContentManager contentManager)
        {
            this.soundCollection = new Dictionary<string, ISound>();
            this.contentManager = contentManager;
        }

        public void ExecuteQuery(ISoundable currentSender)
        {
            this.SetSound(currentSender);

            this.sound.Play();
        }

        private void SetSound(ISoundable currentSender)
        {
            string soundName = currentSender.SoundStatus.ToString().ToLower() + Suffix;

            if (!this.soundCollection.ContainsKey(currentSender.ToString()))
            {
                ISound newSound = this.GetSoundFromAssembly(soundName);
                this.soundCollection.Add(currentSender.ToString(), newSound);
            }

            this.sound = this.soundCollection[currentSender.ToString()];
        }

        private ISound GetSoundFromAssembly(string soundName)
        {
            var newSound = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(x => x.IsDefined(typeof(SoundAttribute), true))
                .FirstOrDefault(s => s.Name.ToLower() == soundName);

            if (newSound == null)
            {
                throw new ArgumentNullException("Sound Effect Manager", "Cant find current sound!");
            }

            //Sound sound = new ClickSound(this.contentManager);
            ISound currentSound =(ISound)Activator.CreateInstance(newSound, this.contentManager);
 
            return currentSound;
        }
    }
}
