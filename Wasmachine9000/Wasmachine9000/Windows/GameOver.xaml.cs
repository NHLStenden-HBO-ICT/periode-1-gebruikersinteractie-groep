using System;
using System.Collections.Generic;
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
        }

        private void MainWindow_Click(object sender, RoutedEventArgs e)
        {
            Helpers.OpenWindow(new MainWindow());
        }

        //private void PlayAgain_Click(object sender, RoutedEventArgs e)
        //{
        //    Helpers.OpenWindow(new GameWindow());
        //}

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PlayAgain_Click(object sender, EventArgs e)
        {
            // Get the playername from the game over screen
            string playername = Playername.Text;

            // Call the PostScore method
            string username = playername;
            int score = 50000;
            App.Scoreboard.PostScore(username, score);

            Console.WriteLine("Score gepost!");


            var player = new GameWindow(); // Maak een instantie van de klasse waar _playerScore is gedefinieerd.
            string scoreString = player._playerScore.ToString(); // Roep de ToString-methode aan.

            //View score on the game over screen
            Score.Text = scoreString;
        }
    }
}
