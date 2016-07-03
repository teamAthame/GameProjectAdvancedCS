using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AthameRPG.Characters.WarUnits;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AthameRPG.Objects.BattleFields
{
    public class Battlefield
    {
        // instance/loaded from MapManager

        private const string DefaultImagePath = "../Content/Obstacles/stonebattlefield";
        private const string SmallLettersPath = "../Content/Fonts/SmallLetters";

        protected Texture2D battlefieldImage;
        protected string imagePath;
        protected Vector2 battlefieldDrawCoord;
        protected SpriteFont spriteFontSmallLetters;
        
        private Dictionary<WarUnit, decimal> playerUnits;
        private Dictionary<WarUnit, decimal> enemyUnits;

        public Battlefield()
        {
            this.imagePath = DefaultImagePath;
            this.playerUnits = new Dictionary<WarUnit, decimal>();
            this.enemyUnits = new Dictionary<WarUnit, decimal>();
        }
        
        public void LoadContent(ContentManager contentManager)
        {
            this.battlefieldImage = contentManager.Load<Texture2D>(this.imagePath);
            this.spriteFontSmallLetters = contentManager.Load<SpriteFont>(SmallLettersPath);

        }

        public void Update(GameTime gameTime)
        {
            foreach (var playerUnit in this.playerUnits)
            {
                playerUnit.Key.Update(gameTime);
            }
            foreach (var enemyUnit in this.enemyUnits)
            {
                enemyUnit.Key.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.battlefieldImage, this.battlefieldDrawCoord, Color.White);

            foreach (var playerUnit in this.playerUnits)
            {
                playerUnit.Key.Draw(spriteBatch);
                spriteBatch.DrawString(this.spriteFontSmallLetters, playerUnit.Value.ToString(),
                    playerUnit.Key.WarUnitDrawCoord, Color.White);
            }
            foreach (var enemyUnit in this.enemyUnits)
            {
                enemyUnit.Key.Draw(spriteBatch);
                spriteBatch.DrawString(this.spriteFontSmallLetters, enemyUnit.Value.ToString(),
                    new Vector2(enemyUnit.Key.WarUnitDrawCoord.X, enemyUnit.Key.WarUnitDrawCoord.Y), Color.White);
            }
        }

        public void LoadArmies(IReadOnlyDictionary<WarUnit, decimal> playerArmy, IReadOnlyDictionary<WarUnit, decimal> enemyArmy)
        {
            this.playerUnits.Clear();
            this.enemyUnits.Clear();

            this.playerUnits = (Dictionary<WarUnit, decimal>)playerArmy;
            this.enemyUnits = (Dictionary<WarUnit, decimal>)enemyArmy;
        }
    }
}
