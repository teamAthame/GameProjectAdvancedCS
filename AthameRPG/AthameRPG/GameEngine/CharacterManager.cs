using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using AthameRPG.Characters;
using AthameRPG.GameEngine.Maps;

namespace AthameRPG.GameEngine
{
    public class CharacterManager
    {
        private const float PLAYER_START_POSITION_X = 400;
        private const float PLAYER_START_POSITION_Y = 300;
        private const string PATH_ENEMY_POSITION_ON_MAP = @"../../../../Content/Maps/01-enemy.txt";

        private static CharacterManager instance;
        private static List<Vector2> enemiesPositionList;
        
        // Maybe these will be in Dictionary and will be read from txt fail !
        private Texture2D playerImage;
        private const string PATH_BARBARIAN_IMAGE = @"../Content/Character/superman";
        private Texture2D gargamelImage;


        private const string PATH_GARGAMEL_IMAGE = @"../Content/Character/GoblinWalk";
        //private const string PATH_GARGAMEL_IMAGE = @"../Content/Fonts/terrain";

        public static Barbarian barbarian;

        /// <summary>
        ///  return again private only for test and ENEMY
        /// </summary>
        public static List<Gargamel> enemiesList;

        protected ContentManager content;

        public CharacterManager()
        {
            barbarian = new Barbarian(PLAYER_START_POSITION_X, PLAYER_START_POSITION_Y);
            enemiesPositionList = new List<Vector2>();
            enemiesList = new List<Gargamel>();
        }
        
        public static List<Vector2> EnemiesPositionList
        {
            get
            {
                List<Vector2> copyOfEnemiesList = enemiesPositionList;
                return copyOfEnemiesList; 
            }
            set
            {
                enemiesPositionList = value;
            }
        }

        public static void AddEnemies(Vector2 enemyPosition)
        {
            enemiesPositionList.Add(enemyPosition);
        }

        public Texture2D PlayerImage
        {
            get
            {
                return this.playerImage;
            }
        }

        public Texture2D GargamelImage
        {
            get
            {
                return this.gargamelImage;
            }
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
            
            Content = new ContentManager(Content.ServiceProvider, "Content");
            playerImage = Content.Load<Texture2D>(PATH_BARBARIAN_IMAGE);
            barbarian.LoadContent();

            gargamelImage = Content.Load<Texture2D>(PATH_GARGAMEL_IMAGE);

            FileLoader.ReadEnemyPosition(PATH_ENEMY_POSITION_ON_MAP);

            int id = 0;
           
            foreach (var enemyPos in EnemiesPositionList)
            {
                Gargamel newGargamel = new Gargamel(enemyPos.X, enemyPos.Y, id);
                newGargamel.LoadContent();
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
            barbarian.Update(gameTime);

            foreach (var gargamelcho in enemiesList)
            {
                gargamelcho.Update(gameTime);
            }
        }
        public void Draw(SpriteBatch spritebatch)
        {
            barbarian.Draw(spritebatch);

            foreach (var gargamelcho in enemiesList)
            {
                gargamelcho.Draw(spritebatch);
            }
        }
    }
}
