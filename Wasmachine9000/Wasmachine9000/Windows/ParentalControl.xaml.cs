using System;
using System.Windows;
using System.Windows.Input;

namespace Wasmachine9000.Windows
{
    /// <summary>
    /// Interaction logic for ParentalControl.xaml
    /// </summary>
    public partial class ParentalControl : Window
    {
        public ParentalControl()
        {
            int maxPlayTime = App.GameState.MaxplayTime / 60;
            bool playtimeControl = App.GameState.PlaytimeControl;

            InitializeComponent();
            TimeSlider.Value = maxPlayTime;
            ToggleButton.IsChecked = playtimeControl;

            SaveSettings();
            changetext();

            if (App.GameState.GetPincode() != 0)
            {
                PincodeInstructie.Text =
                    "Voor een nieuwe pincoe in en druk op `opslaan` om een nieuwe pincode in te stellen";
                PincodeInstructie.FontSize = 20;
            }
        }


        public void changetext()
        {
            limit.Text = "Limiet: " + App.GameState.MaxplayTime / 60 + " Minuten";
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                SaveSettings();
                Helpers.OpenWindow(new MainWindow());
            }
        }


        private void Back_Click(object sender, RoutedEventArgs e)
        {
            SaveSettings();
            Helpers.OpenWindow(new MainWindow());
        }

        private void SaveSettings()
        {
            App.GameState.MaxplayTime = (int)TimeSlider.Value * 60;
            App.GameState.PlaytimeControl = (bool)ToggleButton.IsChecked;
            App.GameState.SaveGameState();
        }


        private void TimeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double newValue = e.NewValue;
            double oldValue = e.OldValue;

            // Check if the value actually changed
            if (newValue != oldValue)
            {
                SaveSettings();
                changetext();
            }
        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            SaveSettings();
            changetext();
        }

        private void NumericOnly(object sender, TextCompositionEventArgs e)
        {
            if (IsTextNumeric(e.Text))
            {
                e.Handled = true;
            }

            if (PincodeInput.Text.Length >= 4)
            {
                e.Handled = true;
            }
        }

        private static bool IsTextNumeric(string str)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex("[^1-9]");
            return reg.IsMatch(str);
        }

        private void SavePincodeButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (PincodeInput.Text.Trim().Length != 4)
            {
                PincodeInstructie.Text = "Pincode moet uit 4 cijfers bestaan.";
                PincodeInstructie.FontSize = 35;
                return;
            }

            App.GameState.SetPincode(Convert.ToInt32(PincodeInput.Text));
            PincodeInstructie.Text = "Nieuwe pincode is ingesteld.";
            PincodeInstructie.FontSize = 40;
        }

        private void Back_OnClick(object sender, RoutedEventArgs e)
        {
            Helpers.OpenWindow(new MainWindow());
        }
    }
}