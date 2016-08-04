namespace AthameRPG.Objects.Maps
{
    using System.Collections.Generic;
    using System.Linq;
    using AthameRPG.GameEngine.Loaders;
    using AthameRPG.GameEngine.Managers;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Map
    {
        private const int SquareCropSize = 50;

        private readonly Rectangle earthImageCrop = new Rectangle(0, 0, 50, 50);
        private readonly Rectangle grassImageCrop = new Rectangle(50, 0, 50, 50);
        private readonly Rectangle darkStoneImageCrop = new Rectangle(100, 0, 50, 50);

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
                            AddObstacle(new Vector2((float)(j * SquareCropSize), (float)(i * SquareCropSize)));
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
                        case 0://earth
                            this.cropWall = this.earthImageCrop;
                            break;
                        case 1://grass
                            this.cropWall = this.grassImageCrop;
                            break;
                        case 2://dark stone
                            this.cropWall = this.darkStoneImageCrop;
                            break;

                        default:
                            this.cropWall = new Rectangle(250, 0, 50, 50);
                            currentColor = Color.Black;
                            break;
                    }

                    this.currentCoord.X = (float) (j* SquareCropSize) + CharacterManager.barbarian.CoordP().X;
                    this.currentCoord.Y = (float) (i* SquareCropSize) + CharacterManager.barbarian.CoordP().Y;

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
