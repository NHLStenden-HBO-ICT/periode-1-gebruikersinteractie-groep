using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using Wasmachine9000.Windows;

namespace Wasmachine9000
{
    internal class Helpers
    {
        public static void OpenWindow(Window targetWindow)
        {
            // Store currently open window in variable
            Window currentWindow = App.GameState.CurrentWindow;

            // Show new window and set it as current window in GameState
            targetWindow.Show();
            App.GameState.CurrentWindow = targetWindow;

            ChangeMusic(targetWindow, App.GameState.PreviousWindow);
            // Set previous window in GameState to previously closed window and close window
            currentWindow.Hide();
            if (App.GameState.PreviousWindow != null)
            {
                App.GameState.PreviousWindow.Close();
            }

            App.GameState.PreviousWindow = currentWindow;


          
        }

        public static void OpenPreviousWindow()
        {
            Window targetWindow = App.GameState.PreviousWindow;
            Window currentWindow = App.GameState.CurrentWindow;
            ChangeMusic(targetWindow, App.GameState.CurrentWindow);

            targetWindow.Show();
            currentWindow.Hide();

            App.GameState.PreviousWindow = currentWindow;
            App.GameState.CurrentWindow = targetWindow;
        }

        public static void ChangeMusic(Window targetWindow, Window previousWindow)
        {
            switch (targetWindow)
            {
                case MainWindow:
                    if (previousWindow is GameWindow) App.AudioPlayer.LoadAndPlayMusic("Menu theme.wav");
                    break;

                case GameWindow:
                    App.AudioPlayer.LoadAndPlayMusic("Ingame.wav");
                    break;
            }
        }

        public static string GetSpriteResource(string filename)
        {
            return "pack://application:,,,/Assets/Sprites/Entities/" + filename;
        }

        public static string GetBackgroundResource(string filename)
        {
            return "pack://application:,,,/Assets/Background/" + filename;
        }

        public static bool CollidesWith(Rectangle source, Rectangle target)
        {
            Rect sourceRect = new Rect(Canvas.GetLeft(source), Canvas.GetBottom(source), source.Width, source.Height);
            Rect targetRect = new Rect(Canvas.GetLeft(target), Canvas.GetBottom(target), target.Width, target.Height);

            return sourceRect.IntersectsWith(targetRect);
        }

        public static bool CollidesWithPlayer(Rectangle source)
        {
            return CollidesWith(source, App.GameInfo.Player.GetEntityRectangle());
        }

       
    }
}