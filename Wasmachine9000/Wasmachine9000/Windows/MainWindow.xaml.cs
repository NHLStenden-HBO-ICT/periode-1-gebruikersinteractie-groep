using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using Wasmachine9000.Game;
using Wasmachine9000.Windows;

namespace Wasmachine9000
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<ScoreboardItem> _scoreboard = new();
        public DispatcherTimer ScoreboardTimer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();

            // sets WPS height and width to the same height and width as the primary display
            this.Height = SystemParameters.FullPrimaryScreenHeight;
            this.Width = SystemParameters.FullPrimaryScreenWidth;

            _scoreboard = App.Scoreboard.GetScoreboard();
            UpdateScoreboard(_scoreboard);
            ScoreboardTimer.Interval= TimeSpan.FromSeconds(10);
            ScoreboardTimer.Tick += ScoreboardTimer_Tick;
            ScoreboardTimer.Start();    
        }

        private void ScoreboardTimer_Tick(object? sender, EventArgs e)
        {
            _scoreboard = App.Scoreboard.GetScoreboard();
            UpdateScoreboard(_scoreboard);
        }

        private void UpdateScoreboard(List<ScoreboardItem> scoreboard)
        {
            int scoreIndex = 1;

            // copy scorevoard
            List<UIElement> childrenCopy = new List<UIElement>(ScoreboardContainer.Children.OfType<UIElement>());


            // Clear all items on the grid except the title text
            foreach (UIElement child in childrenCopy)
            {
                if (child is not Border) ScoreboardContainer.Children.Remove(child);
            }

            foreach (ScoreboardItem item in scoreboard)
            {
                // <TextBlock Width="290" FontSize="30" Grid.Column="0" Grid.Row="1" FontFamily="Baloo Bhai 2 SemiBold" TextAlignment="Left" Padding="5,0,0,0" VerticalAlignment="Center"></TextBlock>
                TextBlock scoreboardBlock = new TextBlock
                {
                    Text = scoreIndex + ". " + item.username + ": " + item.score,
                    Width = 290,
                    FontSize = 30,
                    TextAlignment = TextAlignment.Left,
                    Padding = new Thickness(5, 0, 0, 0),
                    VerticalAlignment = VerticalAlignment.Center,
                    FontFamily = new FontFamily("Baloo Bhai 2 SemiBold")
                };

                Grid.SetColumn(scoreboardBlock, 0);
                Grid.SetRow(scoreboardBlock, scoreIndex);

                // scoreboardBlock.FontFamily = "Baloo Bhai 2 SemiBold";
                ScoreboardContainer.Children.Add(scoreboardBlock);
                scoreIndex++;
            }
        }


        #region Event handlers

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            Helpers.OpenWindow(new Instellingen());
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            if (App.GameState.PlayLockedUntil > DateTime.Now && App.GameState.PlaytimeControl)
            {
                MessageBox.Show("Je mag niet spelen :(. Je moet wachten tot " + App.GameState.PlayLockedUntil);
                return;
            }

            Helpers.OpenWindow(new GameWindow());
        }

        private void Winkel_Click(object sender, RoutedEventArgs e)
        {
            //open a new Winkel window and close the current window
            Helpers.OpenWindow(new Winkel());
        }

        private void Oudermenu_Click(object sender, RoutedEventArgs e)
        {
            if (App.GameState.GetPincode() == 0)
            {
                Helpers.OpenWindow(new ParentalControl());
                return;
            }

            Helpers.OpenWindow(new ParentPin());
        }

        private void Start_MouseLeave(object sender, MouseEventArgs e)
        {
        }

        private void Start_MouseEnter(object sender, MouseEventArgs e)
        {
        }

        #endregion

        public void OpenWindow(Window window)
        {
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            #region close application + messagebox

            // Check if the "Escape" key is pressed (Key.Escape)
            if (e.Key == Key.Escape)
            {
                // Display a confirmation dialog
                MessageBoxResult result = MessageBox.Show("Weet je zeker dat je de game wilt sluiten?",
                    "Bevestig Afsluiten", MessageBoxButton.YesNo, MessageBoxImage.Question);

                // If the user clicks "Yes," close the application
                if (result == MessageBoxResult.Yes)
                {
                    Application.Current.Shutdown();
                }
            }

            #endregion
        }
    }
}