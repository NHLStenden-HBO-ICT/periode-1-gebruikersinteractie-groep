using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Wasmachine9000.Game;

namespace Wasmachine9000
{
    internal class Helpers
    {
        public static void OpenWindow (Window targetWindow)
        {

            //switch windows
            targetWindow.Show();
            App.GameState.CurrentWindow.Close();

            //store current and previous windows to variables
            Window previousWindow = App.GameState.PreviousWindow;
            Window currentWindow = App.GameState.CurrentWindow;

            //change GameState Windows
            App.GameState.CurrentWindow = previousWindow;
            App.GameState.PreviousWindow = currentWindow;

        }
        public static void OpenPreviousWindow()
        {
            //switch windows
            App.GameState.PreviousWindow.Show();
            App.GameState.CurrentWindow.Close();

            //store current and previous windows to variables
            Window previousWindow = App.GameState.PreviousWindow;
            Window currentWindow = App.GameState.CurrentWindow;

            //change GameState Windows
            App.GameState.CurrentWindow = previousWindow;
            App.GameState.PreviousWindow = currentWindow;
        }
    }
}
