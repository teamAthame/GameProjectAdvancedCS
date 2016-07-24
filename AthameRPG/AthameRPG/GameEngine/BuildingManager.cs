using System.Collections.Generic;
using AthameRPG.Objects.Castles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AthameRPG.GameEngine
{
    public class BuildingManager
    {
        private static List<Castle> listOfCastlesOnMap;
        
        public BuildingManager()
        {
            listOfCastlesOnMap = new List<Castle>();
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
            //listOfCastlesOnMap.Clear();
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

        public static void AddCastleFromTxtMapToList(Castle castle)
        {
            listOfCastlesOnMap.Clear();
            listOfCastlesOnMap.Add(castle);
        }
    }
}
