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
        public string BuyText1Content { get; set; }
        public Winkel()
        {
            InitializeComponent();
            // sets WPS height and width to the same height and width as the primary display
            this.Height = SystemParameters.FullPrimaryScreenHeight;
            this.Width = SystemParameters.FullPrimaryScreenWidth;
            
            UpdateCoinAmount();
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Helpers.OpenPreviousWindow();
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
        
        private void Buy1_Click(object sender, RoutedEventArgs e)
        {
            int itemPrice = 100; // Set the price of the item here
            GameState gameState = GameState.LoadGameState();
    
            if (gameState.GetCoins() >= itemPrice)
            {
                // Deduct coins from user
                gameState.SetCoins(gameState.GetCoins() - itemPrice);

                // Update user coin ammount display
                CoinsText.Text = gameState.GetCoins().ToString();

                // Update  button to "Equip"
                // Button buyButton = (Button)sender;
                // BuyButton1.DataContext = this;
                // BuyText1Content = "111";
                
            }
            else
            {
                MessageBox.Show("Je hebt niet genoeg munten om deze Cosmetica te kopen.");
            }
        }
    }
}