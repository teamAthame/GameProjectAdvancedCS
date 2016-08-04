namespace AthameRPG.GameEngine.Managers
{
    using System.Collections.Generic;
    using AthameRPG.Contracts;
    using AthameRPG.Objects.Castles;
    using AthameRPG.Objects.UI;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

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
                this.SubscribeForSoundEvents(castle);
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
            SandWatch.TurnIsClicked = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var castle in listOfCastlesOnMap)
            {
                castle.Draw(spriteBatch);
            }
        }

        private void SubscribeForSoundEvents(ISoundable soundable)
        {
            soundable.OnEvent += ScreenManager.Instance.SoundEffectManager.ExecuteQuery;
        }
        
        public static void AddCastleFromTxtMapToList(Castle castle)
        {
            listOfCastlesOnMap.Add(castle);
        }

        public static void Restart()
        {
            foreach (var castle in listOfCastlesOnMap)
            {
                castle.OnEvent -= ScreenManager.Instance.SoundEffectManager.ExecuteQuery;
            }

            listOfCastlesOnMap.Clear();
        }
    }
}
