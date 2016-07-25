using System;
using AthameRPG.GameEngine.Loaders;
using AthameRPG.GameEngine.Managers;
using AthameRPG.Objects.Characters.Heroes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AthameRPG.Objects.Screens
{
    public class FinalScreen : GameScreen
    {
        private const string Credits =
            "Copyright: Team Athame\r\n\r\nSoftware Developers:\r\n\r\nAleksey Tsekov\r\nStanimir Todorov\r\nNikolay Genov\r\nIvo Lekov\r\nZdravko Stoynov";
        
        // congratulations you complete the game in xx turns;
        private const int SwitchCounter = 50;
        private const float DecreaseYStep = 2f;

        private string achievementText = $"Congratulations!\r\nYou complete the game\r\nin {MapManager.Instance.SandWatch.TurnCounter} turns.";

        private const float CreditStartX = 10f;
        private const float CreditStartY = 601f;
        private const float AchievementStartX = 480f;
        private const float AchievementStartY = 420f;
        private const float MaxYCoord = 6f;

        private int frameCounter;

        private bool canIDrawAchievement;

        private Vector2 DrawCoordCredits;
        private Vector2 DrawCoordAchievement;

        public FinalScreen()
        {
            this.DrawCoordCredits = new Vector2(CreditStartX, CreditStartY);
            this.DrawCoordAchievement = new Vector2(AchievementStartX, AchievementStartY);
        }

        public override void Update(GameTime gameTime)
        {
            this.frameCounter += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (this.frameCounter >= SwitchCounter)
            {
                this.frameCounter = 0;
                
                if (this.DrawCoordCredits.Y > MaxYCoord)
                {
                    this.DrawCoordCredits.Y -= DecreaseYStep;
                }
                else
                {
                    this.canIDrawAchievement = true;
                    this.RestartGame();
                }
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(FontLoader.MediumSizeLetter, Credits, this.DrawCoordCredits, Color.AntiqueWhite);

            if (this.canIDrawAchievement)
            {
                spriteBatch.DrawString(FontLoader.MediumSizeLetter, this.achievementText, this.DrawCoordAchievement, Color.Gray);
            }
        }

        private void RestartGame()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                MapManager.Instance.RestartGame();
                MapManager.Instance.SandWatch.Restart();
                MapManager.Instance.ChangeLevel.Restart();
                Character.GetIsInBattle = false;
                CharacterManager.EnemiesPositionList.Clear();
                CharacterManager.enemiesList.Clear();
                ScreenManager.Instance.UnloadContent();
                ScreenManager.Instance.ChangeScreens("MenuScreen");
                
                CharacterManager.barbarian.Restart();
                foreach (var enemy in CharacterManager.enemiesList)
                {
                    enemy.Restart();
                }
            }
        }
    }
}
