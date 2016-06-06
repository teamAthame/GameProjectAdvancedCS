using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AthameRPG.GameEngine.Maps
{
    class VerticalBricks : Map
    {
        private Rectangle cropWall;
        private Vector2 coordinates;

        public Rectangle CropWall { get; set; }

        public VerticalBricks(string mapPath) : base(mapPath)
        {
            this.cropWall = new Rectangle(150, 0, 50, 50);
        }
    }
}
