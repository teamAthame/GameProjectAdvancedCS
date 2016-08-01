using Microsoft.Xna.Framework.Content;

namespace AthameRPG.Objects.Sounds
{
    public class AttackWithRangeWeaponSound : Sound
    {
        private const string PathSound = @"../Content/SoundEffects/AttackWithRangeWeapon";

        public AttackWithRangeWeaponSound(ContentManager contentManager) : base(contentManager)
        {
            this.soundPath = PathSound;
            this.Init();
        }
    }
}
