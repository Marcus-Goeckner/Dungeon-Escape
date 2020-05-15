using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using GameClasses;

namespace DungeonEscape {

    /// <summary>
    /// frmGameOver.xaml.cs
    /// CS 1182
    /// Written By: Marcus Goeckner
    /// 4/6/19
    /// John Holmes
    /// Description: Window that displays when the game could end.
    /// </summary>
    public partial class frmGameOver : Window {

        public frmGameOver() {
            InitializeComponent();
            MapCell currentMapCell = Game.Map.GetCurrentPosition();
            Hero hero = Game.Map.Hero;

            // Checks if the current map cell has a door and shows messages accordingly.
            if (currentMapCell.HasItem && currentMapCell.Item.GetType() == typeof(Door)) {
                Door door = (Door)currentMapCell.Item;
                // Win game is the hero has the matching doorkey and found the door.
                if (hero.HasDoorKey && door.CheckDoorKey(hero.DoorKey)) {
                    Game.GameState = Game.GameStates.Won;
                } else {
                    tbGameOver.Text = "You've found the door, but you still need the key. Keep looking!";
                }
            }

            // Textblock shows you have won the game, collapse the OK buton.
            if (Game.GameState == Game.GameStates.Won) {
                tbGameOver.Text = "Hooray! You've found the door and escaped the treacherous dungeon!";
                btnOk.Visibility = Visibility.Collapsed;
              // Make only "Ok' button visible.
            } else if (Game.GameState == Game.GameStates.Running) {
                btnExit.Visibility = Visibility.Collapsed;
                btnRestart.Visibility = Visibility.Collapsed;
                // Hide "Ok" button and tell user yhe hero has died.
            } else if (Game.GameState == Game.GameStates.Lost) {
                tbGameOver.Text = "Oh no... you've died.";
                btnOk.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Shuts down the game.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, RoutedEventArgs e) {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Resets the game.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRestart_Click(object sender, RoutedEventArgs e) {
            string name = Game.Map.Hero.Name;
            string title = Game.Map.Hero.Title;
            Game.ResetGame(10,10, name, title);
            this.Close();
        }

        /// <summary>
        /// Closes the window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }
    }
}
