using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Globalization;

namespace GameClasses {

    /// <summary>
    /// Actor.cs
    /// Written By: Marcus Goeckner
    /// 3/1/19
    /// Description: Object in the game that are attackable and can attack.
    /// </summary>
    [Serializable]
    public abstract class Actor {

        #region Protected Variables

        // Protected class level variables to hold Actor data.
        protected string _Name;
        protected string _Title;
        protected double _AttackSpeed;
        protected int _PositionX;
        protected int _PositionY;
        protected int _MaxHitPoints;
        protected int _CurrentHitPoints;

        #endregion

        #region Constructors
        public Actor(string name, string title, int startX, int startY, int hitPoints, double attackSpeed) {
            _Name = CapitalizeAllWords(name);
            _Title = CapitalizeTitle(title);
            _PositionX = startX;
            _PositionY = startY;
            _CurrentHitPoints = _MaxHitPoints = hitPoints;
            _AttackSpeed = attackSpeed;       
        }
        #endregion

        #region Public Properties

        // Property for actor's name (capitalizes all words in the name when setting) (read-only).
        public string Name {
            get {
                return _Name;
            }
        }

        // Property for the actor's title (uses title case when setting) (read-only).
        public string Title {
            get {
                return _Title;
            }
        }

        // Property that shows actor's attack speed (read-only).
        public virtual double AttackSpeed {
            get {
                return _AttackSpeed;
            }
        }

        // Property that shows acor's current position on x-axis (read-only).
        public int PositionX {
            get {
                return _PositionX;
            }
        }

        // Property that shows actor's current position on y-axis (read-only).
        public int PositionY {
            get {
                return _PositionY;
            }
        }

        // Property that shows actor's max HP value (read-only).
        public int MaxHitPoints {
            get {
                return _MaxHitPoints;
            }
        }

        // Property that shows actor's current HP (read-only).
        public int CurrentHitPoints {
            get {
                return _CurrentHitPoints;
            }
        }

        // Property that shows if actor is alive (read-only).
        public bool IsAlive {
            get {
                if (CurrentHitPoints > 0) {
                    return true;
                } else {
                    return false;
                }
            }
        }
        #endregion

        #region Class Methods
        /// <summary>
        /// Combine the actor's name and title into one string.
        /// </summary>
        /// <returns> Actor's name and title joined together. </returns>
        public string GetNameWithTitle() {
            return Name + Title;
        }

        /// <summary>
        /// Moves the actor up, down, left, or right in game.
        /// </summary>
        /// <param name="direction"> Enum type direction for actor to move. </param>
        public virtual void MovePosition(Directions direction) {
            if (direction == Directions.Up) {
                _PositionY = _PositionY - 1;
            } else if (direction == Directions.Down) {
                _PositionY = _PositionY + 1;
            } else if (direction == Directions.Left) {
                _PositionX = _PositionX - 1;
            } else if (direction == Directions.Right){
                _PositionX = PositionX + 1;
            }
        }

        /// <summary>
        /// Take away hit points from actor when damaged.
        /// </summary>
        /// <param name="damageAmount"> the amount of HP to lose. </param>
        public void LoseHitPoints(int damageAmount) {
            if ((_CurrentHitPoints - damageAmount < 0) && (damageAmount > 0)) {
                _CurrentHitPoints = 0;
            } else if (damageAmount <= 0) {
                // if damage amount is less than or equal to zero, do nothing.
            } else {
                _CurrentHitPoints -= damageAmount;
            }
        }

        /// <summary>
        /// Give the actor more hit points when healed.
        /// </summary>
        /// <param name="healAmount"> The amount of hit points to gain. </param>
        public void GainHitPoints(int healAmount) {
            if ((_CurrentHitPoints + healAmount <= _MaxHitPoints) && (healAmount > 0)) {
                _CurrentHitPoints += healAmount;
            } else if (healAmount <= 0){
                // if heal amount is less than or equal to zero, do nothing.
            } else {
                _CurrentHitPoints = _MaxHitPoints;
            }        
        }

        /// <summary>
        /// Capitalize the first letter in a string.
        /// </summary>
        /// <param name="strToCap"> The string that is going to be capitalized. </param>
        /// <returns> String with first character capitalized and the rest lower case. </returns>
        public string CapitalizeFirst(string strToCap) {
            // Code based off John Homes MakingObjects in-class code
            return strToCap[0].ToString().ToUpper() + strToCap.Substring(1).ToLower();
        }

        /// <summary>
        /// Capitalize the first letter of all words in a string.
        /// </summary>
        /// <param name="strToCap"> The string of word(s) to capitalize. </param>
        /// <returns> String of words with the first character of each word capitalized. </returns>
        public string CapitalizeAllWords(string strToCap) {
            string[] splitWords = strToCap.Trim().Split(' ');
            string capitalizedWords = "";
            for (int ndx = 0; ndx < splitWords.Length; ndx++) {
                capitalizedWords += CapitalizeFirst(splitWords[ndx]) + " ";
            }
            return capitalizedWords.Trim();
        }

        /// <summary>
        /// Capitalizes string of words using title case conventions where articles and prepositions are ignored.
        /// </summary>
        /// <param name="strToTitleCap"> The string of words to be capitalized. </param>
        /// <returns> String capitalized with title case conventions. </returns>
        public string CapitalizeTitle(string strToTitleCap) {
            // Code based off of this forum page on "OmegaMan's Musings Technology blog": http://omegacoder.com/?p=575
            string strWorkingTitle = strToTitleCap;

            if (string.IsNullOrEmpty(strWorkingTitle) == false) {
                char[] space = new char[] { ' ' };

                List<string> artsAndPreps = new List<string>() {
                    "a", "an", "and", "any", "at", "from", "into", "of", "on", "or", "some", "the", "to", "with", "all",
                };

                CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
                TextInfo textInfo = cultureInfo.TextInfo;

                strWorkingTitle = textInfo.ToTitleCase(strToTitleCap.ToLower());

                List<string> tokens = strWorkingTitle.Split(space, StringSplitOptions.RemoveEmptyEntries).ToList();

                strWorkingTitle = "";
                
                strWorkingTitle += tokens.Aggregate<String, String>(String.Empty, (String prev, String input)
                               => prev + (artsAndPreps.Contains(input.ToLower())
                               ? " " + input.ToLower()            
                               : " " + input));                  

                strWorkingTitle = Regex.Replace(strWorkingTitle, @"(?!^Out)(Out\s+Of)", "out of");
            }
            return strWorkingTitle;
        }
        #endregion

        #region Public Enumerables

        /// <summary>
        /// Enum values to be used as arguments in MovePosition method.
        /// </summary>
        public enum Directions {
            Up, Down, Left, Right
        }

        #endregion
    }
}
