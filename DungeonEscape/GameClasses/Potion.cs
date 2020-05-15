using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClasses {

    /// <summary>
    /// Potion.cs
    /// Written By: Marcus Goeckner
    /// 3/1/19
    /// Description: Potion class inherited from item. Can heal the hero.
    /// </summary>
    [Serializable]
    public class Potion : Item, IRepeatable<Potion> {

        #region Private Variables

        // Private class level variable to hold Potion data.
        private Colors _Color;

        #endregion

        #region Constructors

        public Potion(string name, int value, Colors color):
            base(name, value) {
            _Color = color;
        }

        #endregion

        #region Public Properties

        // Property to show the potion's color.
        public Colors Color {
            get {
                return _Color;
            }
            set {
                _Color = value;
            }
        }

        #endregion

        #region Class Methods

        /// <summary>
        /// Creates a deep copy of the current potion.
        /// </summary>
        /// <returns>A deep copy of the object that calls the method.</returns>
        public Potion CreateCopy() {
            Potion potionCopy = new Potion(this.Name, this.Value, this.Color);
            return potionCopy; 
        }

        #endregion

        #region Public Enumerables

        /// <summary>
        /// Enum values to be used for potion colors.
        /// </summary>
        public enum Colors {
            Blue, Green, Red, Purple
        }

        #endregion

    }
}
