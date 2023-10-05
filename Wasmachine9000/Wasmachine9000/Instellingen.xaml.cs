using System;
using System.Windows;
using System.Windows.Controls;
using NAudio.Wave;

namespace Wasmachine9000
{
    public partial class Instellingen : Window
    {
        private WaveOutEvent waveOut;
        private AudioFileReader audioFile = new AudioFileReader("C:\\HBO-ICT\\Periode 1\\Project 1 wasmachine9000\\wasmachine9000\\Wasmachine9000\\Wasmachine9000\\vine-boom.mp3");
        public Instellingen()
        {
            InitializeComponent();
            waveOut = new WaveOutEvent();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // Adjust the volume based on the slider's value
            waveOut.Volume = (float)(e.NewValue / 100.0); // Assuming the slider range is from 0 to 100
        }

        // Method to play audio

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            {
                try
                {
                    if (waveOut.PlaybackState == PlaybackState.Playing)
                    {
                        // If audio is already playing, stop it and dispose
                        waveOut.Stop();
                        waveOut.Dispose();
                    }

                    // Load and play the audio file
                    audioFile = new AudioFileReader("C:\\HBO-ICT\\Periode 1\\Project 1 wasmachine9000\\wasmachine9000\\Wasmachine9000\\Wasmachine9000\\vine-boom.mp3"); // Replace with your audio file path
                    waveOut.Init(audioFile);
                    waveOut.Play();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error playing audio: " + ex.Message);
                }
            }

        }
    }
}

