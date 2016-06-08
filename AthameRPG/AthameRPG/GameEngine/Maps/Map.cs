using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using AthameRPG.Characters;

namespace AthameRPG.GameEngine.Maps
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
        private void AddObstacle(Vector2 coordObstacle)
        {
            obstacles.Add(coordObstacle); 
        }

        public void LoadContent()
        {
            obstacles.Clear();
            FillListOfObstacles();
        }

        private void FillListOfObstacles()
        {
            for (int i = 0; i < FullMap.Count(); i++)
            {
                for (int j = 0; j < FullMap[i].Count(); j++)
                {

                    int currentNum = FullMap[i][j];
                    /// elements with those NUMBERS are obstacles. /// this is example !!!
                    /// 
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
            
            for (int i = 0; i < FullMap.Count(); i++)
            {
                for (int j = 0; j < FullMap[i].Count(); j++)
                {
                    int currentNum = FullMap[i][j];
                    
                    switch (currentNum)
                    {
                        case 0: cropWall = new Rectangle(0, 0, 50, 50);
                            break;
                        case 1:
                            cropWall = new Rectangle(50, 0, 50, 50);
                            break;
                        case 2:
                            cropWall = new Rectangle(100, 0, 50, 50);
                            break;
                        
                    }
                    
                    currentCoord.X = (float)(j * 50) + CharacterManager.barbarian.CoordP().X;
                    currentCoord.Y = (float)(i * 50) + CharacterManager.barbarian.CoordP().Y;
                    
                    spriteBatch.Begin();
                    spriteBatch.Draw(MapManager.Instance.Terrain, currentCoord, cropWall, Color.White);
                    spriteBatch.End();
                }
            }
        }

        public List<List<int>> FullMap
        {
            get
            {
                List<List<int>> copyOfMap = fullMap;
                return copyOfMap;
            }
            private set
            {
                fullMap = FileLoader.MapReader(mapPath);
            }
        }

        
    }
}
