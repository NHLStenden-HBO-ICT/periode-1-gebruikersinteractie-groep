using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for ParentalControl.xaml
    /// </summary>
    public partial class ParentalControl : Window
    {
        public int slidervalue;
        public ParentalControl()
        {
            int maxPlayTime = App.GameState.MaxplayTime /60;
            bool playtimeControl = App.GameState.PlaytimeControl;

            InitializeComponent();
            TimeSlider.Value = maxPlayTime;
            ToggleButton.IsChecked = playtimeControl;

            SaveSettings();
            changetext();




        }

       

        public void changetext()
        {
            limit.Text = "Limiet: " + App.GameState.MaxplayTime/60 + " Minuten";
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

            App.GameState.MaxplayTime = (int) TimeSlider.Value *60;
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
    }
}
