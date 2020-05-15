using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClasses {

    /// <summary>
    /// Hero.cs
    /// Written By: Marcus Goeckner
    /// 3/1/19
    /// Description: Hero class inherited from actor. Can equip weapons.
    /// </summary>
    [Serializable]
    public class Hero : Actor, ICombat {

        #region Private Variables

        // Private class level variables to hold Hero data.
        private Weapon _EquippedWeapon;
        private bool _IsRunningAway;
        private DoorKey _DoorKey;

        #endregion

        #region Contructors

        public Hero(string name, string title, int startX, int startY, int hitPoints, double attackSpeed) :
            base(name, title, startX, startY, hitPoints, attackSpeed) {
            _IsRunningAway = false;
            _EquippedWeapon = null;
        }

        #endregion

        #region Public Properties

        // Property to show if a hero has a weapon equipped (read-only).
        public bool HasWeaponEquipped {
            get {
                if (_EquippedWeapon != null) {
                   return true;
                } else {
                   return false;
                }
            }
        }

        // Property to get the weapon that the hero has equipped (read-only).
        public Weapon EquippedWeapon {
            get {
                return _EquippedWeapon;
            }
        }

        // Property to get the equipped doorkey (read-only).
        public DoorKey DoorKey {
            get {
                return _DoorKey;
            }
        }

        // Property to show if a hero is running away from a monster.
        public bool IsRunningAway {
            get {
                return _IsRunningAway;
            }
            set {
                _IsRunningAway = value;
            }
        }

        // Property to show if the hero has a door key (read-only).
        public bool HasDoorKey {
            get {
                if (_DoorKey != null) {
                    return true;
                } else {
                    return false;
                }
            }
        }

        // Property to show the hero's attack damage value (read-only).
        public int AttackDamage {
            get {
                if (HasWeaponEquipped == true) {
                    return _EquippedWeapon.Value;
                } else {
                    return 1;
                }
            }
        }

        // Override AttackSpeed property from actor class (read-only).
        public override double AttackSpeed {
            get {
                if (HasWeaponEquipped == true) {
                    return _AttackSpeed - _EquippedWeapon.AttackSpeedModifier;
                } else {
                    return _AttackSpeed;
                }
            }
        }


        #endregion

        #region Class Methods

        /// <summary>
        /// Override MovePosition method from actor class.
        /// </summary>
        /// <param name="direction">Direction to move.</param>
        public override void MovePosition(Directions direction) {
            base.MovePosition(direction);
        }

        /// <summary>
        /// Removes hitpoints from attacked actor and tells if the actor is still alive.
        /// </summary>
        /// <param name="actor">Actor to be attacked.</param>
        /// <returns>Bool indicating if actor is alive.</returns>
        public bool Attack(Actor actor) {
            actor.LoseHitPoints(this.AttackDamage);
            return actor.IsAlive;
        }

        /// <summary>
        /// Applies an item to the hero.
        /// </summary>
        /// <param name="item">Item to apply.</param>
        /// <returns>Null if potion or there was no previously equipped item. The previously equipped item is returned if not null.</returns>
        public Item ApplyItem(Item item) {
            Item previouslyEquippedItem;
            if (item.GetType() == typeof(Potion)) {
                this.GainHitPoints(item.Value);
                return null;
            } else if (item.GetType() == typeof(Weapon)) {
                previouslyEquippedItem = _EquippedWeapon;
                _EquippedWeapon = (Weapon)item;
                return previouslyEquippedItem;
            } else if (item.GetType() == typeof(DoorKey)) {
                previouslyEquippedItem = _DoorKey;
                _DoorKey = (DoorKey)item;
                return previouslyEquippedItem;
            } else {
                return item;
            }
        }

        #endregion

        #region Operator OverLoads

        /// <summary>
        /// Overload + operator to make hero and monsters fight
        /// </summary>
        /// <param name="hero">hero to fight</param>
        /// <param name="monster">monster to fight</param>
        /// <returns>Bool indicating if the hero is alive after the fight.</returns>
        public static bool operator +(Hero hero, Monster monster) {
            if (hero.IsRunningAway == false) {
                // Hero attacks first if hero has greater attack speed and is alive.
                if (hero.AttackSpeed > monster.AttackSpeed && hero.IsAlive) {
                    hero.Attack(monster);
                    // Monster attacks after if still alive.
                    if (monster.IsAlive) {
                        monster.Attack(hero);
                    }
                // Monster attacks first if monster has greater attack speed and is alive.
                } else if (hero.AttackSpeed < monster.AttackSpeed && monster.IsAlive) {
                    monster.Attack(hero);
                    // Hero attacks after if still alive.
                    if (hero.IsAlive) {
                        hero.Attack(monster);
                    }
                // If hero and monster both have the same attack speed and are both alive, attack at the same time.
                } else if (hero.AttackSpeed == monster.AttackSpeed && monster.IsAlive && hero.IsAlive) {
                    hero.Attack(monster);
                    monster.Attack(hero);
                }
            } else if (hero.IsRunningAway == true) {
                if (hero.AttackSpeed > monster.AttackSpeed) {
                    // Do nothing because hero gets away.
                } else if (hero.AttackSpeed <= monster.AttackSpeed && monster.IsAlive) {
                    monster.Attack(hero);
                }
            }
            return hero.IsAlive;
        }

        #endregion
    }
}
