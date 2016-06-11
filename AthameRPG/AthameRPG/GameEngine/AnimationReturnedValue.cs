﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace AthameRPG.GameEngine
{
    public class AnimationReturnedValue
    {
        private string direction;
        private Rectangle imageCrop;

        public AnimationReturnedValue()
        {
            
        }

        public AnimationReturnedValue(string direction, Rectangle imageCrop)
        {
            this.Direction = direction;
            this.ImageCrop = imageCrop;
        }

        public string Direction
        {
            get
            {
                return this.direction;
                
            }
            private set { this.direction = value; }
        }

        public Rectangle ImageCrop { get;private set; }
    }
}
