using System;
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
        public GameState GameState;

        public App()
        {
            this.GameState = GameState.LoadGameState();
            

        }
    }
}
