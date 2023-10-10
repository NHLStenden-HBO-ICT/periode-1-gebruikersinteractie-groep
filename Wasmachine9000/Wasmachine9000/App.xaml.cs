﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Wasmachine9000.Game;

namespace Wasmachine9000
{
    public partial class App : Application
    {
        public static GameState GameState = new GameState();
        public static Scoreboard Scoreboard = new Scoreboard();
        private AudioPlayer audioPlayer;
        public App()
        {
            GameState = GameState.LoadGameState();
            audioPlayer = AudioPlayer.Instance;
            // Start menu screen and set GameState to it
            Window menuScreen = new MainWindow();
            GameState.CurrentWindow = menuScreen;
            menuScreen.Show();

        }
    }
}
