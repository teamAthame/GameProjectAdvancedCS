using Microsoft.Xna.Framework;
namespace AthameRPG.Characters.WarUnits
{
    public class BlackDragon : WarUnit
    {
        private const int DefaultStrengthLevel = 7;
        private const string DefaultImagePath = "../Content/Character/blackDragon";
        private const int DefaultCropStayWidth = 69;
        private const int DefaultCropStayHeight = 97;
        private const int DefaultCropMoveWidth = 0;
        private const int DefaultCropMoveHeight = 0;
        private const int DefaultCropAttackWidth = 0;
        private const int DefaultCropAttackHeight = 0;
        private const float DefaultMoveSpeed = 6f;
        private const float DefaultAvailableMove = 300f;
        private const int DefaultHealth = 500;
        private const int DefaultMinAttackDistance = 10;
        private readonly Vector2 DefaultStartPositionInBattleLikePlayer = new Vector2(5,5);
        private readonly Vector2 DefaultStartPositionInBattleLikeEnemy = new Vector2(725, 5);

        public BlackDragon() : base()
        {
            this.strengthLevel = DefaultStrengthLevel;
            this.imagePath = DefaultImagePath;
            this.warUnitDrawCoord = DefaultStartPositionInBattleLikeEnemy;
            //this.warUnitDrawCoord.X = 725;
            //this.warUnitDrawCoord.Y = 0;
            this.cropStayWidth = DefaultCropStayWidth;
            this.cropStayHeight = DefaultCropStayHeight;
            this.cropMoveWidth = 0;
            this.cropMoveHeight = 0;
            this.cropAttackWidth = 0;
            this.cropAttackHeight = 0;
            this.moveSpeed = DefaultMoveSpeed;
            this.availableMove = DefaultAvailableMove;
            this.damage = 200;
            this.health = DefaultHealth;
            this.minAttackDistance = DefaultMinAttackDistance;
        }

        public BlackDragon(bool playerUnit):base(playerUnit)
        {
            this.strengthLevel = DefaultStrengthLevel;
            this.imagePath = DefaultImagePath;
            this.warUnitDrawCoord = this.DefaultStartPositionInBattleLikePlayer;
            //this.warUnitDrawCoord.X = 5;
            //this.warUnitDrawCoord.Y = 5;
            this.cropStayWidth = DefaultCropStayWidth;
            this.cropStayHeight = DefaultCropStayHeight;
            this.cropMoveWidth = 0;
            this.cropMoveHeight = 0;
            this.cropAttackWidth = 0;
            this.cropAttackHeight = 0;
            this.moveSpeed = DefaultMoveSpeed;
            this.availableMove = DefaultAvailableMove;
            this.damage = 200;
            this.health = DefaultHealth;
            this.minAttackDistance = DefaultMinAttackDistance;
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
