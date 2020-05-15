using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClasses {

    /// <summary>
    /// DoorKey.cs
    /// Written By: Marcus Goeckner
    /// 3/1/19
    /// Description: DoorKey class that inherits from Item. Used to open doors.
    /// </summary>
    [Serializable]
    public class DoorKey : Item {

        #region Private Variables

        // Private variable to hold DoorKey data.
        private string _Code;

        #endregion

        #region Constructors

        public DoorKey(string name, int value, string code) : base(name, value) {
            _Code = code;
        }

        #endregion

        #region Public Properties

        // Property to expose the DoorKey's code (read-only).
        public string Code {
            get {
                return _Code;
            }
        }

        #endregion
    }
}
