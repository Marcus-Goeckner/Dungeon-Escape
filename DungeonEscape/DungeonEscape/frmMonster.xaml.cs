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
    /// frmMonster.xaml.cs
    /// Written By: Marcus Goeckner
    /// 3/17/19
    /// Description: Window that displays when the hero encounters a monster.
    /// </summary>
    public partial class frmMonster : Window {

        MediaPlayer hitSound;

        // Code used to disable the close 'X' button at the top of the window to prevent the player from not selecting fight or run away.
        // Retrieved from: https://www.codeproject.com/Tips/1155345/How-to-Remove-the-Close-Button-from-a-WPF-ToolWind
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        // Global variables to hold the monster at the current map cell and the hero.
        Hero hero = Game.Map.Hero;
        Monster monster = Game.Map.GetCurrentPosition().Monster;
    
        public frmMonster() {
            InitializeComponent();
            Loaded += Window_Loaded;
            tbHero.Text = hero.Name + "\r\n" + hero.CurrentHitPoints + "/" + hero.MaxHitPoints;
            tbMonster.Text = monster.Name + "\r\n" + monster.CurrentHitPoints + "/" + monster.MaxHitPoints;
        }

        /// <summary>
        /// Window loaded event to remove the close button.
        /// Retrieved from: https://www.codeproject.com/Tips/1155345/How-to-Remove-the-Close-Button-from-a-WPF-ToolWind
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e) {
            // Code to remove close box from window
            var hwnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }

        /// <summary>
        /// Uses overloaded + operator to make the monster and the hero fight.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAttack_Click(object sender, RoutedEventArgs e) {
            bool heroIsAlive = hero + monster;
            tbHero.Text = hero.Name + "\r\n" + hero.CurrentHitPoints + "/" + hero.MaxHitPoints;
            tbMonster.Text = monster.Name + "\r\n" + monster.CurrentHitPoints + "/" + monster.MaxHitPoints;
            hitSound = new MediaPlayer();
            // Sound retrieved from: https://soundimage.org/
            hitSound.Open(new Uri(@"assets/KnifeStab.wav", UriKind.Relative));
            hitSound.Play();
            if (heroIsAlive != true) {
                Game.GameState = Game.GameStates.Lost;
                this.Close();
                frmGameOver frmGameOver = new frmGameOver();
                frmGameOver.ShowDialog();
            } else if (monster.IsAlive != true) {
                Game.Map.GameBoard[hero.PositionY, hero.PositionX].Monster = null;
                this.Close();
            }

        }

        /// <summary>
        /// Button that allows player to run away from monster instead of fighting.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRunAway_Click(object sender, RoutedEventArgs e) {
            this.Close();
            hero.IsRunningAway = true;
            bool heroIsAlive = hero + monster;
            // Game is lost if hero dies when trying to run away.
            if (heroIsAlive != true) {
                Game.GameState = Game.GameStates.Lost;
                this.Close();
                frmGameOver frmGameOver = new frmGameOver();
                frmGameOver.ShowDialog();
            } else {
                // hero takes some damage becaue of slower speed.
                frmRunAway frmRunAway = new frmRunAway();
                frmRunAway.ShowDialog();
                hero.IsRunningAway = false;
                this.Close();         
            }
        }   
    }
}
