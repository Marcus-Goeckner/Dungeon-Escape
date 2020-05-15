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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Media;
using System.IO;
using Microsoft.Win32;
using System.Runtime.Serialization.Formatters.Binary;
using GameClasses;


namespace DungeonEscape {

    /// <summary>
    /// MainWindow.xaml.cs
    /// CS 1182
    /// Written By: Marcus Goeckner
    /// 4/5/19
    /// John Holmes
    /// Description: WPF application to run the game.
    /// WOWNESS: Added music, sound effects, graphics, the ability to choose your name, the ability for the game window to scale, and and an instructions screen.
    /// </summary>
    public partial class MainWindow : Window {

        // Player variables to play music and soaund effects in game.
        MediaPlayer monsterEncounter;
        SoundPlayer themeMusic;


        public MainWindow() {
            InitializeComponent();

            themeMusic = new SoundPlayer();
            // All music and sounds retrieved from: https://soundimage.org/
            themeMusic.SoundLocation = @"assets/DungeonEscapeThemeMusic.wav";
            themeMusic.PlayLooping();
            
            // Show the main menu as soon as the game launches.
            frmMainMenu frmMain = new frmMainMenu();
            frmMain.ShowDialog();

            // Show the map and hero details when game begins.
            UpdateHeroDetails();
            DrawMap();
        }

        // Button click event to reset the game with a newly generated map.
        private void btnRefreshMap_Click(object sender, RoutedEventArgs e) {
            Game.ResetGame();
            UpdateHeroDetails();
            DrawMap();
        }

        // Button click event to make the hero move up on the game board.
        private void btnUp_Click(object sender, RoutedEventArgs e) {
            Game.Map.MoveHero(Actor.Directions.Up);
            DrawMap();
            CheckMapCell();
            UpdateHeroDetails();
            DrawMap();
        }

        // Button click event to make the hero move right on the game board.
        private void btnRight_Click(object sender, RoutedEventArgs e) {
            Game.Map.MoveHero(Actor.Directions.Right);
            DrawMap();
            CheckMapCell();
            UpdateHeroDetails();
            DrawMap();
        }

        // Button click event to make the hero move down on the game board.
        private void btnDown_Click(object sender, RoutedEventArgs e) {
            Game.Map.MoveHero(Actor.Directions.Down);
            DrawMap();
            CheckMapCell();
            UpdateHeroDetails();
            DrawMap();
        }

        // Button click event to make the hero move left on the game board.
        private void btnLeft_Click(object sender, RoutedEventArgs e) {
            Game.Map.MoveHero(Actor.Directions.Left);
            DrawMap();
            CheckMapCell();
            UpdateHeroDetails();
            DrawMap();          
        }

