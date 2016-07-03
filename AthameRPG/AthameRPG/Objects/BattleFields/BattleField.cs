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
        protected Texture2D battlefieldImage;
        protected string imagePath;
        protected Vector2 battlefieldDrawCoord;
        //protected SpriteEffects battlefieldEffect;

        private Dictionary<WarUnit, int> playerUnits;
        private Dictionary<WarUnit, int> enemyUnits;

        public Battlefield()
        {
            this.imagePath = DefaultImagePath;
            this.playerUnits = new Dictionary<WarUnit, int>();
            this.enemyUnits = new Dictionary<WarUnit, int>();
        }
        
        public void LoadContent(ContentManager contentManager)
        {
            this.battlefieldImage = contentManager.Load<Texture2D>(this.imagePath);
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
            }
            foreach (var enemyUnit in this.enemyUnits)
            {
                enemyUnit.Key.Draw(spriteBatch);
            }
        }

        public void LoadArmies(IReadOnlyDictionary<WarUnit, int> playerArmy, IReadOnlyDictionary<WarUnit, int> enemyArmy)
        {
            this.playerUnits.Clear();
            this.enemyUnits.Clear();

            this.playerUnits = (Dictionary<WarUnit,int>)playerArmy;
            this.enemyUnits = (Dictionary<WarUnit, int>)enemyArmy;
        }
    }
}
