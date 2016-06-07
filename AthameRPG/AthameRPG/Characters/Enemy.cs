using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthameRPG.Characters
{
    public abstract class Enemy : Unit
    {
        public Enemy(float startPositionX, float startPositionY) : base(startPositionX, startPositionY)
        {

        }
    }
}
