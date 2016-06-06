using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace AthameRPG.GameEngine.Maps
{
    public class Wall : Map
    {
        private Rectangle cropWall;
        private Vector2 coordinates;
        private Texture2D image;

        public Rectangle CropWall { get; set; }

        public Wall(string mapPath) : base(mapPath)
        {
            this.cropWall = new Rectangle(150, 0, 50, 50);
            
        }

         
    }
}
