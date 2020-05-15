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

namespace DungeonEscape {

    /// <summary>
    /// frmInstructions.xaml.cs
    /// CS 1182
    /// Written By: Marcus Goeckner
    /// 4/13/19
    /// John Holmes
    /// Description: Window that displays to explain game to the user.
    /// </summary>
    public partial class frmInstructions : Window {

        public frmInstructions() {
            InitializeComponent();
            tbHeroName.Text = Game.Map.Hero.GetNameWithTitle();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }
    }
}
