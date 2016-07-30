using AthameRPG.Objects.Characters.WarUnits;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AthameRPG.Objects.Characters.Heroes
{
    public class Gargamel : Enemy
    {
        //public override event OnEvent OnEvent;

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
        private const int SoundFrameSwitch = 300;
        
        public Gargamel(float startPositionX, float startPositionY, int id)
            : base(
                startPositionX, startPositionY, id, DefaultWariorAttackPoints, DefaultWariorHealthPoints,
                DefaultWariorDefence)
        {
            this.coordGargamel = new Vector2(startPositionX - (cropWidth/2f), startPositionY - (cropHeight/2f));
            
        }

        protected override void LoadDefaultStats()
        {
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
            this.defaultPlayerMove = DefaultGargamelMove;
            this.maxSoundFrameSwitch = SoundFrameSwitch;

            this.LoadDefaultStartArmy();
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            enemyImage = content.Load<Texture2D>(PathGargamelImage);
            
        }
        
        protected override void LoadDefaultStartArmy()
        {
            //this.availableCreatures.Add(new BlackDragon(), 20);
            //this.availableCreatures.Add(new Goro(), 3);
            this.availableCreatures.Add(new Elf(), 1);
        }
    }
}