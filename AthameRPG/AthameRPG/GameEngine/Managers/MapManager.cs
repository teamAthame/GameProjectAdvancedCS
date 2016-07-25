using System.Collections.Generic;
using AthameRPG.Enums;
using AthameRPG.GameEngine.Loaders;
using AthameRPG.Objects.BattleFields;
using AthameRPG.Objects.Characters.Heroes;
using AthameRPG.Objects.Maps;
using AthameRPG.Objects.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AthameRPG.GameEngine.Managers
{
    public class MapManager
    {
        private const string Map_List_Path = @"../../../../Content/Maps/mapList.txt";
        private const string Terrain_Path = @"../Content/MapImages/terrain";
        private const int StartingMapIndex = 0;
        private const int ValueForEnemyPath = 1;
        private const int Addition = 2;

        private static MapManager instance;

        private ContentManager contentManager;

        private Map currentMap;
        private List<string> mapListPath;
        private Texture2D terrain;
        private CharacterManager charManager;
        private BuildingManager buildingManager;
        private SandWatch sandWatch;
        private Battlefield battlefield;
        private ChangeLevel changeLevel;
        
        private bool isLevelChanging;
        private bool IsDrawLevelChanging;

        public MapManager()
        {
            this.MapIndex = StartingMapIndex; 
            this.MapListPath = new List<string>();
            this.currentMap = new Map(this.MapListPath[MapIndex]);
            this.charManager = new CharacterManager();
            this.buildingManager = new BuildingManager();
            this.sandWatch = new SandWatch();
            this.battlefield = new Battlefield();
            this.changeLevel = new ChangeLevel();
        }


        public static MapManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MapManager();
                }
                return instance;
            }
        }

        public void RestartGame()
        {
            this.isLevelChanging = false;
            this.IsDrawLevelChanging = false;
            this.MapIndex = StartingMapIndex;
            this.currentMap = new Map(this.MapListPath[MapIndex]);
        }

        public ChangeLevel ChangeLevel
        {
            get { return this.changeLevel; }
        }

        public SandWatch SandWatch
        {
            get { return this.sandWatch; }
        }

        public Battlefield Battlefield
        {
            get { return this.battlefield; }
        }
        
        public Map CurrentMap
        {
            get { return this.currentMap; }
            set { this.currentMap = value; }
        }

        public Texture2D Terrain
        {
            get
            {
                return this.terrain;
            }
        }

        private int MapIndex { get; set; }

        private List<string> MapListPath
        {
            get
            {
                return this.mapListPath;
            }
            set
            {
                this.mapListPath = FileLoader.PathsReader(Map_List_Path);
            }
        }

        public string GetEnemyBuildingFilePath
        {
            get { return this.MapListPath[this.MapIndex + ValueForEnemyPath]; }
        }

        public void LoadContent(ContentManager contentManager)
        {
            
            contentManager = new ContentManager(contentManager.ServiceProvider, "Content");
            this.contentManager = contentManager;
            this.terrain = contentManager.Load<Texture2D>(Terrain_Path);
            this.currentMap.LoadContent();
            this.charManager.LoadContent(contentManager);
            this.buildingManager.LoadContent(contentManager);
            this.battlefield.LoadContent(contentManager);
            this.changeLevel.LoadContent(contentManager);
        }

        public void UnloadContent()
        {
            currentMap.UnloadContent();
            charManager.UnloadContent();
            buildingManager.UnloadContent();
            //battlefield.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            if (this.IsDrawLevelChanging)
            {
                this.UpdateLevelChanging(gameTime);
            }
            else if (!this.IsDrawLevelChanging)
            {
                this.UpdateNormalGame(gameTime);
            }
            
        }

        private void UpdateLevelChanging(GameTime gameTime)
        {
            if (this.isLevelChanging)
            {
                this.NextLevel();
                this.changeLevel.Status = Status.Working;
                this.isLevelChanging = false;
            }

            this.changeLevel.Update(gameTime);

            if (this.changeLevel.Status == Status.Complete)
            {
                this.IsDrawLevelChanging = false;
            }
        }

        private void UpdateNormalGame(GameTime gameTime)
        {
            currentMap.Update(gameTime);
            sandWatch.Update(gameTime);
            charManager.Update(gameTime);
            buildingManager.Update(gameTime);

            if (Character.GetIsInBattle)
            {
                battlefield.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!this.IsDrawLevelChanging)
            {
                this.DrawGame(spriteBatch);
            }
            else
            {
                this.DrawChangeLevel(spriteBatch);
            }
            
        }

        private void DrawChangeLevel(SpriteBatch spriteBatch)
        {
            this.changeLevel.Draw(spriteBatch);
        }

        private void DrawGame(SpriteBatch spriteBantch)
        {
            if (!Character.GetIsInBattle && !Character.GetIsInCastle)
            {
                this.currentMap.Draw(spriteBantch);
            }
            if (!Character.GetIsInBattle)
            {
                this.buildingManager.Draw(spriteBantch);
            }
            else
            {
                this.battlefield.Draw(spriteBantch);
            }

            this.sandWatch.Draw(spriteBantch);
            this.charManager.Draw(spriteBantch);
        }

        public void TryChangeLevel(Character currentPlayer)
        {
            this.IsThereNoMoreLevels();

            this.isLevelChanging = true;
            this.IsDrawLevelChanging = true;
        }

        private void IsThereNoMoreLevels()
        {
            if (this.MapIndex + Addition >= this.MapListPath.Count)
            {
                ScreenManager.Instance.ChangeScreens("FinalScreen");
            }
        }

        private void NextLevel()
        {
            this.MapIndex += 2;

            this.buildingManager = new BuildingManager();
            this.currentMap = new Map(this.MapListPath[MapIndex]);
            this.currentMap.LoadContent();
            this.charManager.PrepareUnitsForNextLevel();

            this.buildingManager.LoadContent(this.contentManager);
        }
    }
}
