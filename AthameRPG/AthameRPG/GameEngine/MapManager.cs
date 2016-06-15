using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using AthameRPG.GameEngine.Maps;

namespace AthameRPG.GameEngine
{
    public class MapManager
    {
        private const string MAP_LIST_PATH = @"../../../../Content/Maps/mapList.txt";
        private Texture2D terrain;
        private int mapIndex;
        private List<string> mapListPath;
        private static MapManager instance;
        private Map currentMap;
        private CharacterManager charManager;

        public MapManager()
        {
            this.MapIndex = 0; /// TODO 
            this.MapListPath = new List<string>();
            currentMap = new Map(MapListPath[MapIndex]);
            charManager = new CharacterManager();
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
                this.mapListPath = FileLoader.PathsReader(MAP_LIST_PATH);
            }
        }

        public ContentManager ContentManager { get; private set; }

        public void LoadContent(ContentManager ContentManager)
        {
            this.ContentManager = new ContentManager(ContentManager.ServiceProvider, "Content");
            terrain = ContentManager.Load<Texture2D>("../Content/MapImages/terrain");
            currentMap.LoadContent();
            charManager.LoadContent(ContentManager);

        }

        public void UnloadContent()
        {
            currentMap.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            currentMap.Update(gameTime);
            charManager.Update(gameTime);


        }

        public void Draw(SpriteBatch spriteBantch)
        {
            currentMap.Draw(spriteBantch);
            charManager.Draw(spriteBantch);
        }

        
    }
}
