using Microsoft.Xna.Framework;
namespace AthameRPG.Characters.WarUnits
{
    public class BlackDragon : WarUnit
    {
        private const int DefaultStrengthLevel = 7;
        private const string DefaultImagePath = "../Content/Character/blackDragon";
        private const int DefaultCropStayWidth = 69;
        private const int DefaultCropStayHeight = 97;
        private const int DefaultCorrectionCropByX = 326;
        private const int DefaultCropMoveWidth = 0;
        private const int DefaultCropMoveHeight = 0;
        private const int DefaultCropAttackWidth = 0;
        private const int DefaultCropAttackHeight = 0;
        private const float DefaultMoveSpeed = 6f;
        private const float DefaultAvailableMove = 300f;
        private const int DefaultHealth = 500;
        private const int DefaultDamage = 200;
        private const int DefaultProtectedStep = 90;
        private const int DefaultAttackAnywayDistance = 250;
        private const int DefaultMinAttackDistance = 10;

        private readonly Vector2 DefaultStartPositionInBattleLikePlayer = new Vector2(5,5);
        private readonly Vector2 DefaultStartPositionInBattleLikeEnemy = new Vector2(725, 5);

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
            this.cropStayWidth = DefaultCropStayWidth;
            this.cropStayHeight = DefaultCropStayHeight;
            this.cropMoveWidth = 0;
            this.cropMoveHeight = 0;
            this.cropAttackWidth = 0;
            this.cropAttackHeight = 0;
            this.moveSpeed = DefaultMoveSpeed;
            this.availableMove = DefaultAvailableMove;
            this.damage = DefaultDamage;
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

        
    }
}
