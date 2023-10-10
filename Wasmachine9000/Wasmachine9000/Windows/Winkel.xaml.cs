using System.Windows;

namespace Wasmachine9000
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
    }
}