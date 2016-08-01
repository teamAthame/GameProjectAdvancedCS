using System;
using AthameRPG.Attributes.Behavior;
using AthameRPG.Enums;
using Microsoft.Xna.Framework;

namespace AthameRPG.Objects.Characters.WarUnits
{
    [FireBreatheUnit]
    [FlyUnit]
    public class BlackDragon : WarUnit
    {
        //private const int DefaultStrengthLevel1 = 7;
        private const StrenghtLevel DefaultStrengthLevel = StrenghtLevel.Strongest;
        private const string DefaultImagePath = "../Content/Character/blackDragon";
        //private const int DefaultStayRow = 0;
        //private const int DefaultCropStayWidth = 69;
        //private const int DefaultCropStayHeight = 97;
        //private const int DefaultMoveRow = 0; //-------------------------------
        //private const int DefaultCropMoveWidth = 0;
        //private const int DefaultCropMoveHeight = 0;
        //private const int DefaultAttackRow = 0; //----------------------------
        //private const int DefaultCropAttackWidth = 0;
        //private const int DefaultCropAttackHeight = 0;
        //private const int DefaultCorrectionCropByX = 326;
        private const float DefaultMoveSpeed = 6f;
        private const float DefaultAvailableMove = 450f;
        private const int DefaultHealth = 500;
        private const int DefaultDamage = 200;
        private const int DefaultProtectedStep = 90;
        private const int DefaultAttackAnywayDistance = 250;
        private const int DefaultMinAttackDistance = 10;

        private const int CropWidth = 70;
        private const int CropHeight = 100;

        private const int MaxSoundFrameSwitch = 200;

        private readonly Vector2 DefaultStartPositionInBattleLikePlayer = new Vector2(10,10);
        private readonly Vector2 DefaultStartPositionInBattleLikeEnemy = new Vector2(715, 10);

        public BlackDragon() : base()
        {
            this.warUnitDrawCoord = DefaultStartPositionInBattleLikeEnemy;

            
        }

        public BlackDragon(bool playerUnit):base(playerUnit)
        {
            this.warUnitDrawCoord = this.DefaultStartPositionInBattleLikePlayer;
            
        }

        protected override void LoadDefaultUnitStats()
        {
            base.LoadDefaultUnitStats();
            this.strengthLevel = DefaultStrengthLevel;
            this.imagePath = DefaultImagePath;
            //this.cropStayRow = DefaultStayRow;
            //this.cropStayWidth = DefaultCropStayWidth;
            //this.cropStayHeight = DefaultCropStayHeight;
            //this.cropMoveWidth = 0;
            //this.cropMoveHeight = 0;
            //this.cropAttackWidth = 0;
            //this.cropAttackHeight = 0;
            //this.correctionStayCropByX = DefaultCorrectionCropByX;
            this.moveSpeed = DefaultMoveSpeed;
            this.availableMove = DefaultAvailableMove;
            this.damage = DefaultDamage;
            this.health = DefaultHealth;
            this.minAttackDistance = DefaultMinAttackDistance;
            this.protectedStep = DefaultProtectedStep;
            this.attackAnywayDistance = DefaultAttackAnywayDistance;

            this.maxSoundFrameSwitch = MaxSoundFrameSwitch;

            this.cropWidth = CropWidth;
            this.cropHeight = CropHeight; 

            this.cropStay = new[]
            {
                // x + x(correction) , y , width , height
                new Rectangle(533,0,69,98),
                new Rectangle(601,0,68,98),
                new Rectangle(668,0,69,98),
                new Rectangle(737,0,70,98)
            };

            this.cropMove = new[]
            {
                new Rectangle(225,98,151,118),
                new Rectangle(375,98,155,118),
                new Rectangle(530,98,158,118),
                new Rectangle(687,98,157,118),
                
            };

            this.cropAttack = new[]
            {
                new Rectangle(0,401,148,100),
                new Rectangle(152,401,164,100),
                new Rectangle(316,401,162,100),
                new Rectangle(478,401,160,100)
            };

            this.cropHit = new[]
            {
                new Rectangle(666,512,123,95),
                new Rectangle(788,512,103,95),
                new Rectangle(890,512,108,95),
                new Rectangle(997,512,95,95),
            };
        }

        public override int GetDefaultHeаlth()
        {
            return DefaultHealth;
        }

        public override void SetStartPositionInBattleLikePlayer()
        {
            this.warUnitDrawCoord = this.DefaultStartPositionInBattleLikePlayer;
        }

        public override void SetStartPositionInBattleLikeEnemy()
        {
            this.warUnitDrawCoord = this.DefaultStartPositionInBattleLikeEnemy;
        }

        protected override float GetDefaultMove()
        {
            return DefaultAvailableMove;
        }

        protected override void SetSoundMoveStatus()
        {
            this.SoundStatus = SoundStatus.Fly;
        }

        protected override void SetSoundAttackStatus()
        {
            this.SoundStatus = SoundStatus.AttackWithFireBreath;
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
