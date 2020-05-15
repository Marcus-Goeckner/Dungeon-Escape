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
    /// frmItem.xaml.cs
    /// Written By: Marcus Goeckner
    /// 3/17/19
    /// Description: Window that displays when the hero finds an item.
    /// </summary>
    public partial class frmItem : Window {
        public frmItem() {
            InitializeComponent();
            // Display the name of the found item to a text block.
            tbFoundItem.Text = "You found a " + Game.Map.GetCurrentPosition().Item.Name + ". Do you want to take it?";
            if (Game.Map.GetCurrentPosition().Item.GetType() == typeof(Potion)) {
                tbFoundItem.Text += " (+" + Game.Map.GetCurrentPosition().Item.Value + " hp)";
            } else if (Game.Map.GetCurrentPosition().Item.GetType() == typeof(Weapon)) {
                tbFoundItem.Text += " (+" + Game.Map.GetCurrentPosition().Item.Value + " dmg)";
            }
        }

        private void btnYes_Click(object sender, RoutedEventArgs e) {
            Item item = Game.Map.GetCurrentPosition().Item;
            Item droppedItem = Game.Map.Hero.ApplyItem(item);
            Game.Map.GetCurrentPosition().Item = droppedItem;

            this.Close();
        }

        private void btnNo_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }
    }
}
