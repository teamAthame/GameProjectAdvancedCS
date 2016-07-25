﻿using Microsoft.Xna.Framework;
namespace AthameRPG.Characters.WarUnits
{
    public class Goro : WarUnit
    {
        private const int DefaultStrengthLevel = 6;
        private const string DefaultImagePath = "../Content/Character/goro";
        //private const int DefaultStayRow = 0;
        //private const int DefaultCropStayWidth = 50;
        //private const int DefaultCropStayHeight = 95;
        //private const int DefaultMoveRow = 0; //-------------------------------
        //private const int DefaultCropMoveWidth = 0;
        //private const int DefaultCropMoveHeight = 0;
        //private const int DefaultAttackRow = 0; //----------------------------
        //private const int DefaultCropAttackWidth = 0;
        //private const int DefaultCropAttackHeight = 0;
        //private const int DefaultCorrectionCropByX = 7;

        private const int DefaultProtectedStep = 90;
        private const float DefaultMoveSpeed = 2f;
        private const float DefaultAvailableMove = 200f;
        private const int DefaultHealth = 400;
        private const int DefaultDamage = 150;
        private const int DefaultAttackAnywayDistance = 200;

        // in this case Goro is archer
        private const int DefaultMinAttackDistance = 10;

        private Vector2 DefaultStartPositionInBattleLikePlayer = new Vector2(5, 200);
        private Vector2 DefaultStartPositionInBattleLikeEnemy = new Vector2(725, 200);

        public Goro():base()
        {
            this.warUnitDrawCoord = DefaultStartPositionInBattleLikeEnemy;
            this.amIArcherOrMage = false;

            
        }
        public Goro(bool playerUnit):base(playerUnit)
        {
            this.warUnitDrawCoord = DefaultStartPositionInBattleLikePlayer;
            this.amIArcherOrMage = false;

            
            
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

            this.cropWidth = 55; //---------------------------------
            this.cropHeight = 80; //--------------------------------

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

    }
}