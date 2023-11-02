using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Wasmachine9000.Windows;

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
            Helpers.OpenPreviousWindow();
        }

        private void Doorgaan_Click(object sender, RoutedEventArgs e)
        {
            if (Pincode.Password == App.GameState.GetPincode().ToString())
            {
                Helpers.OpenWindow(new ParentalControl());
            }
            else
            {
                MessageBoxResult badPass = MessageBox.Show("Onjuiste Pincode");
                Pincode.Password = "";
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
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