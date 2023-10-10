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
        private SoundPlayer _player;

        public AudioPlayer()
        {
            
        }

        public void Start()
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
            _player = new SoundPlayer(filePath);

            // Load and play the new audio file
            _player.Load();
            _player.PlayLooping();
        }

        public void Stop()
        {
            if (_player != null)
            {
                _player.Stop();
                _player.Dispose();
                _player = null;
            }
        }

        public void Mute()
        {
            Stop();
        }

        public void Unmute()
        {
            // Play the loaded audio file if it exists
            if (_player != null)
            {
                _player.PlayLooping();
            }
        }
    }
}
