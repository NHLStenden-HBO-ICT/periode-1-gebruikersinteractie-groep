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

        private readonly SoundPlayer player;

        private AudioPlayer()
        {
            player = new SoundPlayer();
            player.SoundLocation = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets\\Audio\\Music\\Menu theme.wav");
            player.Load();
            player.PlayLooping();
        }

        public void Mute()
        {
            player.Stop();
        }

        public void Unmute()
        {
            player.PlayLooping();
        }
    }
}
