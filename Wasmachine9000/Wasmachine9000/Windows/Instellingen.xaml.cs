using System;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xaml;
using Wasmachine9000.Game;

namespace Wasmachine9000
{
    public partial class Instellingen : Window
    {
        public bool SoundIsOn = true;
        public SolidColorBrush backgroundRed = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ff7200"));
        public SolidColorBrush backgroundBlue = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00c0ff"));
       
       
        public Instellingen()
        {
            InitializeComponent();
            Ellipse musicToggleEllipse = (Ellipse)MusicToggle.Template.FindName("MusicToggleEllipse", MusicToggle);
            Ellipse MainToggleEllipse = (Ellipse)MainToggle.Template.FindName("MainToggleEllipse", MainToggle);
            Ellipse SFXToggleEllipse = (Ellipse)SFXToggle.Template.FindName("SFXToggleEllipse", SFXToggle);
            App.GameState.SFXSound = false;
            

            Loaded += Window_loaded;
            // sets WPS height and width to the same height and width as the primary display
            this.Height = SystemParameters.FullPrimaryScreenHeight;
            this.Width = SystemParameters.FullPrimaryScreenWidth;
           
        }

        public void Window_loaded(object sender, RoutedEventArgs e)
        {
            Ellipse musicToggleEllipse = (Ellipse)MusicToggle.Template.FindName("MusicToggleEllipse", MusicToggle);
            Ellipse MainToggleEllipse = (Ellipse)MainToggle.Template.FindName("MainToggleEllipse", MainToggle);
            Ellipse SFXToggleEllipse = (Ellipse)SFXToggle.Template.FindName("SFXToggleEllipse", SFXToggle);

            if (App.GameState.MusicSound == true)
            {
                musicToggleEllipse.Fill = backgroundBlue;
            }
            else
            {
                musicToggleEllipse.Fill = backgroundRed;
            }
            if (App.GameState.SFXSound == true)
            {
                SFXToggleEllipse.Fill = backgroundBlue;
            }
            else
            {
                SFXToggleEllipse.Fill = backgroundRed;
            }
            if (App.GameState.MusicSound == true && App.GameState.SFXSound == true)
            {
                MainToggleEllipse.Fill = backgroundBlue;
            }
            else if(App.GameState.MusicSound == false && App.GameState.SFXSound == false)
            {
                MainToggleEllipse.Fill = backgroundRed;
            }
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // Adjust the volume based on the slider's value

        }
        // Method to play audio

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           

        }
        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Helpers.OpenPreviousWindow();
            }
        }
        private void MusicToggle_Click(object sender, RoutedEventArgs e)
        {
            Ellipse musicToggleEllipse = (Ellipse)MusicToggle.Template.FindName("MusicToggleEllipse", MusicToggle);
            Ellipse MainToggleEllipse = (Ellipse)MainToggle.Template.FindName("MainToggleEllipse", MainToggle);
            Ellipse SFXToggleEllipse = (Ellipse)SFXToggle.Template.FindName("SFXToggleEllipse", SFXToggle);
         
                if (App.GameState.MusicSound == true) 
                {
                    App.AudioPlayer.StopMusic();
                    App.GameState.MusicSound = false;
                    musicToggleEllipse.Fill = backgroundRed; // Change the Ellipse color to red
                    if(App.GameState.MusicSound == false && App.GameState.SFXSound == false)
                    {
                        MainToggleEllipse.Fill = backgroundRed;
                    }
                    else
                    {
                        MainToggleEllipse.Fill = backgroundBlue;
                    }
                }
                else
                {
                    App.AudioPlayer.StartMusic();
                    App.GameState.MusicSound = true;
                    musicToggleEllipse.Fill = backgroundBlue; // Change the Ellipse color to blue
                    if(App.GameState.SFXSound == true && App.GameState.MusicSound == true)
                    {
                        MainToggleEllipse.Fill = backgroundBlue;
                    }
                }        
            if (musicToggleEllipse.Fill == backgroundRed && SFXToggleEllipse.Fill == backgroundRed)
            {
                MainToggleEllipse.Fill = backgroundRed;
            }
        }

        private void SFXToggle_Click(object sender, RoutedEventArgs e)
        {
            Ellipse musicToggleEllipse = (Ellipse)MusicToggle.Template.FindName("MusicToggleEllipse", MusicToggle);
            Ellipse MainToggleEllipse = (Ellipse)MainToggle.Template.FindName("MainToggleEllipse", MainToggle);
            Ellipse SFXToggleEllipse = (Ellipse)SFXToggle.Template.FindName("SFXToggleEllipse", SFXToggle);
            if (App.GameState.SFXSound != null)
            {
                if (App.GameState.SFXSound)
                {
                    App.AudioPlayer.StopSFX();
                    App.GameState.SFXSound = false;
                    SFXToggleEllipse.Fill = backgroundRed;
                    if (App.GameState.SFXSound == false && App.GameState.MusicSound == false)
                    {
                        MainToggleEllipse.Fill = backgroundRed;
                    }
                    else
                    {
                        MainToggleEllipse.Fill = backgroundBlue;
                    }
                }
                else
                {
                    App.AudioPlayer.StartSFX();
                    App.GameState.SFXSound = true;
                    Debug.WriteLine(App.GameState.SFXSound);
                    SFXToggleEllipse.Fill = backgroundBlue;
                    if (App.GameState.SFXSound == true || App.GameState.MusicSound == true)
                    {
                        MainToggleEllipse.Fill = backgroundBlue;
                    }
                }
            }
        }

        private void MainToggle_Click(object sender, RoutedEventArgs e)
        {
            Ellipse musicToggleEllipse = (Ellipse)MusicToggle.Template.FindName("MusicToggleEllipse", MusicToggle);
            Ellipse MainToggleEllipse = (Ellipse)MainToggle.Template.FindName("MainToggleEllipse", MainToggle);
            Ellipse SFXToggleEllipse = (Ellipse)SFXToggle.Template.FindName("SFXToggleEllipse", SFXToggle);
           
                if (App.GameState.MusicSound == true || App.GameState.SFXSound == true)
                {
                    App.AudioPlayer.StopMusic();
                    App.AudioPlayer.StopSFX();
                    App.GameState.SFXSound = false;
                App.GameState.MusicSound = false;
                    MainToggleEllipse.Fill = backgroundRed; // Change the Ellipse color to red
                    musicToggleEllipse.Fill = backgroundRed;
                    SFXToggleEllipse.Fill = backgroundRed;
                }
                else
                {              
                    App.AudioPlayer.StartMusic();
                    App.AudioPlayer.StartSFX();
                App.GameState.MusicSound = true;
                App.GameState.SFXSound = true;
                    MainToggleEllipse.Fill = backgroundBlue; // Change the Ellipse color to blue
                    musicToggleEllipse.Fill = backgroundBlue;
                    SFXToggleEllipse.Fill = backgroundBlue;
                } 
        }
        }
}


