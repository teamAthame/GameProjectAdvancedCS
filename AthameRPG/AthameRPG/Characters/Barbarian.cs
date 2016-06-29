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
        private const int DefaultBarbarianAnimationCropCropStay = 360;
        private const int DefaultBarbarianAnimationCropNorth = 395;
        private const int DefaultBarbarianAnimationCropSouth = 20;
        private const int DefaultBarbarianAnimationCropEast = 580;
        private const int DefaultBarbarianAnimationCropWest = 210;
        private const int DefaultBarbarianAnimationCropNorthEast = 485;
        private const int DefaultBarbarianAnimationCropNorthWest = 300;
        private const int DefaultBarbarianAnimationCropSouthEast = 675;
        private const int DefaultBarbarianAnimationCropSouthWest = 120;
        private const int DefaultBarbarianCropWidth = 70;
        private const int DeafaultBarbarianCropHeight = 80;
        private const float DefaultBarbarianMoveSpeed = 3f;

        //private Texture2D playerImage;
        private const string PATH_BARBARIAN_IMAGE = @"../Content/Character/HexenFighter";
        
        private ContentManager content;
        
        public Barbarian(float startPositionX, float startPositionY)
            : base(startPositionX, startPositionY, DefaultBarbarianAttackPoints, DefaultBarbarianHealthPoints,
                DefaultBarbarianDefencePoints)
        {
            cropWidth = DefaultBarbarianCropWidth;
            cropHeight = DeafaultBarbarianCropHeight;
            this.moveSpeedPlayer = DefaultBarbarianMoveSpeed;
            this.cropStay = DefaultBarbarianAnimationCropCropStay;
            this.north = DefaultBarbarianAnimationCropNorth;
            this.south = DefaultBarbarianAnimationCropSouth;
            this.east = DefaultBarbarianAnimationCropEast;
            this.west = DefaultBarbarianAnimationCropWest;
            this.northEast = DefaultBarbarianAnimationCropNorthEast;
            this.northWest = DefaultBarbarianAnimationCropNorthWest;
            this.southEast = DefaultBarbarianAnimationCropSouthEast;
            this.southWest = DefaultBarbarianAnimationCropSouthWest;
            
            //this.AtackHandler =new AtackBarbarian();
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            playerImage = content.Load<Texture2D>(PATH_BARBARIAN_IMAGE);
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
    
    
    
    

