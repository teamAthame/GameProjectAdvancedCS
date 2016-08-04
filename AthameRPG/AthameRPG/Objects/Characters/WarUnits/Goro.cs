namespace AthameRPG.Objects.Characters.WarUnits
{
    using AthameRPG.Enums;
    using Microsoft.Xna.Framework;

    public class Goro : WarUnit
    {
        private const StrenghtLevel DefaultStrengthLevel = StrenghtLevel.Stronger;
        private const string DefaultImagePath = "../Content/Character/goro";
        private const int DefaultProtectedStep = 90;
        private const float DefaultMoveSpeed = 2f;
        private const float DefaultAvailableMove = 200f;
        private const int DefaultHealth = 400;
        private const int DefaultDamage = 150;
        private const int DefaultAttackAnywayDistance = 200;

        private const int DefaultMinAttackDistance = 10;

        private const int CropWidth = 55;
        private const int CropHeight = 80;

        private const int MaxSoundFrameSwitch = 200;

        private Vector2 DefaultStartPositionInBattleLikePlayer = new Vector2(10, 200);
        private Vector2 DefaultStartPositionInBattleLikeEnemy = new Vector2(715, 200);

        public Goro():base()
        {
            this.warUnitDrawCoord = DefaultStartPositionInBattleLikeEnemy;
        }
        public Goro(bool playerUnit):base(playerUnit)
        {
            this.warUnitDrawCoord = DefaultStartPositionInBattleLikePlayer;
        }

        protected override void LoadDefaultUnitStats()
        {
            base.LoadDefaultUnitStats();
            this.strengthLevel = DefaultStrengthLevel;
            this.imagePath = DefaultImagePath;
            this.moveSpeed = DefaultMoveSpeed;
            this.availableMove = DefaultAvailableMove;
            this.damage = DefaultDamage;
            this.health = DefaultHealth;
            this.minAttackDistance = DefaultMinAttackDistance;
            this.protectedStep = DefaultProtectedStep;
            this.attackAnywayDistance = DefaultAttackAnywayDistance;

            this.cropWidth = CropWidth;
            this.cropHeight = CropHeight;

            this.maxSoundFrameSwitch = MaxSoundFrameSwitch;

            this.cropStay = new[]
            {
                // x + x(correction) , y , width , height
                new Rectangle(10,10,45,80),
                new Rectangle(55,10,50,80),
                new Rectangle(108,10,51,80),
                new Rectangle(162,10,52,80)
            };

            this.cropMove = new[]
            {
                new Rectangle(55,98,42,80),
                new Rectangle(140,98,45,80),
                new Rectangle(326,98,46,80),
                new Rectangle(372,98,48,80),

            };

            this.cropAttack = new[]
            {
                new Rectangle(193,185,52,80),
                new Rectangle(247,185,70,80),
                new Rectangle(193,185,52,80),
                new Rectangle(247,185,70,80)
            };

            this.cropHit = new[]
            {
                new Rectangle(189,267,57,80),
                new Rectangle(245,267,52,80),
                new Rectangle(189,267,57,80),
                new Rectangle(245,267,52,80)
            };
        }

        public override int GetDefaultHeаlth()
        {
            return DefaultHealth;
        }

        protected override float GetDefaultMove()
        {
            return DefaultAvailableMove;
        }

        public override void SetStartPositionInBattleLikePlayer()
        {
            this.warUnitDrawCoord = this.DefaultStartPositionInBattleLikePlayer;
        }

        public override void SetStartPositionInBattleLikeEnemy()
        {
            this.warUnitDrawCoord = this.DefaultStartPositionInBattleLikeEnemy;
        }

        protected override void SetSoundMoveStatus()
        {
            this.SoundStatus = SoundStatus.Walk;
        }

        protected override void SetSoundAttackStatus()
        {
            this.SoundStatus = SoundStatus.AttackWithMele;
        }

        protected override void SetSoundTakeDamageStatus(SoundStatus attackerSoundStatus)
        {
            if (attackerSoundStatus == SoundStatus.AttackWithFireBreath)
            {
                this.SoundStatus = SoundStatus.TakeDamageFromFire;
            }
            else
            {
                this.SoundStatus = SoundStatus.TakeDamage;
            }
        }
    }
}
