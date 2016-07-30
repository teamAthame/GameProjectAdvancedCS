using System;
using System.Collections.Generic;
using System.Linq;
using AthameRPG.GameEngine;
using AthameRPG.GameEngine.Collisions;
using AthameRPG.GameEngine.Loaders;
using AthameRPG.GameEngine.Managers;
using AthameRPG.Objects.Characters.Heroes;
using AthameRPG.Objects.Characters.WarUnits;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using AthameRPG.Objects.Weapons;

namespace AthameRPG.Objects.BattleFields
{
    public class Battlefield
    {
        // instance/loaded from MapManager

        private const string DefaultImagePath = "../Content/Obstacles/stonebattlefield";
        private const string WinText = "You Won the battle!";
        private const string LoseText = "Game Over!";
        private const string SupportText = "Press ENTER to continue.";
        private readonly string[] InfoText = new []
        {
            "If creature is flashing in red, you can select it by RIGHT mouse click.",
            "You can move selected creature by double click.",
            "Your selected creature can attack enemy one if enemy creature is in attack radius."
        };
        private const float DrawInfoX = 100;
        private const float DrawInfoY = 0;
        private const float DrawInfoStepY = 25;
        private const int WinTextX = 220;
        private const int WinTextY = 250;
        private const int LoseTextX = 220;
        private const int LoseTextY = 250;
        private const int SupportTextX = 220;
        private const int SupportTextY = 300;

        // class Enemy change this value when set ItIsBattle....
        public static int unitStreghtLevelIndex = 7;

        private Texture2D battlefieldImage;
        private string imagePath;
        private Vector2 battlefieldDrawCoord;
        private Vector2 winTextCoord;
        private Vector2 loseTextCoord;
        private Vector2 supportTextCoord;
        private Vector2 drawInfo;

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
            
            this.winTextCoord = new Vector2(ScreenManager.SCREEN_WIDTH/2 - FontLoader.BigSizeFont.MeasureString(WinText).X/2,
                WinTextY);
            this.loseTextCoord = new Vector2(
                ScreenManager.SCREEN_WIDTH/2 - FontLoader.BigSizeFont.MeasureString(LoseText).X/2, LoseTextY);
            this.supportTextCoord = 
                new Vector2(ScreenManager.SCREEN_WIDTH/2 - FontLoader.BigSizeFont.MeasureString(SupportText).X/2, SupportTextY);

        }

        public void Update(GameTime gameTime)
        {
            //MouseExtended.Current.GetState(gameTime);
            
            if (!this.isBattle)
            {
                this.SwitchToMenuOrReturnInGame();
                return;
            }

            this.CheckForBattleEnd();

            if (!this.CheckForAttackAnimation())
            {
                this.TrySwitchTurn();
                this.TryRemoveDeathUnits();
            }
            
            foreach (var playerUnit in this.playerUnits)
            {
                if ((playerUnit.Key.GetStrengthLevel == unitStreghtLevelIndex) 
                    && this.playerTurn 
                    && playerUnit.Key.inBattleTurn == true
                    && this.CanShoot())
                {
                    playerUnit.Key.CanBeSeleted = true;
                }
                else
                {
                    playerUnit.Key.CanBeSeleted = false;
                }

                
                playerUnit.Key.Update(gameTime);

            }

            if (!this.CheckForAttackAnimation())
            {
                this.TrySwitchTurn();
                this.TryRemoveDeathUnits();
            }

            foreach (var enemyUnit in this.enemyUnits)
            {

                if (!this.playerTurn && enemyUnit.Key.GetStrengthLevel == unitStreghtLevelIndex)
                {
                    enemyUnit.Key.MoveInBattle();
                }
                enemyUnit.Key.Update(gameTime);
            }

            this.TryRemovedKilledUnits();
            if (!this.playerTurn)
            {
                this.CheckEnemyUnitsForInBattleTurn();
            }
           
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.battlefieldImage, this.battlefieldDrawCoord, Color.White);
            
            this.PrintInfoText(spriteBatch);

