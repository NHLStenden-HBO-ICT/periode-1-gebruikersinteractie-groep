using System;
using System.Media;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Policy;
using System.Windows;
using System.IO;


namespace Wasmachine9000.Game
{
    public class AudioPlayer
    {
        private static readonly Lazy<AudioPlayer> lazy = new Lazy<AudioPlayer>(() => new AudioPlayer());

        public static AudioPlayer Instance => lazy.Value;

        private SoundPlayer player;

        private AudioPlayer()
        {
            // Load and play the default audio file during initialization
            LoadAndPlayAudio("Menu theme.wav");
        }

        public void LoadAndPlayAudio(string fileName)
        {
            // Stop the current audio playback
            Stop();

            // Create a new SoundPlayer instance with the specified audio file
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets\\Audio\\Music", fileName);
            player = new SoundPlayer(filePath);

            // Load and play the new audio file
            player.Load();
            player.PlayLooping();
        }

        public void Stop()
        {
            if (player != null)
            {
                player.Stop();
                player.Dispose();
                player = null;
            }
        }

        public void Mute()
        {
            Stop();
        }

        public void Unmute()
        {
            // Play the loaded audio file if it exists
            if (player != null)
            {
                player.PlayLooping();
            }
        }
    }
}
