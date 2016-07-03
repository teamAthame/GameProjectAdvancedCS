using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthameRPG.Characters.WarUnits
{
    public class Goro : WarUnit
    {
        private const int DefaultStrengthLevel = 6;
        private const string DefaultImagePath = "../Content/Character/goro";
        private const int DefaultCropStayWidth = 69;
        private const int DefaultCropStayHeight = 97;
        private const int DefaultCropMoveWidth = 0;
        private const int DefaultCropMoveHeight = 0;
        private const int DefaultCropAttackWidth = 0;
        private const int DefaultCropAttackHeight = 0;
        private const float DefaultMoveSpeed = 2f;
        private const float DefaultAvailableMove = 200f;

        public Goro():base()
        {
            this.strengthLevel = DefaultStrengthLevel;
            this.imagePath = DefaultImagePath;
            this.warUnitDrawCoord.X = 730f;
            this.warUnitDrawCoord.Y = 200f;
            this.cropStayWidth = DefaultCropStayWidth;
            this.cropStayHeight = DefaultCropStayHeight;
            this.cropMoveWidth = 0;
            this.cropMoveHeight = 0;
            this.cropAttackWidth = 0;
            this.cropAttackHeight = 0;
            this.moveSpeed = DefaultMoveSpeed;
            this.availableMove = DefaultAvailableMove;
        }
        public Goro(bool playerUnit):base(playerUnit)
        {
            this.strengthLevel = DefaultStrengthLevel;
            this.imagePath = DefaultImagePath;
            this.warUnitDrawCoord.X = 5;
            this.warUnitDrawCoord.Y = 400;
            this.cropStayWidth = DefaultCropStayWidth;
            this.cropStayHeight = DefaultCropStayHeight;
            this.cropMoveWidth = 0;
            this.cropMoveHeight = 0;
            this.cropAttackWidth = 0;
            this.cropAttackHeight = 0;
            this.moveSpeed = DefaultMoveSpeed;
            this.availableMove = DefaultAvailableMove;
        }

        protected override float GetDefaultMove()
        {
            return DefaultAvailableMove;
        }
    }
}
