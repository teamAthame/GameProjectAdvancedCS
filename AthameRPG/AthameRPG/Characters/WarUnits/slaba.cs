﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace AthameRPG.Characters.WarUnits
{
    public class Slaba : WarUnit
    {
        // това трябва да го има във всяка гадина  !!!
        private const int DefaultStrengthLevel = 2;
        private const int DefaultHealth = 400;
        private Vector2 DefaultStartPositionInBattleLikePlayer = new Vector2(5, 5);
        private Vector2 DefaultStartPositionInBattleLikeEnemy = new Vector2(725, 5);

        public Slaba()
        {
            this.strengthLevel = DefaultStrengthLevel;
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
