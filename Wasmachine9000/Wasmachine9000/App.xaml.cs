using System.Windows;
using System.Windows.Shapes;
using Wasmachine9000.Game;

namespace Wasmachine9000
{
    public partial class App : Application
    {
        public static GameState GameState = new GameState();
        public static Scoreboard Scoreboard = new Scoreboard();
        public static AudioPlayer AudioPlayer = new AudioPlayer();
        public static GameTimer GameTimer = new GameTimer();

        public static Rectangle? PlayerRectangle;

        public App()
        {
            GameState = GameState.LoadGameState();

            // Start the music player
            AudioPlayer.Start();

            // Start menu screen and set GameState to it
            Window menuScreen = new MainWindow();
            GameState.CurrentWindow = menuScreen;
            menuScreen.Show();
        }
    }
}