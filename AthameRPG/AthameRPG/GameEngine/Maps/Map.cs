using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AthameRPG.GameEngine.Maps
{
    public class Map 
    {
        private string mapPath;
        //private Texture2D terrain;
        private List<List<int>> fullMap;
        public static List<Map> obstacle;
        private Rectangle cropWall;
        private Vector2 coordinates;

        public Map(string mapPath)
        {
            this.MapPath = mapPath;
            this.FullMap = new List<List<int>>();
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

        public void LoadContent()
        {
            
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
                        
                    }
                    Vector2 currentCoor = new Vector2((float)(j * 50), (float)(i * 50));
                    spriteBatch.Draw(MapManager.Instance.Terrain, currentCoor, cropWall, Color.White);
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
