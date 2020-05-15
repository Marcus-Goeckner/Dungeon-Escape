using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameClasses;

namespace DungeonEscape {

    /// <summary>
    /// Game.cs
    /// Written By: Marcus Goeckner
    /// 3/17/19
    /// Description: Static Game class that eill keep track of entire game.
    /// </summary>
    public static class Game {

        #region Private Variables

        // Private class level variables to hold game class data.
        private static GameStates _GameState;
        private static Map _Map;
        private static int _GameBoardHeight = 10;
        private static int _GameBoardWidth = 10;

        #endregion

        #region Public Properties

        // Property to show the current state of the game.
        public static GameStates GameState {
            get {
                if (Map.Hero.IsAlive == false) {
                    return GameStates.Lost;
                } else {
                    return _GameState;
                }
            }
            set {
                _GameState = value;
            }
        }

        // Property to show the map of the game.
        public static Map Map {
            get {
                return _Map;
            }
            set {
                _Map = value;
            }
        }

        #endregion

        #region Class Methods

        /// <summary>
        /// Reset the entire game to start again from the beginning with a newly generated map.
        /// </summary>
        /// <param name="gameBoardHeight">Height of the newly reset game board.</param>
        /// <param name="gameBoardWidth">Width of the newly rest gameboard.</param>
        public static void ResetGame(int gameBoardHeight, int gameBoardWidth, string heroName, string heroTitle) {
            GameState = GameStates.Running;
            _Map = new Map(gameBoardHeight, gameBoardWidth);

            Random rand = new Random();
            bool heroPlaced = false;
            
            // Randomly place the hero in a map cell that doesnt contain a monster or an item.
            while (heroPlaced == false) {
                int randomX = rand.Next(Map.GameBoard.GetLength(1));
                int randomY = rand.Next(Map.GameBoard.GetLength(0));
                MapCell startingMapCell = Map.GameBoard[randomY, randomX];
                if (startingMapCell.HasItem == false && startingMapCell.HasMonster == false) {
                    Map.Hero = new Hero(heroName, heroTitle, randomX, randomY, 200, 10);
                    heroPlaced = true;
                    Map.GameBoard[Map.Hero.PositionY, Map.Hero.PositionX].IsDiscovered = true;
                }
            }
        }

        /// <summary>
        /// Reset the entire game from the beginning with a newly generated map (using default field values for height and width).
        /// </summary>
        public static void ResetGame() {
            ResetGame(_GameBoardHeight, _GameBoardWidth, Game.Map.Hero.Name, Game.Map.Hero.Title);
        }

        #endregion

        #region Public Enumerables

        /// <summary>
        /// Enum values to be used to dictate the current state of the game.
        /// </summary>
        public enum GameStates{
            Running, Lost, Won
        }

        #endregion
    }
}