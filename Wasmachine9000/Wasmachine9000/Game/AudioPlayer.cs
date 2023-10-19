using System;
using System.Media;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Policy;
using System.Windows;
using System.IO;
using System.Windows.Media;



namespace Wasmachine9000.Game 
{ 
 public class AudioPlayer
    {
        public MediaPlayer MusicPlayer;
        public MediaPlayer SFXPlayer;
       
        public void StartMusic()
        {
            // Load and play the default audio file during initialization
            LoadAndPlayAudio("Menu theme.wav");
        }
        public void StartSFX()
        {
            LoadAndPlayAudio2("Game over.wav");
        }

        public void LoadAndPlayAudio(string fileName)
        {
            // Stop the current audio playback
            

            // Create a new SoundPlayer instance with the specified audio file
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets\\Audio\\Music", fileName);
            MusicPlayer = new MediaPlayer();
            MusicPlayer.Open(new Uri(filePath));
            MusicPlayer.Play();
            MusicPlayer.MediaEnded += MusicPlayer_MediaEnded;
        }

        public void LoadAndPlayAudio2(string fileName2)
        {
            string filePath2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets\\Audio\\Music", fileName2);
            

            SFXPlayer = new MediaPlayer();
            SFXPlayer.Open(new Uri(filePath2));
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
