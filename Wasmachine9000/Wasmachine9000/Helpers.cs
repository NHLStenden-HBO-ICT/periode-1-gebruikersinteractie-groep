using System;
using System.IO;
using System.Windows;
using Wasmachine9000.Game;
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

            changemusic(targetWindow, App.GameState.PreviousWindow);
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
            changemusic(targetWindow, App.GameState.CurrentWindow);

            targetWindow.Show();
            currentWindow.Hide();

            App.GameState.PreviousWindow = currentWindow;
            App.GameState.CurrentWindow = targetWindow;
        }
        public static void changemusic(Window targetWindow, Window previouswindow)
        {

            
                switch (targetWindow)
                {

                    case MainWindow:
                    if(previouswindow is GameWindow)
                    {
                        App.audioPlayer.LoadAndPlayAudio("Menu theme.wav");
                    }
                        
                        break;

                    case GameWindow:
                        App.audioPlayer.LoadAndPlayAudio("Ingame.wav");
                        break;

                }
           

        }

      
    }
}