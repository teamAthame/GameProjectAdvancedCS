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
        
        private static CharacterManager instance;
        private static Dictionary<int, Vector2> enemiesPositionList;

        public static Character barbarian;
        public static List<Enemy> enemiesList;

        private ContentManager content;
        private bool oneTimeDraw;

        public CharacterManager()
        {
            barbarian = new Barbarian(PLAYER_START_POSITION_X, PLAYER_START_POSITION_Y);
            enemiesPositionList = new Dictionary<int, Vector2>();
            enemiesList = new List<Enemy>();
            this.oneTimeDraw = true;
        }

        public Vector2 StartPosition
        {
            get { return new Vector2(PLAYER_START_POSITION_X, PLAYER_START_POSITION_Y); }
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

        public void LoadContent(ContentManager content)
        {
            this.content = content;

            barbarian.LoadContent(content);

            this.ReadBuildingEnemyPositionAndCreateEnemies(content);
        }

        private void ReadBuildingEnemyPositionAndCreateEnemies(ContentManager content)
        {
            FileLoader.ReadEnemyAndBuildingPositions(MapManager.Instance.GetEnemyBuildingFilePath);

            int setEnemyid = 0;

            foreach (var enemyPos in EnemiesPositionList)
            {
                Enemy enemy = new Gargamel(enemyPos.Value.X, enemyPos.Value.Y, setEnemyid);
                enemy.LoadContent(content);
                enemiesList.Add(enemy);
                setEnemyid++;
            }
        }

        public void PrepareUnitsForNextLevel()
        {
            this.ReadBuildingEnemyPositionAndCreateEnemies(this.content);
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
            this.CheckForMapAccomplished();

            if (!Character.GetIsInBattle && !Character.GetIsInCastle)
            {

                if (itIsPlayerTurn)
                {
                    barbarian.Update(gameTime);

                }

                bool support = true;

                foreach (var enemy in enemiesList)
                {
                    enemy.Update(gameTime);

                    if (!itIsPlayerTurn && enemy.ISeePlayer)
                    {

                        support = false;
                    }
                }

                if (support && !itIsPlayerTurn)
                {
                    itIsPlayerTurn = true;
                }

            }
        }

        private void CheckForMapAccomplished()
        {
            if (enemiesList.Count == 0)
            {
                //Environment.Exit(0);
                MapManager.Instance.TryChangeLevel(barbarian);
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
        
        public static Dictionary<int, Vector2> EnemiesPositionList
        {
            get
            {
                Dictionary<int, Vector2> copyOfEnemiesList = enemiesPositionList;
                return copyOfEnemiesList;
            }
            set { enemiesPositionList = value; }
        }

        public static void AddEnemies(KeyValuePair<int, Vector2> enemyPosition)
        {
            enemiesPositionList.Add(enemyPosition.Key, enemyPosition.Value);
        }

        public Texture2D PlayerImage
        {
            get { return Character.PlayerImage; }
        }
    }
}
