using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Wasmachine9000.Game.CanvasObject;
using Wasmachine9000.Game.Entities;

namespace Wasmachine9000.Windows;

public partial class GameWindow : Window
{
    // public static variables needed by other parts of the game
    
    
    // Player and movement control/variables
    // private int _velocityCap = 1200;
    // private int _playerAcceleration = 150;
    // private int _gravity = 70;
    //
    // private double _bottomLevel = 0;
    // private double _ceilingLevel = 0;
    //
    // private int _playerScore = 0;

    private List<CanvasLane> _canvasLanes = new List<CanvasLane>();
    private CanvasLane? _lastLane;

    // Player
    private PlayerEntity _playerEntity;

    public GameWindow()
    {
        InitializeComponent();
        App.GameInfo.GameCanvas = GameCanvas;

        // Set bottom and ceiling level
        App.GameInfo.FloorLevel = 60;
        CanvasContainer.Loaded += (sender, args) => App.GameInfo.CeilingLevel = CanvasContainer.ActualHeight;

        GameCanvas.Focus(); // Makes keyboard event work
        
        // Create canvas lanes
        _canvasLanes.Add(new CanvasLane(150));
        _canvasLanes.Add(new CanvasLane(350));
        _canvasLanes.Add(new CanvasLane(550));

        // Register the canvas listener to the global game timer.
        App.GameTimer.AddListener("canvasListener", CanvasTick);
        App.GameTimer.AddListener("highscoreListener", HighscoreTick);
        App.GameTimer.AddListener("entitiesListener", EntitiesTick);

        // Set player position and adds it to the entities list
        _playerEntity = new PlayerEntity((int)Math.Round(SystemParameters.FullPrimaryScreenWidth / 10) * 2,
            (int) App.GameInfo.FloorLevel);
        App.GameInfo.Player = _playerEntity;
        App.GameInfo.CanvasEntities.AddEntity(_playerEntity);
        
        
    }

    private double _playerScoreTracker = 0;

    private void HighscoreTick(object? sender, EventArgs e)
    {
        _playerScoreTracker += App.GameTimer.DeltaTime;

        // Check if one second has elapsed
        if (_playerScoreTracker > 0.6)
        {
            _playerScoreTracker = 0;
            App.GameInfo.PlayerScore++;
            ScoreTextBlock.Text = App.GameInfo.PlayerScore.ToString();

            Random random = new Random();
            if (random.Next(0, 5) == 4)
            {
                App.GameInfo.CanvasEntities.AddEntity(new DirtyClothes(2000, GetRandomCanvasLane().GetLanePosition()));
                App.GameInfo.CanvasEntities.AddEntity(new DirtyClothes(2000, GetRandomCanvasLane().GetLanePosition()));
            }
            else
            {
                App.GameInfo.CanvasEntities.AddEntity(new DirtyClothes(2000, GetRandomCanvasLane().GetLanePosition()));
            }

            if (random.Next(0, 10) == 7)
            {
                App.GameInfo.CanvasEntities.AddEntity(new RocketPants(2000, (int) _playerEntity.GetY()));
            }
        }
    }

    private void CanvasTick(object? sender, EventArgs e)
    {
        
    }

    private void EntitiesTick(object? sender, EventArgs e)
    {
        // 'Coppies' canvas entities array so it can be modified whilst being looped over.
        foreach (CanvasEntity entity in App.GameInfo.CanvasEntities.GetCanvasEntities().ToArray())
        {
            // Check if entity is out of bounds
            if (entity.GetX() + entity.GetEntityRectangle().Width < 0)
            {
                App.GameInfo.CanvasEntities.RemoveEntity(entity);
                return;
            }

            // Process entity tick
            entity.EntityTick();

            // Destroy entity when collided with player
            if (entity is not PlayerEntity && Helpers.CollidesWithPlayer(entity.GetEntityRectangle()))
            {
                App.GameInfo.CanvasEntities.RemoveEntity(entity);
                App.GameInfo.PlayerScore = 0;
            };
        }
    }

    private void CanvasKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Space) _playerEntity.SetPlayerRising(true);

        if (e.Key == Key.Escape)
        {
            App.GameTimer.RemoveListener("canvasListener");
            App.GameTimer.RemoveListener("highscoreListener");
            App.GameTimer.RemoveListener("entitiesListener");
            Helpers.OpenPreviousWindow();
        }
    }

    private void CanvasKeyUp(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Space) _playerEntity.SetPlayerRising(false);
    }

    private CanvasLane GetRandomCanvasLane()
    {
        // Dont need to randomize when theres only one lane
        if (_canvasLanes.Count == 1) return _canvasLanes[0];

        Random random = new Random();

        // Choose random canvas lane
        int num = random.Next(0, _canvasLanes.Count);
        CanvasLane randomLane = _canvasLanes[random.Next(0, _canvasLanes.Count)];

        // Recurse function until another random lane has been found
        if (_lastLane != null && randomLane == _lastLane) randomLane = GetRandomCanvasLane();

        // Set last lane to current selected name
        _lastLane = randomLane;
        return randomLane;
    }
}