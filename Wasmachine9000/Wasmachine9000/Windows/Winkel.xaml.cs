using System.Windows;
using System.Windows.Input;

namespace Wasmachine9000.Windows
{
    /// <summary>
    /// Interaction logic for Winkel.xaml
    /// </summary>
    public partial class Winkel : Window
    {
        public Winkel()
        {
            InitializeComponent();
            // sets WPS height and width to the same height and width as the primary display
            this.Height = SystemParameters.FullPrimaryScreenHeight;
            this.Width = SystemParameters.FullPrimaryScreenWidth;
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Helpers.OpenPreviousWindow();
            }
        }
    }
}