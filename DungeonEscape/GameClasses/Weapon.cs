using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClasses {

    /// <summary>
    /// Weapon.cs
    /// Written By: Marcus Goeckner
    /// 3/1/19
    /// Description: Weapon class inherited from item. Implements IRepeatable.
    /// </summary>
    [Serializable]
    public class Weapon : Item, IRepeatable<Weapon>{

        #region Private Variables

        // Private class level variable to hold Weapon data.
        private int _AttackSpeedModifier;

        #endregion

        #region Constructors

        public Weapon(string name, int value, int attackSpeedModifier) : 
            base (name, value) {
            _AttackSpeedModifier = attackSpeedModifier;
        }

        #endregion

        #region Public Properties

        // Property to show the weapon's attack speed modifier value (read-only).
        public int AttackSpeedModifier {
            get {
                return _AttackSpeedModifier;
            }
        }

        #endregion

        #region Class Methods

        /// <summary>
        /// Creates a deep copy of the current weapon.
        /// </summary>
        /// <returns>A deep copy of the object that calls the method.</returns>
        public Weapon CreateCopy() {
            Weapon weaponCopy = new Weapon(this.Name, this.Value, this.AttackSpeedModifier);
            return weaponCopy;
        }

        #endregion

    }
}
