using System;
using System.Windows;
using System.Windows.Controls;


namespace Wasmachine9000
{
    public partial class Instellingen : Window
    {
        public Instellingen()
        {
            InitializeComponent();
            // sets WPS height and width to the same height and width as the primary display
            this.Height = SystemParameters.FullPrimaryScreenHeight;
            this.Width = SystemParameters.FullPrimaryScreenWidth;
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // Adjust the volume based on the slider's value

        }

        // Method to play audio

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           

        }
    }
}

