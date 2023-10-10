using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
            // Store currently open window in variable
            Window currentWindow = App.GameState.CurrentWindow;
            
            // Show new window and set it as current window in GameState
            targetWindow.Show();
            App.GameState.CurrentWindow = targetWindow;
            

            // Set previous window in GameState to previously closed window and close window
            currentWindow.Hide();
            if(App.GameState.PreviousWindow!= null)
            {
                App.GameState.PreviousWindow.Close();
            }
      
            App.GameState.PreviousWindow = currentWindow;



        }
        public static void OpenPreviousWindow()
        {
            OpenWindow(App.GameState.PreviousWindow);
        }
    }
}
