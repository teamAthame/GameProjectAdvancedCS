using Microsoft.Xna.Framework;

namespace AthameRPG.Characters.WarUnits
{
    public class Elf : WarUnit
    {
        private const string DefaultImagePath = "../Content/Character/elf";
        private const int DefaultStrengthLevel = 5;
        private const int DefaultCropStayWidth = 62;
        private const int DefaultCropStayHeight = 90;
        private const int DefaultCorrectionCropByX = 0;
        private const int DefaultCropMoveWidth = 0;
        private const int DefaultCropMoveHeight = 0;
        private const int DefaultCropAttackWidth = 0;
        private const int DefaultCropAttackHeight = 0;
        private const float DefaultMoveSpeed = 6f;
        private const float DefaultAvailableMove = 150f;
        private const int DefaultHealth = 200;
        private const int DefaultDamage = 70;
        private const int DefaultMinAttackDistance = 650;
        private const int DefaultProtectedStep = 90;
        private const int DefaultAttackAnywayDistance = 250;

        private readonly Vector2 DefaultStartPositionInBattleLikePlayer = new Vector2(5, 400);
        private readonly Vector2 DefaultStartPositionInBattleLikeEnemy = new Vector2(725, 400);

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