        // Event listener to handle the keypress event for moving the hero around the game board.
        private void Window_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Up) {
                btnUp_Click(sender, e);
            } else if (e.Key == Key.Right) {
                btnRight_Click(sender, e);
            } else if (e.Key == Key.Down) {
                btnDown_Click(sender, e);
            } else if (e.Key == Key.Left) {
                btnLeft_Click(sender, e);
            }
        }

        /// <summary>
        /// Menu item click event that allows the player to save their current game progress.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveGame_Click(object sender, RoutedEventArgs e) {
            FileStream fs = null;
            try {
                // Create a save file dialog.
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Map File (*.map)|*.map";
                if (sfd.ShowDialog() == true) {
                    BinaryFormatter bf = new BinaryFormatter();
                    fs = new FileStream(sfd.FileName, FileMode.Create);
                    // Save the game map in a .map file.
                    bf.Serialize(fs, Game.Map);
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            } finally {
                if (fs != null) {
                    fs.Close();
                }
            }     
        }

        /// <summary>
        /// Load a previously saved game state.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    Game.Map = map;
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            } finally {
                if (fs != null) {
                    fs.Close();
                }
            }
            UpdateHeroDetails();
            DrawMap();
        }

        /// <summary>
        /// Loops through game board array of mapcells and displays the cells to the user.
        /// Images retrieved from: https://itch.io/game-assets/free/tag-pixel-art
        ///                        https://opengameart.org/
        ///                        https://www.pinterest.com/pin/471752129697165710/
        ///                        http://pixelartmaker.com/art/e97fe18f09c317a
        ///                        https://scratchwars.com/photos/zbrane_avatar/f/0/23.png?m=1485028045
        ///                        
        /// </summary>
        public void DrawMap() {

            grdMapCells.Children.Clear();

            // Loop through the entire game board.
            for (int row = 0; row < Game.Map.GameBoard.GetLength(0); row++) {
                for (int column = 0; column < Game.Map.GameBoard.GetLength(1); column++) {
                    TextBlock tbCell = new TextBlock();
                    MapCell mapCell = Game.Map.GameBoard[row, column];
                    tbCell.Name = "mapCell" + row + column;
                    tbCell.Tag = mapCell;
                    tbCell.TextAlignment = TextAlignment.Center;
                    tbCell.TextWrapping = TextWrapping.Wrap;

                    // Display the contents in map cells that have different potions.
                    if (mapCell.HasItem) {
                        if (mapCell.Item.GetType() == typeof(Potion) && mapCell.IsDiscovered) {
                            Potion potion = (Potion)mapCell.Item;
                            if (potion.Color == Potion.Colors.Blue) {
                                InlineUIContainer container = CreateImageContainer(@"assets/BluePotion.png");
                                tbCell.Inlines.Add(container); 
                            } else if (potion.Color == Potion.Colors.Green) {
                                InlineUIContainer container = CreateImageContainer(@"assets/GreenPotion.png");
                                tbCell.Inlines.Add(container);
                            } else if (potion.Color == Potion.Colors.Red) {
                                InlineUIContainer container = CreateImageContainer(@"assets/RedPotion.png");
                                tbCell.Inlines.Add(container);
                            } else if (potion.Color == Potion.Colors.Purple) {
                                InlineUIContainer container = CreateImageContainer(@"assets/PurplePotion.png");
                                tbCell.Inlines.Add(container);
                            }
                            // Display the map cell that contains the door.
                        } else if (mapCell.Item.GetType() == typeof(Door) && mapCell.IsDiscovered) {
                            InlineUIContainer container = CreateImageContainer(@"assets/Door.png");
                            tbCell.Inlines.Add(container);
                            // Display the map cell that contains the key.
                        } else if (mapCell.Item.GetType() == typeof(DoorKey) && mapCell.IsDiscovered) {
                            InlineUIContainer container = CreateImageContainer(@"assets/Key.png");
                            tbCell.Inlines.Add(container);
                        } else if (mapCell.Item.GetType() == typeof(Weapon) && mapCell.IsDiscovered){
                            if (mapCell.Item.Name == "Dagger") {
                                InlineUIContainer container = CreateImageContainer(@"assets/Dagger.png");
                                tbCell.Inlines.Add(container);
                            } else if (mapCell.Item.Name == "Club") {
                                InlineUIContainer container = CreateImageContainer(@"assets/Club.png");
                                tbCell.Inlines.Add(container);
                            } else if (mapCell.Item.Name == "Sword") {
                                InlineUIContainer container = CreateImageContainer(@"assets/Sword.png");
                                tbCell.Inlines.Add(container);
                            } else if (mapCell.Item.Name == "Claymore") {
                                InlineUIContainer container = CreateImageContainer(@"assets/Claymore.png");
                                tbCell.Inlines.Add(container);
                            }
                        }
                      

                    } else if (mapCell.HasMonster && mapCell.IsDiscovered) {
                        if (mapCell.Monster.Name == "Goblin") {
                            InlineUIContainer container = CreateImageContainer(@"assets/Goblin.png");
                            tbCell.Inlines.Add(container);
                        } else if (mapCell.Monster.Name == "Orc") {
                            InlineUIContainer container = CreateImageContainer(@"assets/Orc.png");
                            tbCell.Inlines.Add(container);
                        } else if (mapCell.Monster.Name == "Giant Slime") {
                            InlineUIContainer container = CreateImageContainer(@"assets/GiantSlime.png");
                            tbCell.Inlines.Add(container);
                        } else if (mapCell.Monster.Name == "Rat") {
                            InlineUIContainer container = CreateImageContainer(@"assets/Rat.png");
                            tbCell.Inlines.Add(container);
                        } else if (mapCell.Monster.Name == "Skeleton") {
                            InlineUIContainer container = CreateImageContainer(@"assets/Skeleton.png");
                            tbCell.Inlines.Add(container);
                        }
                   
                    }
                    if (mapCell == Game.Map.GetCurrentPosition()) {
                        InlineUIContainer container = CreateImageContainer(@"assets/Adventurer.png");
                        tbCell.Inlines.Add(container);
                    }
                    // Make cell black if its not discovered.
                    if (mapCell.IsDiscovered == false) {
                        tbCell.Background = Brushes.Black;
                        tbCell.Foreground = Brushes.Black;
                    }
                    
                    // Add the cells to the grid, showing the game board.
                    Grid.SetColumn(tbCell, column);
                    Grid.SetRow(tbCell, row);
                    grdMapCells.Children.Add(tbCell);
                }
            }
        }

        /// <summary>
        /// Checks if there is anything in the current map call and displays the correct window if there is anything present.
        /// </summary>
        private void CheckMapCell() {
            // Show game over dialog if current position has a door.
            if (Game.Map.GetCurrentPosition().HasItem && Game.Map.GetCurrentPosition().Item.GetType() == typeof(Door)) {
                frmGameOver frmGameOver = new frmGameOver();
                frmGameOver.ShowDialog();
                // Show item dialog if current position has an item.
            } else if (Game.Map.GetCurrentPosition().HasItem) {
                frmItem frmItem = new frmItem();
                frmItem.ShowDialog();
                // Show monster dialog if current position has a monster.
            } else if (Game.Map.GetCurrentPosition().HasMonster) {
                monsterEncounter = new MediaPlayer();
                // Music retrieved from: https://soundimage.org/
                monsterEncounter.Open(new Uri(@"assets/MonsterEncounter.wav", UriKind.Relative));
                monsterEncounter.Play();
                frmMonster frmMonster = new frmMonster();
                frmMonster.ShowDialog();
            }
        }

        /// <summary>
        /// Displays hero information such as current weapon, health, and key.
        /// </summary>
        private void UpdateHeroDetails() {
            Hero hero = Game.Map.Hero;
            lblHeroName.Content = Game.Map.Hero.GetNameWithTitle();
            lblHeroHP.Content = hero.CurrentHitPoints + "/" + hero.MaxHitPoints;
            if (hero.HasWeaponEquipped) {
                lblWeapon.Content = hero.EquippedWeapon.Name + " (+" + hero.EquippedWeapon.Value + " dmg)";
            } else {
                lblWeapon.Content = "None";
            }
            if (hero.HasDoorKey) {
                lblKey.Content = "Yes";
            } else {
                lblKey.Content = "None";
            }
        }

        /// <summary>
        /// Create an image container to be added to the textblocks on the game GUI.
        /// </summary>
        /// <param name="imagePath">relative path of the file to display.</param>
        /// <returns></returns>
        private InlineUIContainer CreateImageContainer(string imagePath) {
            BitmapImage imageSource = new BitmapImage(new Uri(imagePath, UriKind.Relative));
            Image image = new Image();
            image.Source = imageSource;
            InlineUIContainer container = new InlineUIContainer(image);
            return container;
        }
    }
}
