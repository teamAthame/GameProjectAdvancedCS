using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using AthameRPG.GameEngine;

namespace AthameRPG.Characters
{
    public abstract class Character : Unit
    {
        /// <summary>
        /// Unit is  abstract class for ALL GOOD PLAYERS
        /// </summary>

        protected const int cropWidth = 32;
        protected const int cropHeight = 48;

        protected static Vector2 drawCoordPlayer;

        public Character(float startPositionX, float startPositionY) : base(startPositionX, startPositionY )
        {
            drawCoordPlayer = new Vector2(startPositionX - cropWidth / 2, startPositionY - cropHeight / 2);
        }

        public static Vector2 DrawCoordPlayer
        {
            get
            {
                return drawCoordPlayer;
            }
        }

        public static int PlayerCropWidth
        {
            get
            {
                return cropWidth;
            }
        }
        public static int PlayerCropHeight
        {
            get
            {
                return cropHeight;
            }
        }

        

    }
}
