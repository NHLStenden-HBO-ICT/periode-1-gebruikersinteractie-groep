using System;
using System.IO;
using System.Windows.Media;

namespace Wasmachine9000.Game
{
    public class AudioPlayer
    {
        public MediaPlayer MusicPlayer = new MediaPlayer();
        public MediaPlayer SFXPlayer = new MediaPlayer();

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
            LoadAndPlaySFX("Game over.wav");
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

        private void MusicPlayer_MediaEnded(object sender, EventArgs e)
        {
            // When the media ends, set the position to the beginning to repeat it
            MusicPlayer.Position = TimeSpan.Zero;
        }
    }
}