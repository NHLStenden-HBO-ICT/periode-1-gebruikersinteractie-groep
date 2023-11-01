using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Wasmachine9000.Game;

namespace Wasmachine9000.Windows
{
    /// <summary>
    /// Interaction logic for Winkel.xaml
    /// </summary>
    public partial class Winkel : Window
    {
        public Winkel()
        {
            InitializeComponent();
            // sets WPS height and width to the same height and width as the primary display
            this.Height = SystemParameters.FullPrimaryScreenHeight;
            this.Width = SystemParameters.FullPrimaryScreenWidth;

            UpdateCoinAmount();
            checkCosmetics();
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Helpers.OpenPreviousWindow();
            }
        }

        private void checkCosmetics()
        {
            GameState gameState = GameState.LoadGameState();
            Dictionary<string, Dictionary<string, bool>> checkCosmetic1 = gameState.GetAllCosmetics();

            if (checkCosmetic1["Cosmetic1"]["Bought"] == true)
            {
                EquipButton1.Visibility = Visibility.Visible;
                if (checkCosmetic1["Cosmetic1"]["Equipped"] == true)
                {
                    EquipButton1.Content = "Equipped";
                }
                else
                {
                    EquipButton1.Content = "Equip";
                }
            }
            else
            {
                EquipButton1.Visibility = Visibility.Collapsed;
            }

            if (checkCosmetic1["Cosmetic2"]["Bought"] == true)
            {
                EquipButton2.Visibility = Visibility.Visible;
                if (checkCosmetic1["Cosmetic2"]["Equipped"] == true)
                {
                    EquipButton2.Content = "Equipped";
                }
                else
                {
                    EquipButton2.Content = "Equip";
                }
            }
            else
            {
                EquipButton2.Visibility = Visibility.Collapsed;
            }

            if (checkCosmetic1["Cosmetic3"]["Bought"] == true)
            {
                EquipButton3.Visibility = Visibility.Visible;
                if (checkCosmetic1["Cosmetic3"]["Equipped"] == true)
                {
                    EquipButton3.Content = "Equipped";
                }
                else
                {
                    EquipButton3.Content = "Equip";
                }
            }
            else
            {
                EquipButton3.Visibility = Visibility.Collapsed;
            }

            if (checkCosmetic1["Cosmetic4"]["Bought"] == true)
            {
                EquipButton4.Visibility = Visibility.Visible;
                if (checkCosmetic1["Cosmetic4"]["Equipped"] == true)
                {
                    EquipButton4.Content = "Equipped";
                }
                else
                {
                    EquipButton4.Content = "Equip";
                }
            }
            else
            {
                EquipButton4.Visibility = Visibility.Collapsed;
            }

            if (checkCosmetic1["Cosmetic5"]["Bought"] == true)
            {
                EquipButton5.Visibility = Visibility.Visible;
                if (checkCosmetic1["Cosmetic5"]["Equipped"] == true)
                {
                    EquipButton5.Content = "Equipped";
                }
                else
                {
                    EquipButton5.Content = "Equip";
                }
            }
            else
            {
                EquipButton5.Visibility = Visibility.Collapsed;
            }

            if (checkCosmetic1["Cosmetic6"]["Bought"] == true)
            {
                EquipButton6.Visibility = Visibility.Visible;
                if (checkCosmetic1["Cosmetic6"]["Equipped"] == true)
                {
                    EquipButton6.Content = "Equipped";
                }
                else
                {
                    EquipButton6.Content = "Equip";
                }
            }
            else
            {
                EquipButton6.Visibility = Visibility.Collapsed;
            }
        }

        private void UpdateCoinAmount()
        {
            // Load the GameState to get the coin amount
            GameState gameState = GameState.LoadGameState();
            int coinAmount = gameState.GetCoins();
            // Update the TextBlock with the coin amount
            CoinsText.Text = coinAmount.ToString();
        }

        private void CosmeticButton_Click(object sender, RoutedEventArgs e)
        {
            // Get clicked cosmetic name
            Button button = (Button)sender;
            string cosmeticName = button.Tag as string;

            // Load the GameState to get the coin amount
            GameState gameState = GameState.LoadGameState();
            int coinAmount = gameState.GetCoins();
            int requiredCoins = 111;

            if (coinAmount >= requiredCoins)
            {
                // deduct coins and save
                coinAmount -= requiredCoins;
                gameState.SetCoins(coinAmount);

                // Update cosmetic 
                gameState.UpdateCosmetic(cosmeticName, "Bought", true);

                // Update buttons
                checkCosmetics();
            }
            else
            {
                MessageBox.Show("Je hebt niet genoeg munten!");
            }

            UpdateCoinAmount();
        }

        private void EquipButton_Click(object sender, RoutedEventArgs e)
        {
            GameState gameState = GameState.LoadGameState();
            Button button = (Button)sender;
            string cosmeticName = button.Tag as string;
            TextBlock theText = button.Template.FindName("theText", button) as TextBlock;

            if (button.Content == "Equip")
            {
                // Change
                // button.Content = "Unequip";
                gameState.UpdateCosmetic(cosmeticName, "Equipped", true);
            }
            else if (button.Content == "Equipped")
            {
                // Change
                // button.Content = "Equip";
                gameState.UpdateCosmetic(cosmeticName, "Equipped", false);
            }

            checkCosmetics();
        }

        private void Back_OnClick(object sender, RoutedEventArgs e)
        {
            Helpers.OpenPreviousWindow();
        }
    }
}