namespace AthameRPG.Objects.Castles
{
    using AthameRPG.Objects.Characters.WarUnits;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;

    public class StoneCastle : Castle
    {
        private const int DefaultCastleImageWidth = 250;
        private const int DefaultCastleImageHeight = 200;
        private const int CurrentRowOnImage = 0;
        private const int CurrentColOnImage = 0;
        private const string DefaultInsideCastlePath = @"../Content/Obstacles/insideGargamel";
        
        public StoneCastle(Vector2 coordinatesOnMap) : base(coordinatesOnMap)
        {
            this.insideFirstCastlePath = DefaultInsideCastlePath;
            this.cropCurrentCastleImageWidth = DefaultCastleImageWidth;
            this.cropCurrentCastleImageHeight = DefaultCastleImageHeight;
            
            this.cropCastle = new Rectangle(CurrentColOnImage, CurrentRowOnImage, this.cropCurrentCastleImageWidth,
                this.cropCurrentCastleImageHeight);

        }

        // units which will be generated in this castle
        protected override void LoadUnitsThatWillBeGenerated()
        {
            this.gadini.Add(new BlackDragon(true), 0);
            this.gadini.Add(new Goro(true), 0);
            this.gadini.Add(new Elf(true), 0);
        }

        public override void LoadContent(ContentManager Content)
        {
            base.LoadContent(Content);

            foreach (var gadina in this.gadini)
            {
                gadina.Key.LoadContent(Content);
            }

        }
    }
}
