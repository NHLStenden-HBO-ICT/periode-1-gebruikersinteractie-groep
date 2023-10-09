using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Wasmachine9000
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
           
            // sets WPS height and width to the same height and width as the primary display
            this.Height = SystemParameters.FullPrimaryScreenHeight;
            this.Width = SystemParameters.FullPrimaryScreenWidth;

        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            Instellingen window = new Instellingen();
            window.Show();
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Winkel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Oudermenu_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Start_MouseLeave(object sender, MouseEventArgs e)
        {

        }

        private void Start_MouseEnter(object sender, MouseEventArgs e)
        {

        }
    }
}
