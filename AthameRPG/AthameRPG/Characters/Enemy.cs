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
        
        public const int cropWidth = 80;
        public const int cropHeight = 85;

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
