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
using System.Windows.Shapes;

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

        private void PlayAgain_Click(object sender, RoutedEventArgs e)
        {
            Helpers.OpenWindow(new GameWindow());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
