using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClasses {

    /// <summary>
    /// Monster.cs
    /// Written By: Marcus Goeckner
    /// 3/1/19
    /// Description: Monster class inherited from actor. Uses Repeatable and Combat interfaces. Has set attack value to inflict on a hero.
    /// </summary>
    [Serializable]
    public class Monster : Actor, IRepeatable<Monster>, ICombat {

        #region Private Variables

        // Private class level variable to hold Monster data.
        private int _AttackValue;

        #endregion

        #region Constructors

        public Monster(string name, string title, int startX, int startY, int hitPoints, double attackSpeed, int attackValue) :
            base(name, title, startX, startY, hitPoints, attackSpeed) {
            _AttackValue = attackValue;
        }

        #endregion

        #region Public Properties

        // Property that shows a monster's attack value (read-only).
        public int AttackValue {
            get {
                return _AttackValue;
            }
        }

        #endregion

        #region Class Methods

        /// <summary>
        /// Creates a deep copy of current monster.
        /// </summary>
        /// <returns>A deep copy of the monster that calls the method.</returns>
        public Monster CreateCopy() {
            Monster monsterCopy = new Monster(this.Name, this.Title, this.PositionX, this.PositionY, this.CurrentHitPoints, this.AttackSpeed, this.AttackValue);
            return monsterCopy;
        }

        /// <summary>
        /// Removes hit points from attacked actor and tells if the actor is still alive.
        /// </summary>
        /// <param name="actor">Actor to be attacked.</param>
        /// <returns>Bool indicating if actor is alive.</returns>
        public bool Attack(Actor actor) {
            actor.LoseHitPoints(this.AttackValue);
            return actor.IsAlive;
        }

        #endregion
    }
}
