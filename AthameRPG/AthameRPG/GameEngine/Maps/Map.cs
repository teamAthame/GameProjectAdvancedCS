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
                        case 10:
                        case 11:
                        case 12:
                        case 13:
                        case 14:
                        case 15:
                        case 16:
                        case 17:
                        case 18:
                        case 19:
                        case 20:
                        case 21:
                        case 22:
                        case 23:
                        case 24:
                        case 25:
                        case 26:
                        case 27:
                        case 28:
                        case 29:

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
                    Color currentColor = Color.White;
                    
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
                        case 10:
                            cropWall = new Rectangle(0, 50, 50, 50);
                            break;
                        case 11:
                            cropWall = new Rectangle(50, 50, 50, 50);
                            break;
                        case 12:
                            cropWall = new Rectangle(100, 50, 50, 50);
                            break;
                        case 13:
                            cropWall = new Rectangle(150, 50, 50, 50);
                            break;
                        case 14:
                            cropWall = new Rectangle(200, 50, 50, 50);
                            break;
                        case 15:
                            cropWall = new Rectangle(0, 100, 50, 50);
                            break;
                        case 16:
                            cropWall = new Rectangle(50, 100, 50, 50);
                            break;
                        case 17:
                            cropWall = new Rectangle(100, 100, 50, 50);
                            break;
                        case 18:
                            cropWall = new Rectangle(150, 100, 50, 50);
                            break;
                        case 19:
                            cropWall = new Rectangle(200, 100, 50, 50);
                            break;
                        case 20:
                            cropWall = new Rectangle(0, 150, 50, 50);
                            break;
                        case 21:
                            cropWall = new Rectangle(50, 150, 50, 50);
                            break;
                        case 22:
                            cropWall = new Rectangle(100, 150, 50, 50);
                            break;
                        case 23:
                            cropWall = new Rectangle(150, 150, 50, 50);
                            break;
                        case 24:
                            cropWall = new Rectangle(200, 150, 50, 50);
                            break;
                        case 25:
                            cropWall = new Rectangle(0, 200, 50, 50);
                            break;
                        case 26:
                            cropWall = new Rectangle(50, 200, 50, 50);
                            break;
                        case 27:
                            cropWall = new Rectangle(100, 200, 50, 50);
                            break;
                        case 28:
                            cropWall = new Rectangle(150, 200, 50, 50);
                            break;
                        case 29:
                            cropWall = new Rectangle(200, 200, 50, 50);
                            break;

                        default:
                            cropWall = new Rectangle(250, 0, 50, 50);
                            currentColor = Color.Black;
                            break;

                    }
                    
                    currentCoord.X = (float)(j * 50) + CharacterManager.barbarian.CoordP().X;
                    currentCoord.Y = (float)(i * 50) + CharacterManager.barbarian.CoordP().Y;
                    
                    spriteBatch.Draw(MapManager.Instance.Terrain, currentCoord, cropWall, currentColor);
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
