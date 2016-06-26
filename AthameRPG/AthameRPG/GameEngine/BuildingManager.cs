using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AthameRPG.Objects.Castles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AthameRPG.GameEngine
{
    public class BuildingManager
    {
        private static List<MainCastle> listOfCastlesOnMap;

        public BuildingManager()
        {
            listOfCastlesOnMap = new List<MainCastle>();
        }
        
        public void LoadContent(ContentManager content)
        {
            foreach (var castle in listOfCastlesOnMap)
            {
                castle.LoadContent(content);
            }

        }

        public void UnloadContent()
        {

        }

        public void Update(GameTime gameTime)
        {
            foreach (var castle in listOfCastlesOnMap)
            {
                castle.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var castle in listOfCastlesOnMap)
            {
                castle.Draw(spriteBatch);
            }
        }

        public static void AddCastleFromTxtMapToList(MainCastle castle)
        {
            listOfCastlesOnMap.Add(castle);
        }
    }
}
