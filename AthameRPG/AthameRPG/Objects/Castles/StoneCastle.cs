using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AthameRPG.Characters.WarUnits;
using AthameRPG.GameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AthameRPG.Objects.Castles
{
    public class StoneCastle : MainCastle
    {
        // gadinite koito shte se prodavat v zamaka .... добавете вашите !!!
        private BlackDragon blackDragon;
        private Goro goro;
        private NaiSilna naiSilna;
        private NaiSlaba naiSlaba;
        private Silna silna;
        private Slaba slaba;
        private Sredna sredna;
        private SrednoSilna srednoSilna;
        private SrednoSlaba srednoSlaba;

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
            this.silna = new Silna();
            this.cropCastle = new Rectangle(CurrentColOnImage, CurrentRowOnImage, this.cropCurrentCastleImageWidth,
                this.cropCurrentCastleImageHeight);

            // опиши/вкарай какви гадини искаш да има съответния замък.
            this.gadini.Add(new BlackDragon(true), 0);
            this.gadini.Add(new Goro(true), 0);

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
