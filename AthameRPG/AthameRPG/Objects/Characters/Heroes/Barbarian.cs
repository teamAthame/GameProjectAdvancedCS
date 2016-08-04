namespace AthameRPG.Objects.Characters.Heroes
{
    using AthameRPG.GameEngine.Managers;
    using AthameRPG.Objects.Characters.WarUnits;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

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
        private const double DefaultBarbarianMove = 300;
        private const int SoundFrameSwitch = 220;
        
        private const string BarbarianImagePath = @"../Content/Character/HexenFighter";
        
        public Barbarian(float startPositionX, float startPositionY)
            : base(startPositionX, startPositionY, DefaultBarbarianAttackPoints, DefaultBarbarianHealthPoints,
                DefaultBarbarianDefencePoints)
        {
            
             //this.AtackHandler =new AtackBarbarian();
        }
        
        protected override void LoadDefaultStats()
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
            this.availableMove = DefaultBarbarianMove;
            this.defaultPlayerMove = DefaultBarbarianMove;
            this.direction = "NW";

            this.maxSoundFrameSwitch = SoundFrameSwitch;

            this.LoadDefaultStartArmy();
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            playerImage = content.Load<Texture2D>(BarbarianImagePath);
        }

        public override void UnloadContent()
        {
            
        }

        public override void Restart()
        {
            drawCoordPlayer = CharacterManager.Instance.StartPosition;
            this.abstractPlayerPositon = this.GetStartPosition;
            base.Restart();
        }

        protected override void LoadDefaultStartArmy()
        {
            this.availableCreatures.Add(new BlackDragon(true), 50);
            this.availableCreatures.Add(new Goro(true), 40);
            this.availableCreatures.Add(new Elf(true), 10);
        }
    }
}