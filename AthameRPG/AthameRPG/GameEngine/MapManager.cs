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
        private static MapManager instance;
        private Texture2D terrain;
        private Map currentMap;
        public ContentManager ContentManager { get; private set; }
        private int mapIndex;
        private List<string> mapListPath;

        public MapManager()
        {
            this.MapIndex = 0;
            this.MapListPath = new List<string>();
            currentMap = new Map(MapListPath[MapIndex]);
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

        public void LoadContent(ContentManager ContentManager)
        {
            this.ContentManager = new ContentManager(ContentManager.ServiceProvider, "Content");
            terrain = ContentManager.Load<Texture2D>("../Content/Fonts/terrain");
            currentMap.LoadContent();

        }

        public void UnloadContent()
        {
            currentMap.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            currentMap.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBantch)
        {
            currentMap.Draw(spriteBantch);
        }

        
    }
}