            if (this.isBattle)
            {
                this.DrawUnitsInBattleAndTheirQuantity(spriteBatch);
            }
            else
            {
                if (this.amIWinner)
                {
                    spriteBatch.DrawString(FontLoader.BigSizeFont, WinText, this.winTextCoord, Color.Red);

                    //Character.GetIsInBattle = false;
                }
                else
                {
                    spriteBatch.DrawString(FontLoader.BigSizeFont, LoseText, this.loseTextCoord, Color.Red);
                    spriteBatch.DrawString(FontLoader.BigSizeFont, SupportText, this.supportTextCoord, Color.Blue);
                }
            }


        }

        private void DrawUnitsInBattleAndTheirQuantity(SpriteBatch spriteBatch)
        {
            foreach (var playerUnit in this.playerUnits)
            {
                playerUnit.Key.Draw(spriteBatch);
                spriteBatch.DrawString(FontLoader.SmallSizeFont, playerUnit.Value.ToString(),
                    playerUnit.Key.WarUnitDrawCoord, Color.Red);
            }
            foreach (var enemyUnit in this.enemyUnits)
            {
                enemyUnit.Key.Draw(spriteBatch);
                spriteBatch.DrawString(FontLoader.SmallSizeFont, enemyUnit.Value.ToString(),
                    new Vector2(enemyUnit.Key.WarUnitDrawCoord.X, enemyUnit.Key.WarUnitDrawCoord.Y), Color.Red);
            }
        }

        private void PrintInfoText(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < 3; i++)
            {
                this.drawInfo = new Vector2(DrawInfoX, DrawInfoY + (i * DrawInfoStepY));

                spriteBatch.DrawString(FontLoader.SmallSizeFont, InfoText[i], this.drawInfo, Color.White);
            }
            
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

                    this.UnsubscribeFromSoundEvent(unit);

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

                this.RestartGame();
            }
        }

        private void UnsubscribeFromSoundEvent(Unit unit)
        {
            unit.OnEvent -= ScreenManager.Instance.SoundEffectManager.ExecuteQuery;
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
                this.UnsubscribeFromSoundEvent(CharacterManager.barbarian);
                foreach (var enemy in CharacterManager.enemiesList)
                {
                    enemy.Restart();
                }
            }
        }

        private void CheckForBattleEnd()
        {
            bool hasAttackAnimation = this.CheckForAttackAnimation();

            if (!hasAttackAnimation)
            {
                this.isBattle = this.playerUnits.Count > 0 && this.enemyUnits.Count > 0;

                if (!this.isBattle)
                {
                    this.amIWinner = this.playerUnits.Count > 0;
                }
            }
            
        }

        private bool CheckForAttackAnimation()
        {
            foreach (var unit in this.playerUnits.Keys)
            {
                if (unit.AmIAttacking)
                {
                    return true;
                }
            }

            foreach (var unit in this.enemyUnits.Keys)
            {
                if (unit.AmIAttacking)
                {
                    return true;
                }
            }

            return false;
        }

        private void TryRemovedKilledUnits()
        {
            while (this.supportRemoveKilledUnitsFromPlayerArmy.Count > 0)
            {
                KeyValuePair<WarUnit, decimal> currentUnit = this.supportRemoveKilledUnitsFromPlayerArmy.Dequeue();
                this.playerUnits[currentUnit.Key] -= currentUnit.Value;
                if (this.playerUnits[currentUnit.Key] < 0)
                {
                    this.playerUnits[currentUnit.Key] = 0;
                }
            }
            while (this.supportRemoveKilledUnitsFromEnemyArmy.Count > 0)
            {
                KeyValuePair<WarUnit, decimal> currentUnit = this.supportRemoveKilledUnitsFromEnemyArmy.Dequeue();
                this.enemyUnits[currentUnit.Key] -= currentUnit.Value;
                if (this.enemyUnits[currentUnit.Key] < 0)
                {
                    this.enemyUnits[currentUnit.Key] = 0;
                }
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

        public void LoadArmies(Dictionary<WarUnit, decimal> playerArmy,
            Dictionary<WarUnit, decimal> enemyArmy, int enemyID)
        {
            // we take enemy ID because if we win to know whick enemy to remove from the active enemies.
            this.enemyId = enemyID;

            // need in the end of round.
            this.oneTimeSwitch = false;

            this.isBattle = true;
            
            this.playerUnits = new Dictionary<WarUnit, decimal>();
            this.enemyUnits = new Dictionary<WarUnit, decimal>();

            this.playerUnits =  playerArmy;
            this.enemyUnits = enemyArmy;

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

        public WarUnit TryTakeNearestPlayerUnit(WarUnit enemyUnit, int radius )
        {
            this.supportUnit = null;

            double minDistance = double.MaxValue;
            
            foreach (var unit in this.playerUnits)
            {
                double currentDistance = CollisionDetection.CalculateDistanceTravelled(unit.Key.WarUnitDrawCoord,
                    enemyUnit.WarUnitDrawCoord);

                if ((currentDistance < minDistance) && ((int)currentDistance < radius))
                {
                    this.supportUnit = unit.Key;

                    minDistance = currentDistance;
                }
            }

            return this.supportUnit;
        }

        public WarUnit TryTakeNearestEnemyUnit(WarUnit enemyUnit, int radius)
        {
            this.supportUnit = null;

            double minDistance = double.MaxValue;

            foreach (var unit in this.enemyUnits)
            {
                double currentDistance = CollisionDetection.CalculateDistanceTravelled(unit.Key.WarUnitDrawCoord,
                    enemyUnit.WarUnitDrawCoord);

                if ((currentDistance < minDistance) && ((int)currentDistance < radius))
                {
                    this.supportUnit = unit.Key;

                    minDistance = currentDistance;
                }
            }

            return this.supportUnit;
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
            if (!attacker.AmIArcherOrMage)
            {
                int unitsToRemoveFromAttacker = counterDamage / attacker.GetDefaultHeаlth();
                int remainingDamageToAttacker = counterDamage - (unitsToRemoveFromAttacker * attacker.GetDefaultHeаlth());

                unitsToRemoveFromAttacker += attacker.DecreaseHealth(remainingDamageToAttacker);
                this.supportRemoveKilledUnitsFromEnemyArmy.Enqueue(new KeyValuePair<WarUnit, decimal>(attacker,
                    unitsToRemoveFromAttacker));
            }
            
        }

        public void TryToAttackEnemyUnit(WarUnit player, WarUnit enemy, Weapon weapon)
        {
            int weaponDamage = 0;

            if (weapon != null)
            {
                weaponDamage = weapon.Damage;
            }
            
            decimal playerQuantity = MapManager.Instance.Battlefield.TryTakeEnemmyUnitQuantity(player);
            int playerDamage = (player.Damage + weaponDamage) * (int)playerQuantity;

            decimal enemyQuantity = MapManager.Instance.Battlefield.TryTakeFriendUnitQuantity(enemy);
            int enemyDamage = (int)(enemy.Damage * enemyQuantity);

            //if (player.inBattleTurn)
            //{
            //    this.AttackEnemyUnit(player, playerDamage, enemy, enemyDamage);
            //}

            //player.inBattleTurn = false;
            this.AttackEnemyUnit(player, playerDamage, enemy, enemyDamage);
        }

        private void AttackEnemyUnit(WarUnit player, int playerDamage, WarUnit enemy, int enemyDamage)
        {
            int unitsToRemoveFromDefender = playerDamage / enemy.GetDefaultHeаlth();
            int remainingDamageToDefender = playerDamage - (enemy.GetDefaultHeаlth() * unitsToRemoveFromDefender);

            unitsToRemoveFromDefender += enemy.DecreaseHealth(remainingDamageToDefender);

            this.supportRemoveKilledUnitsFromEnemyArmy.Enqueue(new KeyValuePair<WarUnit, decimal>(enemy,
                unitsToRemoveFromDefender));

            
            // counter-attack
            if (!player.AmIArcherOrMage)
            {
                int unitsToRemoveFromAttacker = enemyDamage / player.GetDefaultHeаlth();
                int remainingDamageToAttacker = enemyDamage - (unitsToRemoveFromAttacker * player.GetDefaultHeаlth());

                unitsToRemoveFromAttacker += player.DecreaseHealth(remainingDamageToAttacker);

                this.supportRemoveKilledUnitsFromPlayerArmy.Enqueue(new KeyValuePair<WarUnit, decimal>(player,
                    unitsToRemoveFromAttacker));

                
            }
        }

        public Dictionary<WarUnit, decimal> TryTakeEnemyArmy()
        {
            return this.enemyUnits;
        }

        public bool CanShoot()
        {
            foreach (var unit in this.playerUnits.Keys)
            {
                if (unit.IsArrowHasTarget)
                {
                    return false;
                }
            }

            foreach (var unit in this.enemyUnits.Keys)
            {
                if (unit.IsArrowHasTarget)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
