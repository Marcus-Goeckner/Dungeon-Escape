using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClasses {

    /// <summary>
    /// Map.cs
    /// Written By: Marcus Goeckner
    /// 3/1/19
    /// Description: Map class to hold all map cells, available potions, available weapons, and possible monsters.
    /// </summary>
    [Serializable]
    public class Map {

        #region Class level Variables

        // Private class level variables to hold Map data.
        private MapCell[,] _GameBoard = null;
        private List<Item> _Items;
        private List<Monster> _Monsters;
        private Hero _Hero;

        #endregion

        #region Constructors

        public Map(int rows, int columns) {
            _GameBoard = new MapCell[rows, columns];
            FillPotions();
            FillWeapons();
            FillMonsters();
            FillMap();
        }

        #endregion

        #region Properties

        // Property to show the game board (read-only).
        public MapCell[,] GameBoard {
            get {
                return _GameBoard;
            }
        }

        // Property to show the list of game items (read-only).
        private List<Item> Items {
            get {
                if (_Items == null) {
                    _Items = new List<Item>();
                }
                return _Items;
            }
        }

        // Property to show the list of monsters (read-only).
        private List<Monster> Monsters {
            get {
                if (_Monsters == null) {
                    _Monsters = new List<Monster>();
                }
                return _Monsters;
            }
        }

        // Property to show the hero of the map.
        public Hero Hero {
            get {
                return _Hero;
            }
            set {
                _Hero = value;
            }
        }

        // Property to show the HP of the Monster with the most HP in the list of monsters (read-only).
        public int HealthOfStrongestMonster {
            get {
                int healthOfStrongestMonster = Monsters.Max(monster => monster.MaxHitPoints);
                return healthOfStrongestMonster;                 
            }
        }

        // Property to show the value of the weakest weapon in the list of items (read-only).
        public int ValueOfWeakestWeapon {
            get {
                var weaponsList = Items.Where(item => item.GetType() == typeof(Weapon));
                int valueOfWeakestWeapon = weaponsList.Min(weapon => weapon.Value);
                return valueOfWeakestWeapon;
            }
        }

        // Property to show the average healing value for all of the potions in the list of items (read-only).
        public double AveragePotionValue {
            get {
                var potionsList = Items.Where(item => item.GetType() == typeof(Potion));
                double avgPotionValue = potionsList.Average(potion => potion.Value);
                return avgPotionValue;
            }
        }

        // Property to show the count of how many monsters are in the map (read-only).
        public int MonsterCount {
            get {
                int monsterCount = 0;
                for (int row = 0; row < GameBoard.GetLength(0); row++) {
                    for (int column = 0; column < GameBoard.GetLength(1); column++) {
                        if (GameBoard[row,column].HasMonster) {
                            monsterCount++;
                        }
                    }
                }
                return monsterCount;
            }
        }

        // Property to show the count of how many items are in the map (read-only).
        public int ItemCount {
            get {
                int itemCount = 0;
                for (int row = 0; row < GameBoard.GetLength(0); row++) {
                    for (int column = 0; column < GameBoard.GetLength(1); column++) {
                        if (GameBoard[row, column].HasItem) {
                            itemCount++;
                        }
                    }
                }
                return itemCount;
            }
        }

        // Property to show the percentage of the map that has been discovered (read-only).
        public double PercentageDiscovered {
            get {
                double discoveredMapCellCount = 0;
                double totalMapCellCount = 0;
                for (int row = 0; row < GameBoard.GetLength(0); row++) {
                    for (int column = 0; column < GameBoard.GetLength(1); column++) {
                        if (GameBoard[row, column].IsDiscovered) {
                            discoveredMapCellCount++;
                        }
                        totalMapCellCount++;
                    }
                }
                return (discoveredMapCellCount / totalMapCellCount) * 100.00;
            }
        }

        #endregion

        #region Class Methods

        /// <summary>
        /// Fills the game board with a hero, map cells that have randomly generated items, monsters, a door, and a key in them.
        /// </summary>
        private void FillMap() {
            Random rand = new Random();
            for (int row = 0; row < GameBoard.GetLength(0); row++) {
                for (int column = 0; column < GameBoard.GetLength(1); column++) {
                    _GameBoard[row, column] = new MapCell();
                    if (rand.Next(8) == 0) {
                        _GameBoard[row, column].Monster = _Monsters[rand.Next(5)].CreateCopy();
                    } else if (rand.Next(8) == 7) {
                        if (rand.Next(8) <= 3) {
                            Potion potion = (Potion)_Items[rand.Next(4)];
                            _GameBoard[row, column].Item = potion.CreateCopy();
                        } else {
                            Weapon weapon = (Weapon)_Items[rand.Next(4, 8)];
                            _GameBoard[row, column].Item = weapon.CreateCopy();
                        }
                    }
                }
            }

            MapCell mapCell;
            bool keyPlaced = false;
            bool doorPlaced = false;
            
            // Place a single key randomly in an empty map cell.
            while (keyPlaced == false) {
                mapCell = _GameBoard[rand.Next(GameBoard.GetLength(0)), rand.Next(GameBoard.GetLength(1))];
                if (mapCell.HasItem == false && mapCell.HasMonster == false) {
                    mapCell.Item = new Door("Door", 1, "Enter");
                    keyPlaced = true;
                }
            }

            // Place a single door randomly in an empty map cell.
            while (doorPlaced == false) {
                mapCell = _GameBoard[rand.Next(GameBoard.GetLength(0)), rand.Next(GameBoard.GetLength(1))];
                if (mapCell.HasItem == false && mapCell.HasMonster == false) {
                    mapCell.Item = new DoorKey("Key", 1, "Enter");
                    doorPlaced = true;
                }
            }

        }

        /// <summary>
        /// Fills _Items array with all possible potions.
        /// </summary>
        private void FillPotions() {
            _Items = new List<Item>();
            _Items.Add(new Potion("Small Healing Potion", 25, Potion.Colors.Blue));
            _Items.Add(new Potion("Medium Healing Potion", 50, Potion.Colors.Green));
            _Items.Add(new Potion("Large Healing Potion", 75, Potion.Colors.Red));
            _Items.Add(new Potion("Extreme Healing Potion", 150, Potion.Colors.Purple));       
        }

        /// <summary>
        /// Fills _Items array with all possible weapons.
        /// </summary>
        private void FillWeapons() {
            _Items.Add(new Weapon("Dagger", 5, 1));
            _Items.Add(new Weapon("Club", 10, 2));
            _Items.Add(new Weapon("Sword", 15, 3));
            _Items.Add(new Weapon("Claymore", 25, 5));
        }

        /// <summary>
        /// Fills _Monsters array with all possible monsters.
        /// </summary>
        private void FillMonsters() {
            _Monsters = new List<Monster>();
            _Monsters.Add(new Monster("Orc", "The Menace", 0, 0, 100, 10, 25));
            _Monsters.Add(new Monster("Goblin", "The Lurker", 1, 1, 75, 15, 20));
            _Monsters.Add(new Monster("Giant Slime", "The Gush", 2, 2, 25, 2, 10));
            _Monsters.Add(new Monster("Rat", "The Pathetic", 3, 3, 10, 5, 5));
            _Monsters.Add(new Monster("Skeleton", "The Spooky", 4, 4, 50, 10, 15));
        }

        /// <summary>
        /// Gets the current mapCell the hero is in.
        /// </summary>
        /// <returns>New map cell if hero doesn't exist, else return the current map cell the hero is in.</returns>
        public MapCell GetCurrentPosition() {
            if (Hero == null) {
                return new MapCell();
            } else {
                int x = Hero.PositionX;
                int y = Hero.PositionY;
                return GameBoard[y, x];
            }
        }

        /// <summary>
        /// Move the hero around the game board.
        /// </summary>
        /// <param name="direction">Direction for hero to move using directions enum from actor class.</param>
        public bool MoveHero(Actor.Directions direction) {
            if (direction == Actor.Directions.Up && Hero.PositionY != 0) {
                Hero.MovePosition(direction);
            } else if (direction == Actor.Directions.Down && Hero.PositionY != GameBoard.GetLength(0)- 1) {
                Hero.MovePosition(direction);
            } else if (direction == Actor.Directions.Left && Hero.PositionX != 0) {
                Hero.MovePosition(direction);
            } else if (direction == Actor.Directions.Right && Hero.PositionX != GameBoard.GetLength(1) - 1) {
                Hero.MovePosition(direction);
            }
            _GameBoard[Hero.PositionY, Hero.PositionX].IsDiscovered = true;

            MapCell currentPosition = GetCurrentPosition();
            if (currentPosition.HasMonster || currentPosition.HasItem) {
                return true;
            } else {
                return false;
            }
        }

        #endregion

    }
}
