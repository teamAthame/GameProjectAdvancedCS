using AthameRPG.Characters.WarUnits;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AthameRPG.GameEngine;
using Microsoft.Xna.Framework.Content;

namespace AthameRPG.Characters
{
    public class Gargamel : Enemy
    {

        private const int DefaultWariorAttackPoints = 80;
        private const int DefaultWariorHealthPoints = 80;
        private const int DefaultWariorDefence = 40;
        private const string PathGargamelImage = @"../Content/Character/GoblinWalk";

        // Animation values
        private const int DefaultGargamelDirectionCropStay = 0;
        private const int DefaultGargamelDirectionNorth = 440;
        private const int DefaultGargamelDirectionSouth = 15;
        private const int DefaultGargamelDirectionEast = 660;
        private const int DefaultGargamelDirectionWest = 224;
        private const int DefaultGargamelDirectionNorthEast = 550;
        private const int DefaultGargamelDirectionNorthWest = 330;
        private const int DefaultGargamelDirectionSouthEast = 770;
        private const int DefaultGargamelDirectionSouthWest = 122;
        private const double DefaultGargamelMove = 90;
        
        public Gargamel(float startPositionX, float startPositionY, int id)
            : base(
                startPositionX, startPositionY, id, DefaultWariorAttackPoints, DefaultWariorHealthPoints,
                DefaultWariorDefence)
        {
            this.coordGargamel = new Vector2(startPositionX - (cropWidth/2f), startPositionY - (cropHeight/2f));
            this.direction = "NW";
            this.cropStay = DefaultGargamelDirectionCropStay;
            this.north = DefaultGargamelDirectionNorth;
            this.south = DefaultGargamelDirectionSouth;
            this.east = DefaultGargamelDirectionEast;
            this.west = DefaultGargamelDirectionWest;
            this.northEast = DefaultGargamelDirectionNorthEast;
            this.northWest = DefaultGargamelDirectionNorthWest;
            this.southEast = DefaultGargamelDirectionSouthEast;
            this.southWest = DefaultGargamelDirectionSouthWest;
            //this is only test ... it can be smth like this - this.availableCreatures.Add(new NaiSlaba(), 5 * creature_level); !!! idea is yours
            // or it can be randomized
            //this.availableCreatures.Add(new NaiSlaba(), 107);
            //this.availableCreatures.Add(new NaiSilna(), 2);
            //this.availableCreatures.Add(new Sredna(), 34);
            this.availableCreatures.Add(new BlackDragon(), 1);
            this.availableCreatures.Add(new Goro(), 1);

            this.defaultPlayerMove = DefaultGargamelMove;


        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            enemyImage = content.Load<Texture2D>(PathGargamelImage);
        }


    }
}
