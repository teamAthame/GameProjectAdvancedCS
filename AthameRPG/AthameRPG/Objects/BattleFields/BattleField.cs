using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using AthameRPG.Characters;
using AthameRPG.Characters.Heroes;
using AthameRPG.Characters.WarUnits;
using AthameRPG.Controls;
using AthameRPG.GameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AthameRPG.Objects.BattleFields
{
    public class Battlefield
    {
        // instance/loaded from MapManager

        private const string DefaultImagePath = "../Content/Obstacles/stonebattlefield";
        private const string SmallLettersPath = "../Content/Fonts/SmallLetters";
        private const string WinText = "You Won the battle!";
        private const string LoseText = "Game Over!";
        private const string SupportText = "Press ENTER to continue.";
        private const int WinTextX = 220;
        private const int WinTextY = 250;
        private const int LoseTextX = 220;
        private const int LoseTextY = 250;
        private const int SupportTextX = 220;
        private const int SupportTextY = 300;

        // Enemy change this value when set ItIsBattle....
        public static int unitStreghtLevelIndex = 7;

        private Texture2D battlefieldImage;
        private string imagePath;
        private Vector2 battlefieldDrawCoord;
        private SpriteFont spriteFontSmallLetters;
        private SpriteFont bigFont;
        private Vector2 winTextCoord;
        private Vector2 loseTextCoord;
        private Vector2 supportTextCoord;

        private Dictionary<WarUnit, decimal> playerUnits;
        private Queue<KeyValuePair<WarUnit, decimal>> supportRemoveKilledUnitsFromPlayerArmy;
        private Queue<KeyValuePair<WarUnit, decimal>> supportRemoveKilledUnitsFromEnemyArmy;
        private Dictionary<WarUnit, decimal> enemyUnits;

        protected MouseState newMouseState;
        protected MouseState oldMouseState;

        private bool playerTurn;
        private bool isBattle;
        private bool amIWinner;
        private bool oneTimeSwitch;
        private int enemyId;

        private WarUnit supportUnit;


        public Battlefield()
        {
            this.imagePath = DefaultImagePath;
            this.playerUnits = new Dictionary<WarUnit, decimal>();
            this.enemyUnits = new Dictionary<WarUnit, decimal>();
            this.supportRemoveKilledUnitsFromPlayerArmy = new Queue<KeyValuePair<WarUnit, decimal>>();
            this.supportRemoveKilledUnitsFromEnemyArmy = new Queue<KeyValuePair<WarUnit, decimal>>();
            this.winTextCoord = new Vector2(WinTextX, WinTextY);
            this.loseTextCoord = new Vector2(LoseTextX,LoseTextY);
            this.supportTextCoord = new Vector2(SupportTextX,SupportTextY);

        }

        public void LoadContent(ContentManager contentManager)
        {
            this.battlefieldImage = contentManager.Load<Texture2D>(this.imagePath);
            this.spriteFontSmallLetters = contentManager.Load<SpriteFont>(SmallLettersPath);
            this.bigFont = contentManager.Load<SpriteFont>("../Content/Fonts/ArialBig");

            this.winTextCoord = new Vector2(ScreenManager.SCREEN_WIDTH/2 - this.bigFont.MeasureString(WinText).X/2,
                WinTextY);
            this.loseTextCoord = new Vector2(
                ScreenManager.SCREEN_WIDTH/2 - this.bigFont.MeasureString(LoseText).X/2, LoseTextY);
            this.supportTextCoord = 
                new Vector2(ScreenManager.SCREEN_WIDTH/2 - this.bigFont.MeasureString(SupportText).X/2, SupportTextY);

        }

        public void Update(GameTime gameTime)
        {
            //MouseExtended.Current.GetState(gameTime);


            if (!this.isBattle)
            {
                SwitchToMenuOrReturnInGame();
                return;
            }

            this.CheckForBattleEnd();

            this.TrySwitchTurn();
            this.TryRemoveDeathUnits();

            foreach (var playerUnit in this.playerUnits)
            {
                if ((playerUnit.Key.GetStrengthLevel == unitStreghtLevelIndex) && this.playerTurn &&
                    playerUnit.Key.inBattleTurn == true)
                {
                    playerUnit.Key.CanBeSeleted = true;
                }
                else
                {
                    playerUnit.Key.CanBeSeleted = false;
                }

                // ако нямаме гадини от текущия левел
                if (playerTurn)
                {

                }
                playerUnit.Key.Update(gameTime);

            }

            this.TryRemovedKilledUnits();
            this.TryRemoveDeathUnits();

            foreach (var enemyUnit in this.enemyUnits)
            {
                enemyUnit.Key.Update(gameTime);

                if (!this.playerTurn && enemyUnit.Key.GetStrengthLevel == unitStreghtLevelIndex)
                {
                    enemyUnit.Key.MoveInBattle();
                }
            }

            this.TryRemovedKilledUnits();

            this.CheckEnemyUnitsForInBattleTurn();
        }

        private void SwitchToMenuOrReturnInGame()
        {
            this.oldMouseState = this.newMouseState;
            this.newMouseState = Mouse.GetState();

            if (this.amIWinner)
            {
                if (this.newMouseState.LeftButton == ButtonState.Pressed &&
                    this.oldMouseState.LeftButton == ButtonState.Released)
                {
                    this.oneTimeSwitch = true;
                }
                if (this.oneTimeSwitch)
                {
                    Enemy unit = CharacterManager.enemiesList.FirstOrDefault(x => x.ID == this.enemyId);

                    //Vector2 vector = CharacterManager.EnemiesPositionList.FirstOrDefault(x => x == unit.DrawCoordEnemy);
                    //CharacterManager.EnemiesPositionList.Remove(vector);
                    CharacterManager.EnemiesPositionList.Remove(this.enemyId);
                    CharacterManager.barbarian.AvailableCreatures = this.playerUnits;

                    CharacterManager.enemiesList.Remove(unit);
                    Character.GetIsInBattle = false;
                }
            }
            else
            {
                //Keyboard.GetState().IsKeyDown(Keys.Enter)
                // this.newMouseState.LeftButton == ButtonState.Pressed && this.oldMouseState.LeftButton == ButtonState.Released

                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
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

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.battlefieldImage, this.battlefieldDrawCoord, Color.White);

            if (this.isBattle)
            {
                foreach (var playerUnit in this.playerUnits)
                {
                    playerUnit.Key.Draw(spriteBatch);
                    spriteBatch.DrawString(this.spriteFontSmallLetters, playerUnit.Value.ToString(),
                        playerUnit.Key.WarUnitDrawCoord, Color.Red);


                }
                foreach (var enemyUnit in this.enemyUnits)
                {
                    enemyUnit.Key.Draw(spriteBatch);
                    spriteBatch.DrawString(this.spriteFontSmallLetters, enemyUnit.Value.ToString(),
                        new Vector2(enemyUnit.Key.WarUnitDrawCoord.X, enemyUnit.Key.WarUnitDrawCoord.Y), Color.Red);
                }
            }
            else
            {
                if (this.amIWinner)
                {
                    //this.textCoord = new Vector2(this.bigFont.MeasureString(WinText).X / 2, this.bigFont.MeasureString(WinText).Y / 2);
                    spriteBatch.DrawString(this.bigFont, WinText, this.winTextCoord, Color.Red);

                    //Character.GetIsInBattle = false;
                }
                else
                {
                    //this.textCoord = new Vector2(this.bigFont.MeasureString(LoseText).X/2, this.bigFont.MeasureString(LoseText).Y/2);
                    spriteBatch.DrawString(this.bigFont, LoseText, this.loseTextCoord, Color.Red);
                    spriteBatch.DrawString(this.bigFont, SupportText, this.supportTextCoord, Color.Blue);
                }
            }


        }

        private void CheckForBattleEnd()
        {
            this.isBattle = this.playerUnits.Count > 0 && this.enemyUnits.Count > 0;

            if (!this.isBattle)
            {
                this.amIWinner = this.playerUnits.Count > 0;
            }
        }

        private void TryRemovedKilledUnits()
        {
            while (this.supportRemoveKilledUnitsFromPlayerArmy.Count > 0)
            {
                KeyValuePair<WarUnit, decimal> currentUnit = this.supportRemoveKilledUnitsFromPlayerArmy.Dequeue();
                this.playerUnits[currentUnit.Key] -= currentUnit.Value;
            }
            while (this.supportRemoveKilledUnitsFromEnemyArmy.Count > 0)
            {
                KeyValuePair<WarUnit, decimal> currentUnit = this.supportRemoveKilledUnitsFromEnemyArmy.Dequeue();
                this.enemyUnits[currentUnit.Key] -= currentUnit.Value;
            }
        }

        private void TryRemoveDeathUnits()
        {
            Queue<WarUnit> deathUnit = new Queue<WarUnit>();

            foreach (var unit in this.playerUnits)
            {
                if (unit.Value < 1)
                {
                    deathUnit.Enqueue(unit.Key);
                }
            }

            while (deathUnit.Count > 0)
            {
                this.playerUnits.Remove(deathUnit.Dequeue());
            }

            foreach (var unit in this.enemyUnits)
            {
                if (unit.Value < 1)
                {
                    deathUnit.Enqueue(unit.Key);
                }
            }

            while (deathUnit.Count > 0)
            {
                this.enemyUnits.Remove(deathUnit.Dequeue());
            }


        }

        public void LoadArmies(IReadOnlyDictionary<WarUnit, decimal> playerArmy,
            IReadOnlyDictionary<WarUnit, decimal> enemyArmy, int enemyID)
        {
            // взимаме ИД-то за да знаем кое енеми да премахнем. ако победим :) 
            this.enemyId = enemyID;

            //трябва ни за края на рунда 
            this.oneTimeSwitch = false;
            this.isBattle = true;


            this.playerUnits = new Dictionary<WarUnit, decimal>();
            this.enemyUnits = new Dictionary<WarUnit, decimal>();

            this.playerUnits = (Dictionary<WarUnit, decimal>) playerArmy;
            this.enemyUnits = (Dictionary<WarUnit, decimal>) enemyArmy;

            this.ResetDrawPosition();

            this.playerTurn = true;
            this.PrepareUnitsForTurn();
        }

        public void EndCurrentTurn()
        {
            this.supportUnit = this.playerUnits.FirstOrDefault(x => x.Key.isChoosen == true).Key;

            if (this.supportUnit != null)
            {
                this.supportUnit.inBattleTurn = false;
            }
            else
            {
                this.supportUnit =
                    this.playerUnits.FirstOrDefault(
                        x => x.Key.GetStrengthLevel == unitStreghtLevelIndex && x.Key.inBattleTurn == true).Key;
                if (this.supportUnit != null)
                {
                    this.supportUnit.inBattleTurn = false;
                }
            }

            this.TrySwitchTurn();
        }

        public void TrySwitchTurn()
        {
            foreach (var unit in this.playerUnits)
            {
                if (unit.Key.inBattleTurn && unitStreghtLevelIndex == unit.Key.GetStrengthLevel)
                {
                    return;
                }
            }

            this.playerTurn = false;
        }

        public bool CheckEnemyArmy(Predicate<WarUnit> condition)
        {
            foreach (var enemyUnit in this.enemyUnits)
            {
                if (condition(enemyUnit.Key))
                {
                    return true;
                }
            }
            return false;
        }

        public bool CheckPlayerArmy(Predicate<WarUnit> condition)
        {
            foreach (var unit in this.playerUnits)
            {
                if (condition(unit.Key))
                {
                    return true;
                }
            }
            return false;
        }

        private void CheckEnemyUnitsForInBattleTurn()
        {
            foreach (var unit in this.enemyUnits)
            {
                if (unit.Key.inBattleTurn && unit.Key.GetStrengthLevel == unitStreghtLevelIndex)
                {
                    return;
                }
            }

            this.playerTurn = true;

            this.PrepareUnitsForTurn();

            unitStreghtLevelIndex--;

            if (unitStreghtLevelIndex < 0)
            {
                unitStreghtLevelIndex = 7;
            }
        }


        private void ResetDrawPosition()
        {
            foreach (var unit in this.playerUnits)
            {
                unit.Key.SetStartPositionInBattleLikePlayer();
            }
            foreach (var unit in this.enemyUnits)
            {
                unit.Key.SetStartPositionInBattleLikeEnemy();
            }

        }

        private void PrepareUnitsForTurn()
        {
            foreach (var unit in this.playerUnits)
            {
                unit.Key.inBattleTurn = true;
                unit.Key.ReFillAvailableMove();
            }
            foreach (var unit in this.enemyUnits)
            {
                unit.Key.inBattleTurn = true;
                unit.Key.ReFillAvailableMove();
                unit.Key.HaveActionForCurrentTurn = true;
            }
        }

        public WarUnit TryTakeEnemyUnit(Predicate<WarUnit> condition)
        {
            foreach (var unit in this.playerUnits)
            {
                if (condition(unit.Key))
                {
                    return unit.Key;
                }
            }

            return null;
        }

        public WarUnit TryTakeFriendUnit(Predicate<WarUnit> condition)
        {
            foreach (var unit in this.enemyUnits)
            {
                if (condition(unit.Key))
                {
                    return unit.Key;
                }
            }

            return null;
        }

        public decimal TryTakeFriendUnitQuantity(WarUnit unit)
        {
            foreach (var enemyUnit in this.enemyUnits)
            {
                if (enemyUnit.Key == unit)
                {
                    return enemyUnit.Value;
                }
            }
            return 0m;
        }

        public decimal TryTakeEnemmyUnitQuantity(WarUnit unit)
        {
            foreach (var playerUnit in this.playerUnits)
            {
                if (playerUnit.Key == unit)
                {
                    return playerUnit.Value;
                }
            }
            return 0;
        }

        public void AttackPlayerUnit(WarUnit defender, int damage, WarUnit attacker, int counterDamage)
        {
            int unitsToRemoveFromDefender = damage/defender.GetDefaultHeаlth();
            int remainingDamageToDefender = damage - (defender.GetDefaultHeаlth()*unitsToRemoveFromDefender);

            unitsToRemoveFromDefender += defender.DecreaseHealth(remainingDamageToDefender);
            this.supportRemoveKilledUnitsFromPlayerArmy.Enqueue(new KeyValuePair<WarUnit, decimal>(defender,
                unitsToRemoveFromDefender));

            // counter-attack

            int unitsToRemoveFromAttacker = counterDamage/attacker.GetDefaultHeаlth();
            int remainingDamageToAttacker = counterDamage - (unitsToRemoveFromAttacker*attacker.GetDefaultHeаlth());

            unitsToRemoveFromAttacker += attacker.DecreaseHealth(remainingDamageToAttacker);
            this.supportRemoveKilledUnitsFromEnemyArmy.Enqueue(new KeyValuePair<WarUnit, decimal>(attacker,
                unitsToRemoveFromAttacker));
            //attacker.DecreaseHealth(remainingDamageToAttacker);

        }

        public void AttackEnemyUnit(WarUnit unit, decimal damage)
        {

        }

    }
}
