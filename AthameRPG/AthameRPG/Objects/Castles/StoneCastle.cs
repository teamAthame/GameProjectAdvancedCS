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
        // gadinite koito shte se prodavat v zamaka .... добавете вашите !
        private NaiSilna naiSilna;
        private NaiSlaba naiSlaba;
        private Silna silna;
        private Slaba slaba;
        private Sredna sredna;
        private SrednoSilna srednoSilna;
        private SrednoSlaba srednoSlaba;

        private const int CropCurrentCastleImageWidth = 250;
        private const int CropCurrentCastleImageHeight = 200;
        private const int CurrentRowOnImage = 0;
        private const int CurrentColOnImage = 0;

        public StoneCastle(Vector2 coordinatesOnMap) : base(coordinatesOnMap)
        {
            this.silna = new Silna();
            this.cropCastle = new Rectangle(CurrentColOnImage, CurrentRowOnImage, CropCurrentCastleImageWidth,
                CropCurrentCastleImageHeight);
            this.naiSilna = new NaiSilna();
            this.silna = new Silna();
            this.srednoSilna = new SrednoSilna();
            this.sredna = new Sredna();
            this.srednoSlaba = new SrednoSlaba();
            this.slaba = new Slaba();
            this.naiSlaba = new NaiSlaba();
            this.gadini.Add(naiSilna, 0m);
            this.gadini.Add(silna, 0m);
            this.gadini.Add(srednoSilna, 0m);
            this.gadini.Add(sredna, 0m);
            this.gadini.Add(srednoSlaba, 0m);
            this.gadini.Add(slaba, 0m);
            this.gadini.Add(naiSlaba, 0m);
        }
        
        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            this.drawCoordinates.X = this.coordinatesOnMap.X + CharacterManager.barbarian.CoordP().X;
            this.drawCoordinates.Y = this.coordinatesOnMap.Y + CharacterManager.barbarian.CoordP().Y;

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(imageOfCastleOutside, this.drawCoordinates, this.cropCastle, Color.White);

        }
    }
}
