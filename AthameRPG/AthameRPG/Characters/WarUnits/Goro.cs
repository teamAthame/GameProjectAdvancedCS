using Microsoft.Xna.Framework;
namespace AthameRPG.Characters.WarUnits
{
    public class Goro : WarUnit
    {
        private const int DefaultStrengthLevel = 6;
        private const string DefaultImagePath = "../Content/Character/goro";
        private const int DefaultCropStayWidth = 50;
        private const int DefaultCropStayHeight = 95;
        private const int DefaultCropMoveWidth = 0;
        private const int DefaultCropMoveHeight = 0;
        private const int DefaultCropAttackWidth = 0;
        private const int DefaultCropAttackHeight = 0;
        private const int DefaultCorrectionCropByX = 7;

        private const int DefaultProtectedStep = 90;
        private const float DefaultMoveSpeed = 2f;
        private const float DefaultAvailableMove = 200f;
        private const int DefaultHealth = 400;
        private const int DefaultAttackAnywayDistance = 100;

        // in this case Goro is archer
        private const int DefaultMinAttackDistance = 500;

        private Vector2 DefaultStartPositionInBattleLikePlayer = new Vector2(5, 400);
        private Vector2 DefaultStartPositionInBattleLikeEnemy = new Vector2(725, 200);

        public Goro():base()
        {
            this.warUnitDrawCoord = DefaultStartPositionInBattleLikeEnemy;
            this.amIArcherOrMage = true;

            
        }
        public Goro(bool playerUnit):base(playerUnit)
        {
            this.warUnitDrawCoord = DefaultStartPositionInBattleLikePlayer;
            this.amIArcherOrMage = true;

            
            
        }

        protected override void LoadDefaultUnitStats()
        {
            this.strengthLevel = DefaultStrengthLevel;
            this.imagePath = DefaultImagePath;
            this.cropStayWidth = DefaultCropStayWidth;
            this.cropStayHeight = DefaultCropStayHeight;
            this.cropMoveWidth = 0;
            this.cropMoveHeight = 0;
            this.cropAttackWidth = 0;
            this.cropAttackHeight = 0;
            this.moveSpeed = DefaultMoveSpeed;
            this.availableMove = DefaultAvailableMove;
            this.damage = 150;
            this.health = DefaultHealth;
            this.minAttackDistance = DefaultMinAttackDistance;
            this.correctionStayCropByX = DefaultCorrectionCropByX;
            this.protectedStep = DefaultProtectedStep;
            this.attackAnywayDistance = DefaultAttackAnywayDistance;
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
