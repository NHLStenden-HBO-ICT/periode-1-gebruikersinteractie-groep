using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
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

            //View score on the game over screen
            ScoreTextBlock.Text = App.GameInfo.PlayerScore.ToString();
        }

        private void MainWindow_Click(object sender, RoutedEventArgs e)
        {
            // Get the playername from the game over screen
            string playername = Playername.Text;

            // Call the PostScore method
            string username = playername;
            int score = App.GameInfo.PlayerScore;
            App.Scoreboard.PostScore(username, score);

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

            Debug.WriteLine("Score gepost!");

            // Reset score
            App.GameInfo.Reset();
        }

        private void Playername_IsFilled(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Playername.Text))
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
