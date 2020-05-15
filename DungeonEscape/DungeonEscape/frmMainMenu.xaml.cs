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
using System.IO;
using Microsoft.Win32;
using System.Runtime.Serialization.Formatters.Binary;
using GameClasses;


namespace DungeonEscape {

    /// <summary>
    /// frmMainMenu.xaml.cs
    /// CS 1182
    /// Written By: Marcus Goeckner
    /// 4/13/19
    /// John Holmes
    /// Description: Window that displays when the game is first launched.
    /// </summary>
    public partial class frmMainMenu : Window {

        public frmMainMenu() {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e) {
            // Start a new game after entering in a name and title.
            if (txtName.Text != "" && txtTitle.Text!= "") {
                Game.ResetGame(10, 10,txtName.Text, txtTitle.Text);
                this.Close();
                frmInstructions frmInstructions = new frmInstructions();
                frmInstructions.ShowDialog();
            } else {
                tbError.Text = "Enter your name and title first!";
            }
        }

        private void btnLoadGame_Click(object sender, RoutedEventArgs e) {
            FileStream fs = null;
            Map map;
            try {
                // Create an open file dialog.
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "Map File (*.map)|*.map";
                if (ofd.ShowDialog() == true) {
                    BinaryFormatter bf = new BinaryFormatter();
                    fs = new FileStream(ofd.FileName, FileMode.Open);
                    map = (Map)bf.Deserialize(fs);
                    // Set the current map to a previously saved map.
                    Game.Map = new Map(10, 10);
                    Game.Map = map;
                    this.Close();
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            } finally {
                if (fs != null) {
                    fs.Close();
                }
            }
        }
    }
}
