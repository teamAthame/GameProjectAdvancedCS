using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using AthameRPG.Characters.Heroes;
using AthameRPG.Enums;
using AthameRPG.GameEngine.Maps;
using AthameRPG.Objects.BattleFields;
using AthameRPG.Objects.UI;

namespace AthameRPG.GameEngine
{
    public class MapManager
    {
        private const string Map_List_Path = @"../../../../Content/Maps/mapList.txt";
        private const string Terrain_Path = @"../Content/MapImages/terrain";
        private const int StartingMapIndex = 0;
        private const int ValueForEnemyPath = 1;

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
                if (this.isLevelChanging)
                {
                    this.ChangeLevel();
                    this.changeLevel.Status = Status.Working;
                    this.isLevelChanging = false;
                }
                
                this.changeLevel.Update(gameTime);

                if (this.changeLevel.Status == Status.Complete)
                {
                    this.IsDrawLevelChanging = false;
                }
            }
            else if (!this.IsDrawLevelChanging)
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
            //this.IsThereNoMoreLevels();

            this.isLevelChanging = true;
            this.IsDrawLevelChanging = true;
        }

        private void ChangeLevel()
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
