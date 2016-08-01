using AthameRPG.Objects.Characters.WarUnits;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace AthameRPG.Objects.Castles
{
    public class StoneCastle : Castle
    {
        //public override event OnEvent OnEvent;

        // units which will be generated in this castle
        private BlackDragon blackDragon;
        private Goro goro;
        private Elf elf;
        
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
