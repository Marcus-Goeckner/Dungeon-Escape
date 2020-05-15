using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClasses {

    /// <summary>
    /// MapCell.cs
    /// Written By: Marcus Goeckner
    /// 3/1/19
    /// Description: Object in the game that holds actors and items.
    /// </summary>
    [Serializable]
    public class MapCell {

        #region Private Variables

        // Private variables to hold map cell's data.
        private bool _IsDiscovered;
        private Monster _Monster;
        private Item _Item;

        #endregion

        #region Constructors

        public MapCell() {
            IsDiscovered = false;
        }

        #endregion

        #region Public Properties

        // Property to show if a cell had been discovered.
        public bool IsDiscovered {
            get {
                return _IsDiscovered;
            } set {
                _IsDiscovered = value;
            }
        }

        // Property to access the MapCell's monster.
        public Monster Monster {
            get {
                return _Monster;
            }
            set {
                _Monster = value;
            }
        }

        // Property to access the Mapcell's Item.
        public Item Item {
            get {
                return _Item;
            }
            set {
                _Item = value;
            }
        }

        // Property to show if a cell has an enemy monster in it.
        public bool HasMonster {
            get {
                if (_Monster != null) {
                    return true;
                } else {
                    return false;
                }
            }
        }

        // Property to show if a cell has an item in it.
        public bool HasItem {
            get {
                if (_Item != null) {
                    return true;
                } else {
                    return false;
                }
            }
        }
        #endregion
    }
}
