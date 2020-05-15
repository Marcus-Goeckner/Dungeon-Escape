using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClasses {

    /// <summary>
    /// Door.cs
    /// Written By: Marcus Goeckner
    /// 3/1/19
    /// Description: Door class that inherits from Item has a matching DoorKey code.
    /// </summary>
    [Serializable]
    public class Door : Item {

        #region Private Variables

        // Private variable to hold Door data.
        private string _Code;

        #endregion

        #region Constructors

        public Door(string name, int value, string code) : base(name, value) {
            _Code = code;
        }

        #endregion

        #region Public Properties

        // Property to expose the Door's code (read-only).
        public string Code {
            get {
                return _Code;
            }
        }

        #endregion

        #region Class Methods

        /// <summary>
        /// Checks if the doorkey passed in matches the door.
        /// </summary>
        /// <param name="doorKey">Door key to be checked against the door.</param>
        /// <returns>Bool indicating if the door and door key code match.</returns>
        public bool CheckDoorKey(DoorKey doorKey) {
            if (this.Code == doorKey.Code) {
                return true;
            } else {
                return false;
            }
        }

        #endregion
    }
}
