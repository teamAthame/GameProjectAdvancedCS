using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using AthameRPG.GameEngine.Maps;
using AthameRPG.Objects.UI;

namespace AthameRPG.GameEngine
{
    public class MapManager
    {
        private const string Map_List_Path = @"../../../../Content/Maps/mapList.txt";
        private const string Terrain_Path = @"../Content/MapImages/terrain";
        private Texture2D terrain;
        private int mapIndex;
        private List<string> mapListPath;
        private static MapManager instance;
        private Map currentMap;
        private CharacterManager charManager;
        private BuildingManager buildingManager;
        private SandWatch sandWatch;

        public MapManager()
        {
            this.MapIndex = 0; /// TODO 
            this.MapListPath = new List<string>();
            this.currentMap = new Map(MapListPath[MapIndex]);
            this.charManager = new CharacterManager();
            this.buildingManager = new BuildingManager();
            this.sandWatch = new SandWatch();
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

        private int MapIndex
        {
            get
            {
                return this.mapIndex;
            }
            set
            {
                this.mapIndex = value;
            }
        }

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

        public ContentManager ContentManager { get; private set; }

        public void LoadContent(ContentManager ContentManager)
        {
            this.ContentManager = new ContentManager(ContentManager.ServiceProvider, "Content");
            terrain = ContentManager.Load<Texture2D>(Terrain_Path);
            currentMap.LoadContent();
            charManager.LoadContent(ContentManager);
            buildingManager.LoadContent(ContentManager);

        }

        public void UnloadContent()
        {
            currentMap.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            currentMap.Update(gameTime);
            sandWatch.Update(gameTime);
            charManager.Update(gameTime);
            buildingManager.Update(gameTime);

        }

        public void Draw(SpriteBatch spriteBantch)
        {
            currentMap.Draw(spriteBantch);
            buildingManager.Draw(spriteBantch);
            sandWatch.Draw(spriteBantch);
            charManager.Draw(spriteBantch);
        }

        
    }
}
