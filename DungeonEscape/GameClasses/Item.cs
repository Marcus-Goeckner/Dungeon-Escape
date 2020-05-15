using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClasses {

    /// <summary>
    /// Item.cs
    /// Written By: Marcus Goeckner
    /// 3/1/19
    /// Description: Item in the game that can heal or damage actors.
    /// </summary>
    [Serializable]
    public abstract class Item {

        #region Protected Variables
        // Protected variables to hold the item's data.
        protected string _Name;
        protected int _Value;
        #endregion

        #region Constructors
        public Item(string name, int itemValue) {
            _Name = name;
            _Value = itemValue;
        }
        #endregion

        #region Public Properties

        // Property to show the item's name.
        public string Name {
            get {
                return _Name;
            }
            set {
                _Name = value;
            }
        }

        // Property to show the item's value.
        public int Value {
            get {
                return _Value;
            }
            set {
                _Value = value;
            }
        }
        #endregion
    }
}
