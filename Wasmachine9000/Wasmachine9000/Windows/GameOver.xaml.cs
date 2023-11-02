using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using Wasmachine9000.Game;

namespace Wasmachine9000.Windows
{
    /// <summary>
    /// Interaction logic for GameOver.xaml
    /// </summary>
    public partial class GameOver : Window
    {
        public GameOver()
        {
            InitializeComponent();

            if (App.GameState.GetUsername() != null)
            {
                Playername.Text = App.GameState.GetUsername();
            }

            //View score on the game over screen
            ScoreTextBlock.Text = App.GameInfo.PlayerScore.ToString();
            // View coins gained
            CoinsText.Text = App.GameInfo.PlayerCoins.ToString();

            GameState gameState = GameState.LoadGameState();
            int coinAmount = gameState.GetCoins();
            gameState.SetCoins(coinAmount + App.GameInfo.PlayerCoins);
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                App.GameInfo.Reset();

                Helpers.OpenWindow(new MainWindow());
            }
        }

        private void MainWindow_Click(object sender, RoutedEventArgs e)
        {
            // Get the playername from the game over screen
            string playername = Playername.Text;

            // Call the PostScore method
            string username = playername;
            int score = App.GameInfo.PlayerScore;
            App.Scoreboard.PostScore(username, score);

            App.GameState.SetUsername(username);

            Debug.WriteLine("Score gepost!");

            // Reset score
            App.GameInfo.Reset();

            Helpers.OpenWindow(new MainWindow());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }

        private void PlayAgain_Click(object sender, EventArgs e)
        {
            // Get the playername from the game over screen
            string playername = Playername.Text;

            // Call the PostScore method
            string username = playername;
            int score = App.GameInfo.PlayerScore;
            App.Scoreboard.PostScore(username, score);

            App.GameState.SetUsername(username);

            Debug.WriteLine("Score gepost!");

            // Reset score
            App.GameInfo.Reset();

            if (App.GameState.PlayLockedUntil > DateTime.Now && App.GameState.PlaytimeControl)
            {
                MessageBox.Show("Je mag niet spelen :(. Je moet wachten tot " + App.GameState.PlayLockedUntil);
                Helpers.OpenWindow(new MainWindow());
                return;
            }

            Helpers.OpenWindow(new GameWindow());
        }

        private void Playername_IsFilled(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Playername.Text) || Playername.Text.Trim().Length < 3)
            {
                PlayAgainButton.Visibility = Visibility.Collapsed;
                BackHome.Visibility = Visibility.Collapsed;
                BackHomeDummy.Visibility = Visibility.Visible;
                PlayAgainButtonDummy.Visibility = Visibility.Visible;
            }
            else
            {
                PlayAgainButton.Visibility = Visibility.Visible;
                BackHome.Visibility = Visibility.Visible;
                BackHomeDummy.Visibility = Visibility.Collapsed;
                PlayAgainButtonDummy.Visibility = Visibility.Collapsed;
            }
        }
    }
}