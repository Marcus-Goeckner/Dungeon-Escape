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
    /// frmRunAway.xaml.cs
    /// CS 1182
    /// Written By: Marcus Goeckner
    /// 4/5/19
    /// John Holmes
    /// Description: Desription form to tell the user what happened when they tried to run away.
    /// </summary>
    public partial class frmRunAway : Window {

        // Global variables to hold the monster at the current map cell and the hero.
        Hero hero = Game.Map.Hero;
        Monster monster = Game.Map.GetCurrentPosition().Monster;

        public frmRunAway() {
            InitializeComponent();
            if (hero.AttackSpeed <= monster.AttackSpeed) {
                tbRanAway.Text = "You got away, but were too slow and took some damage.";
            } else {
                tbRanAway.Text = "You got away unscathed.";
            }
        }



        private void btnOk_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }
    }
}
