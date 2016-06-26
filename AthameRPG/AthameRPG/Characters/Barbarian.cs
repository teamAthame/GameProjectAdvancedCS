using AthameRPG.GameEngine;
using AthameRPG.GameEngine.Maps;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AthameRPG.Characters
{
    using Atack;
    using AthameRPG.Controls;

    public class Barbarian : Character
    {

        private const int DefaultBarbarianAttackPoints = 120;
        private const int DefaultBarbarianHealthPoints = 180;
        private const int DefaultBarbarianDefencePoints = 70;
        
        //private Texture2D playerImage;
        private const string PATH_BARBARIAN_IMAGE = @"../Content/Character/HexenFighter";
        
        private ContentManager content;
        
        public Barbarian(float startPositionX, float startPositionY)
            : base(startPositionX, startPositionY, DefaultBarbarianAttackPoints, DefaultBarbarianHealthPoints,
                DefaultBarbarianDefencePoints)
        {
            this.cropStay = 360;
            this.north = 395;
            this.south = 20;
            this.east = 580;
            this.west = 210;
            this.northEast = 485;
            this.northWest = 300;
            this.southEast = 675;
            this.southWest = 120;
            
            //this.AtackHandler =new AtackBarbarian();
        }
        
        public override void LoadContent()
        {

            // komplekt ! :)
            content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");
            playerImage = content.Load<Texture2D>(PATH_BARBARIAN_IMAGE);

            //font = content.Load<SpriteFont>("../Content/Fonts/ArialBig");
        }

        public override void UnloadContent()
        {
            
        }

        //public override void Draw(SpriteBatch spriteBatch)
        //{

        //    //spriteBatch.Draw(playerImage, drawCoordPlayer, this.CropCurrentFrame, Color.White);


        //    // for TEST
        //    //spriteBatch.DrawString(font, playerPositon.X + " " + playerPositon.Y, new Vector2(30, 30), Color.Blue);
        //    //spriteBatch.DrawString(font, CharacterManager.enemiesList[0].GARGAcoor.X + " " + CharacterManager.enemiesList[0].GARGAcoor.Y, new Vector2(30, 70), Color.AliceBlue);
        //    //spriteBatch.DrawString(font, CharacterManager.enemiesList[0].GARGA.X + " " + CharacterManager.enemiesList[0].GARGA.Y, new Vector2(30, 110), Color.AliceBlue);
        //}


    }
}
    
    
    
    

