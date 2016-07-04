using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using AthameRPG.Characters.WarUnits;
using AthameRPG.GameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AthameRPG.Objects.BattleFields
{
    public class Battlefield
    {
        // instance/loaded from MapManager

        private const string DefaultImagePath = "../Content/Obstacles/stonebattlefield";
        private const string SmallLettersPath = "../Content/Fonts/SmallLetters";
        
        // Enemy change this value when set ItIsBattle....
        public static int unitStreghtLevelIndex = 7;

        protected Texture2D battlefieldImage;
        protected string imagePath;
        protected Vector2 battlefieldDrawCoord;
        protected SpriteFont spriteFontSmallLetters;
        
        private Dictionary<WarUnit, decimal> playerUnits;
        private Dictionary<WarUnit, decimal> enemyUnits;

        private bool playerTurn;

        private WarUnit supportUnit;

        
        public Battlefield()
        {
            this.imagePath = DefaultImagePath;
            this.playerUnits = new Dictionary<WarUnit, decimal>();
            this.enemyUnits = new Dictionary<WarUnit, decimal>();
            
        }
        
        public void LoadContent(ContentManager contentManager)
        {
            this.battlefieldImage = contentManager.Load<Texture2D>(this.imagePath);
            this.spriteFontSmallLetters = contentManager.Load<SpriteFont>(SmallLettersPath);

        }

        public void Update(GameTime gameTime)
        {
            this.TrySwitchTurn();
            this.TryRemoveDeathUnits();

            foreach (var playerUnit in this.playerUnits)
            {
                if ((playerUnit.Key.GetStrengthLevel == unitStreghtLevelIndex) && this.playerTurn && playerUnit.Key.inBattleTurn == true)
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
            foreach (var enemyUnit in this.enemyUnits)
            {
                enemyUnit.Key.Update(gameTime);

                if (!this.playerTurn && enemyUnit.Key.GetStrengthLevel == unitStreghtLevelIndex)
                {
                    enemyUnit.Key.MoveInBattle();
                }
            }

            this.CheckEnemyUnitsForInBattleTurn();
        }

        private void TryRemoveDeathUnits()
        {
            Queue<WarUnit> deathUnit = new Queue<WarUnit>();

            foreach (var unit in this.playerUnits)
            {
                if (unit.Value < 0)
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
                if (unit.Value < 0)
                {
                    deathUnit.Enqueue(unit.Key);
                }
            }

            while (deathUnit.Count > 0)
            {
                this.enemyUnits.Remove(deathUnit.Dequeue());
            }


        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.battlefieldImage, this.battlefieldDrawCoord, Color.White);

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

        public void LoadArmies(IReadOnlyDictionary<WarUnit, decimal> playerArmy, IReadOnlyDictionary<WarUnit, decimal> enemyArmy)
        {
            this.playerUnits.Clear();
            this.enemyUnits.Clear();

            this.playerUnits = (Dictionary<WarUnit, decimal>)playerArmy;
            this.enemyUnits = (Dictionary<WarUnit, decimal>)enemyArmy;

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
                if (enemyUnit.Key == unit )
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

        public void AttackPlayerUnit(WarUnit defender, int damage)
        {
            int removedUnits = damage/ defender.Health;
            int remainingDamage = damage% defender.Health;
            this.playerUnits[defender] -= removedUnits;
            defender.DecreaseHealth(remainingDamage);
            
        }
        public void AttackEnemyUnit(WarUnit unit, decimal damage)
        {

        }
    }
}
