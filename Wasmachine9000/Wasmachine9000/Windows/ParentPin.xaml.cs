using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Wasmachine9000
{
    /// <summary>
    /// Interaction logic for ParentPin.xaml
    /// </summary>
    public partial class ParentPin : Window
    {
        public ParentPin()
        {
            InitializeComponent();
            // sets WPS height and width to the same height and width as the primary display
            this.Height = SystemParameters.FullPrimaryScreenHeight;
            this.Width = SystemParameters.FullPrimaryScreenWidth;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Helpers.OpenWindow(new MainWindow());
        }

        private void Doorgaan_Click(object sender, RoutedEventArgs e)
        {
            string password = Pincode.Password;
            if (password == "1234")
            {
                MessageBoxResult boodPass = MessageBox.Show("Jup");
            }
            else
            {
                MessageBoxResult badPass = MessageBox.Show("Fout");
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Escape)
            {
                Helpers.OpenPreviousWindow();
            }
        }

        private void CodeButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the clicked button's content (the digit, for example "1")
            Button button = (Button)sender;
            string name = button.Name.ToString();
            string pinNumber = Regex.Replace(name, "[^0-9]", "");

            // Add the digit to the PasswordBox
            PasswordBox passwordBox = Pincode;
            passwordBox.Password += pinNumber;

        }
    }
}
