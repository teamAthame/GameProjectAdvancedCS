using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using AthameRPG.Characters.Heroes;

namespace AthameRPG.GameEngine
{
    public class CharacterManager
    {
        // SandWatch can change this value;
        public static bool itIsPlayerTurn = true;

        private const float PLAYER_START_POSITION_X = 400;
        private const float PLAYER_START_POSITION_Y = 300;
        private const string PathEnemyAndBuildingPositionOnMap = @"../../../../Content/Maps/01-enemy.txt";

        private static CharacterManager instance;
        
        private static Dictionary<int, Vector2> enemiesPositionList;

        protected bool oneTimeDraw;
        
        private Texture2D gargamelImage;

        public Vector2 StartPosition
        {
            get { return new Vector2(PLAYER_START_POSITION_X, PLAYER_START_POSITION_Y);}
        }
        
        public static Character barbarian;
        
        //public static
        public static List<Enemy> enemiesList;

        protected ContentManager content;

        public CharacterManager()
        {
            barbarian = new Barbarian(PLAYER_START_POSITION_X, PLAYER_START_POSITION_Y);
            enemiesPositionList = new Dictionary<int, Vector2>();
            enemiesList = new List<Enemy>();
            this.oneTimeDraw = true;
        }
        
        public static CharacterManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CharacterManager();
                }
                return instance;
            }
        }
        
        public ContentManager Content { get; private set; }

        public void LoadContent(ContentManager Content)
        {
            
            barbarian.LoadContent(Content);
            
            FileLoader.ReadEnemyAndBuildingPositions(PathEnemyAndBuildingPositionOnMap);

            int id = 0;
           
            foreach (var enemyPos in EnemiesPositionList)
            {
                Enemy newGargamel = new Gargamel(enemyPos.Value.X, enemyPos.Value.Y, id);
                newGargamel.LoadContent(Content);
                enemiesList.Add(newGargamel);
                id++;
            }
        }

        public void UnloadContent()
        {
            //barbarian.UnloadContent();

            //foreach (var garga in enemiesList)
            //{
            //    gargamel.UnloadContent();

            //}
        }

        public void Update(GameTime gameTime)
        {
            if (!Character.GetIsInBattle && !Character.GetIsInCastle)
            {

                if (itIsPlayerTurn)
                {
                    barbarian.Update(gameTime);
                    
                }

                bool support = true;
                
                foreach (var gargamelcho in enemiesList)
                {
                    gargamelcho.Update(gameTime);
                    
                    if (!itIsPlayerTurn && gargamelcho.ISeePlayer)
                    {
                        
                        support = false;
                    }
                }

                if (support && !itIsPlayerTurn)
                {
                    itIsPlayerTurn = true;
                }
                
            }
            else if (Character.GetIsInCastle)
            {

            }
            else if (Character.GetIsInBattle)
            {

            }

            
        }

        public void Draw(SpriteBatch spritebatch)
        {
            if (!Character.GetIsInBattle && !Character.GetIsInCastle)
            {
                barbarian.Draw(spritebatch);

                foreach (var gargamelcho in enemiesList)
                {
                    gargamelcho.Draw(spritebatch);
                }
            }
            else if (Character.GetIsInCastle)
            {
                
            }
            else if (Character.GetIsInBattle)
            {

            }
            
        }

        // this will be fix ... will be like barb... every char will give his image
        public Texture2D GargamelImage
        {
            get
            {
                return this.gargamelImage;
            }
        }

        public static Dictionary<int, Vector2> EnemiesPositionList
        {
            get
            {
                Dictionary<int,Vector2> copyOfEnemiesList = enemiesPositionList;
                return copyOfEnemiesList;
            }
            set
            {
                enemiesPositionList = value;
            }
        }

        public static void AddEnemies(KeyValuePair<int,Vector2> enemyPosition)
        {
            enemiesPositionList.Add(enemyPosition.Key, enemyPosition.Value);
        }

        public Texture2D PlayerImage
        {
            get
            {
                return Character.PlayerImage;
            }
        }
    }
}
