﻿using System.Windows.Controls;
using Wasmachine9000.Game.CanvasObject;

namespace Wasmachine9000.Game;

public class GameInformation
{
    public CanvasEntity Player;
    public Canvas GameCanvas;

    // Bounding boxes
    public double FloorLevel = 0;
    public double CeilingLevel = 0;

    // General information 
    public int GameSpeed = 100;
    public int VelocityCap = 1200;
    public int PlayerAcceleration = 150;
    public int Gravity = 70;

    // Scores, coins etc.
    public int PlayerScore = 0;
    public int PlayerCoins = 0;

    public GameInformation()
    {
    }
}