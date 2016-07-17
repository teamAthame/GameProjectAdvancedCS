﻿using System.Collections.Generic;
using AthameRPG.Characters.Heroes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using AthameRPG.GameEngine.Maps;
using AthameRPG.Objects.BattleFields;
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
        private Battlefield battlefield;


        public MapManager()
        {
            this.MapIndex = 0; /// TODO 
            this.MapListPath = new List<string>();
            this.currentMap = new Map(MapListPath[MapIndex]);
            this.charManager = new CharacterManager();
            this.buildingManager = new BuildingManager();
            this.sandWatch = new SandWatch();
            this.battlefield = new Battlefield();
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

        //public ContentManager ContentManager { get; private set; }

        public void LoadContent(ContentManager ContentManager)
        {
            ContentManager = new ContentManager(ContentManager.ServiceProvider, "Content");
            terrain = ContentManager.Load<Texture2D>(Terrain_Path);
            currentMap.LoadContent();
            charManager.LoadContent(ContentManager);
            buildingManager.LoadContent(ContentManager);
            battlefield.LoadContent(ContentManager);

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
            currentMap.Update(gameTime);
            sandWatch.Update(gameTime);
            charManager.Update(gameTime);
            buildingManager.Update(gameTime);

            if (Character.GetIsInBattle)
            {
                battlefield.Update(gameTime);
            }

        }

        public void Draw(SpriteBatch spriteBantch)
        {
            if (!Character.GetIsInBattle && !Character.GetIsInCastle)
            {
                currentMap.Draw(spriteBantch);
            }
            if (!Character.GetIsInBattle)
            {
                buildingManager.Draw(spriteBantch);
            }
            else
            {
                battlefield.Draw(spriteBantch);
            }
            
            sandWatch.Draw(spriteBantch);
            charManager.Draw(spriteBantch);
        }

        
    }
}
