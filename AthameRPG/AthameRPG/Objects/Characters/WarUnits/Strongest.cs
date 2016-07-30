using System;
using AthameRPG.Contracts;
using Microsoft.Xna.Framework;

namespace AthameRPG.Objects.Characters.WarUnits
{
    public class Strongest : WarUnit
    {
        public override event OnEvent OnEvent;

        private const int DefaultStrengthLevel = 7;
        private const int DefaultHealth = 400;
        private Vector2 DefaultStartPositionInBattleLikePlayer = new Vector2(5, 5);
        private Vector2 DefaultStartPositionInBattleLikeEnemy = new Vector2(725, 5);

        public Strongest():base()
        {
            this.strengthLevel = DefaultStrengthLevel;
        }
        public Strongest(bool playerUnit) : base(playerUnit)
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
