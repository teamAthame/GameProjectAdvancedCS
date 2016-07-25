﻿using System.Collections.Generic;
using System.Linq;
using AthameRPG.GameEngine.Loaders;
using AthameRPG.GameEngine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AthameRPG.Objects.Maps
{
    public class Map 
    {
        private string mapPath;
        private List<List<int>> fullMap;
        private Rectangle cropWall;
        private Vector2 currentCoord;
        private static List<Vector2> obstacles;


        public Map(string mapPath)
        {
            this.MapPath = mapPath;
            this.FullMap = new List<List<int>>();
            obstacles = new List<Vector2>();
            
        }
        

        public string MapPath
        {
            get
            {
                return this.mapPath;
            }
            private set
            {
                this.mapPath = value;
            }
        }

        public static List<Vector2> Obstacles
        {
            get
            {
                List<Vector2> copyOfObstacles = obstacles;
                return copyOfObstacles;
            }
            private set
            {
                obstacles = value;
            }
        }

        public void AddObstacle(Vector2 coordObstacle)
        {
            obstacles.Add(coordObstacle); 
        }

        public void LoadContent()
        {
            obstacles.Clear();
            this.FillListOfObstacles();
        }

        private void FillListOfObstacles()
        {
            for (int i = 0; i < FullMap.Count(); i++)
            {
                for (int j = 0; j < FullMap[i].Count(); j++)
                {

                    int currentNum = FullMap[i][j];

                    switch (currentNum)
                    {
                        case 1:
                            AddObstacle(new Vector2((float)(j * 50), (float)(i * 50)));
                            break;
                        
                        default:
                            break;
                    }
                }
            }
        }

        public void UnloadContent()
        {
            
        }

        public void Update(GameTime gameTime)
        {
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            this.DrawWorld(spriteBatch);

        }

        private void DrawWorld(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < FullMap.Count(); i++)
            {
                for (int j = 0; j < FullMap[i].Count(); j++)
                {
                    int currentNum = FullMap[i][j];
                    Color currentColor = Color.White;

                    switch (currentNum)
                    {
                        case 0:
                            this.cropWall = new Rectangle(0, 0, 50, 50);
                            break;
                        case 1:
                            this.cropWall = new Rectangle(50, 0, 50, 50);
                            break;
                        case 2:
                            this.cropWall = new Rectangle(100, 0, 50, 50);
                            break;

                        default:
                            this.cropWall = new Rectangle(250, 0, 50, 50);
                            currentColor = Color.Black;
                            break;
                    }

                    this.currentCoord.X = (float) (j*50) + CharacterManager.barbarian.CoordP().X;
                    this.currentCoord.Y = (float) (i*50) + CharacterManager.barbarian.CoordP().Y;

                    spriteBatch.Draw(MapManager.Instance.Terrain, this.currentCoord, this.cropWall, currentColor);
                }
            }
        }

        public List<List<int>> FullMap
        {
            get
            {
                List<List<int>> copyOfMap = this.fullMap;
                return copyOfMap;
            }
            private set
            {
                this.fullMap = FileLoader.MapReader(mapPath);
            }
        }
        
    }
}