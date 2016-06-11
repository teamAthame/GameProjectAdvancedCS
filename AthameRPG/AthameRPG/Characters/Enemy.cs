using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthameRPG.Characters
{
    public abstract class Enemy : Unit
    {
        //public const int cropWidth = 75;
        //public const int cropHeight = 75;
        //public const int cropWidth = 90;
        //public const int cropHeight = 121;
        public const int cropWidth = 50;
        public const int cropHeight = 50;

        private int id;//  NUMBER IN THE ENEMY LIST //

        public static float SearchingRadius
        {
            get { return 100f; }
        }

        public Enemy(float startPositionX, float startPositionY, int id) : base(startPositionX, startPositionY)
        {
            this.ID = id;
        }

        public int ID
        {
            get
            {
                return this.id;
            }
            private set
            {
                this.id = value;
            }
        }

        
    }
}
