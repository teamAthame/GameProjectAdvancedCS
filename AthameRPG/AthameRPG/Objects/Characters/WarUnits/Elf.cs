﻿namespace AthameRPG.Objects.Characters.WarUnits
{
    using AthameRPG.Enums;
    using AthameRPG.Objects.Weapons.Arrows;
    using Microsoft.Xna.Framework;

    public class Elf : WarUnit
    {
        private const string DefaultImagePath = "../Content/Character/elf";
        private const StrenghtLevel DefaultStrengthLevel = StrenghtLevel.Strong;
        
        private const float DefaultMoveSpeed = 4f;
        private const float DefaultAvailableMove = 150f;
        private const int DefaultHealth = 200;
        private const int DefaultDamage = 30;
        private const int DefaultMinAttackDistance = 650;
        private const int DefaultProtectedStep = 90;
        private const int DefaultAttackAnywayDistance = 250;

        private const int CropWidth = 55;
        private const int CropHeight = 90;

        private const int MaxSoundFrameSwitch = 170;

        private readonly Vector2 DefaultStartPositionInBattleLikePlayer = new Vector2(10, 400);
        private readonly Vector2 DefaultStartPositionInBattleLikeEnemy = new Vector2(715, 400);

        public Elf() : base()
        {
            this.warUnitDrawCoord = DefaultStartPositionInBattleLikeEnemy;
            this.amIArcherOrMage = true;
        }

        public Elf(bool playerUnit):base(playerUnit)
        {
            this.warUnitDrawCoord = this.DefaultStartPositionInBattleLikePlayer;
            this.amIArcherOrMage = true;
        }

        protected override void LoadDefaultUnitStats()
        {
            base.LoadDefaultUnitStats();
            this.strengthLevel = DefaultStrengthLevel;
            this.imagePath = DefaultImagePath;

            this.cropWidth = CropWidth; 
            this.cropHeight = CropHeight;

            this.maxSoundFrameSwitch = MaxSoundFrameSwitch;

            this.cropStay = new[]
            {
                // x + x(correction) , y , width , height
                new Rectangle(190, 0, 58, 90),
                new Rectangle(247,0,58,90),
                new Rectangle(309,0,58,90),
                new Rectangle(364,0,58,90),
            };

            this.cropMove = new[]
            {
                new Rectangle(12,200,47,90),
                new Rectangle(59,200,47,90),
                new Rectangle(106,200,47,90),
                new Rectangle(153,200,47,90)
            };

            this.cropAttack = new[]
            {
                new Rectangle(13,110,55,90),
                new Rectangle(88,110,67,90),
                new Rectangle(158,110,60,90),
                new Rectangle(229,110,60,90)
            };

            this.cropHit = new[]
            {
                new Rectangle(8,0, 55, 90),
                new Rectangle(64,0,60,90),
                new Rectangle(8,0, 55, 90),
                new Rectangle(64,0,60,90),
            };

            this.moveSpeed = DefaultMoveSpeed;
            this.availableMove = DefaultAvailableMove;
            this.damage = DefaultDamage;
            this.health = DefaultHealth;
            this.minAttackDistance = DefaultMinAttackDistance;
            this.protectedStep = DefaultProtectedStep;
            this.attackAnywayDistance = DefaultAttackAnywayDistance;

            this.arrow = new WoodArrow();

            //// crop stay
            //this.correctionStayCropByX = DefaultStayCorrection;
            //this.cropStayRow = DefaultStayRow;
            //this.cropStayWidth = DefaultCropStayWidth;
            //this.cropStayHeight = DefaultCropStayHeight;

            //// crop move
            //this.correctionMoveByX = DefaultCorrectionMove;
            //this.cropMovingRow = DefaultMoveRow;
            //this.cropMoveWidth = DefaultCropMoveWidth;
            //this.cropMoveHeight = DefaultCropMoveHeight;
            //// crop attack
            //this.correctionAttackByX = DefaultCorrectionAttack;
            //this.cropAttackRow = DefaultAttackRow;
            //this.cropAttackWidth = DefaultCropAttackWidth;
            //this.cropAttackHeight = DefaultCropAttackHeight;
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
            this.SoundStatus = SoundStatus.Walk;
        }

        protected override void SetSoundAttackStatus()
        {
            this.SoundStatus = SoundStatus.AttackWithRangeWeapon;
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
