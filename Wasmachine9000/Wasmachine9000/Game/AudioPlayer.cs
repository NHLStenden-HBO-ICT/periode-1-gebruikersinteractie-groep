using System;
using System.IO;
using System.Windows.Media;

namespace Wasmachine9000.Game
{
    public class AudioPlayer
    {
        public static MediaPlayer MusicPlayer = new MediaPlayer();
        public static MediaPlayer SFXPlayer = new MediaPlayer();

        public AudioPlayer()
        {
            MusicPlayer.MediaEnded += (sender, args) => { MusicPlayer.Position = TimeSpan.Zero; };
        }

        public void StartMusic()
        {
            // Load and play the default audio file during initialization
            LoadAndPlayMusic("Menu theme.wav");
        }

        public void StartSFX()
        {
            LoadAndPlaySFX("");
        }

        public void LoadAndPlayMusic(string fileName)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets\\Audio\\Music", fileName);

            MusicPlayer.Stop();
            MusicPlayer.Open(new Uri(filePath));
            MusicPlayer.Play();
        }

        public void LoadAndPlaySFX(string fileName)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets\\Audio\\Music", fileName);

            SFXPlayer.Stop();
            SFXPlayer.Open(new Uri(filePath));
            SFXPlayer.Play();
        }

        public void SetMusicVolume(double MusicVolume)
        {
            MusicPlayer.Volume = MusicVolume;
        }

        public void SetSFXVolume(double SFXVolume)
        {
            SFXPlayer.Volume = SFXVolume;
        }

        public void StopMusic()
        {
            if (MusicPlayer != null)
            {
                MusicPlayer.Stop();
            }
        }

        public void StopSFX()
        {
            if (SFXPlayer != null)
            {
                SFXPlayer.Stop();
            }
        }

        public void Mute()
        {
            StopMusic();
        }

        public void Unmute()
        {
            // Play the loaded audio file if it exists
            if (MusicPlayer != null)
            {
                MusicPlayer.Play();
            }
        }

       
    }
}