using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using AthameRPG.Characters;

namespace AthameRPG.GameEngine
{
    public class CharacterManager
    {
        private const float PLAYER_START_POSITION_X = 400;
        private const float PLAYER_START_POSITION_Y = 305;

        private static CharacterManager instance;
        private Texture2D playerImage;
        private Vector2 playerCoordinates;


        /// <summary>
        /// all other good classess like : Wizard wizard ; Witch witch ....
        /// </summary>
        /// 
        private Barbarian barbarian;

        protected ContentManager content;

        public CharacterManager()
        {
            barbarian = new Barbarian(PLAYER_START_POSITION_X, PLAYER_START_POSITION_Y);
        }

        public Texture2D PlayerImage
        {
            get
            {
                return this.playerImage;
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
            //content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");
            //this.ContentManager = new ContentManager(ContentManager.ServiceProvider, "Content");
            Content = new ContentManager(Content.ServiceProvider, "Content");
            playerImage = Content.Load<Texture2D>("../Content/Character/superman");
            barbarian.LoadContent();
        }

        public void UnloadContent()
        {

        }

        public void Update(GameTime gameTime)
        {
            barbarian.Update(gameTime);
        }
        public void Draw(SpriteBatch spritebatch)
        {
            barbarian.Draw(spritebatch);
        }
    }
}
