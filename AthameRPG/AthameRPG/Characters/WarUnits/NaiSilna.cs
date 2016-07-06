using System;
using Microsoft.Xna.Framework;

namespace AthameRPG.Characters.WarUnits
{
    public class NaiSilna : WarUnit
    {
        // това трябва да го има във всяка гадина  !!!
        private const int DefaultStrengthLevel = 7;
        private const int DefaultHealth = 400;
        private Vector2 DefaultStartPositionInBattleLikePlayer = new Vector2(5, 5);
        private Vector2 DefaultStartPositionInBattleLikeEnemy = new Vector2(725, 5);

        public NaiSilna():base()
        {
            this.strengthLevel = DefaultStrengthLevel;
        }
        public NaiSilna(bool playerUnit) : base(playerUnit)
        {
            this.strengthLevel = DefaultStrengthLevel;
        }

        protected override void LoadDefaultUnitStats()
        {
            
        }

        public override int GetDefaultHeаlth()
        {
            throw new NotImplementedException();
        }

        protected override float GetDefaultMove()
        {
            throw new NotImplementedException();
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
