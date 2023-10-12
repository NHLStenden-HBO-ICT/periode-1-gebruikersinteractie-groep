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
    // Player and movement control/variables
    private int _playerUpVelocity = 0;
    private int _velocityCap = 1200;
    private int _playerAcceleration = 150;
    private int _gravity = 70;
    private bool _playerRising;

    private double _bottomLevel = 0;
    private double _ceilingLevel = 0;

    private int _playerScore = 0;

    private List<CanvasLane> _canvasLanes = new List<CanvasLane>();
    private CanvasLane? lastLane;
    private CanvasEntities CanvasEntities;

    // Player style
    private ImageBrush playerSkin = new ImageBrush();

    public GameWindow()
    {
        InitializeComponent();

        GameCanvas.Focus(); // Makes keyboard event work
        CanvasEntities = new CanvasEntities(GameCanvas);

        // Create canvas lanes

        _canvasLanes.Add(new CanvasLane(150));
        _canvasLanes.Add(new CanvasLane(350));
        _canvasLanes.Add(new CanvasLane(550));

        // Set bottom and ceiling level
        this._bottomLevel = 60;
        CanvasContainer.Loaded += (sender, args) => _ceilingLevel = CanvasContainer.ActualHeight;

        // Register the canvas listener to the global game timer.
        App.GameTimer.AddListener("canvasListener", CanvasTick);
        App.GameTimer.AddListener("highscoreListener", HighscoreTick);
        App.GameTimer.AddListener("entitiesListener", EntitiesTick);

        // Set player position
        Canvas.SetLeft(Player, Math.Round(SystemParameters.FullPrimaryScreenWidth / 10) * 2);
        Canvas.SetBottom(Player, _bottomLevel);

        // Load playerskin into player rectangle
        playerSkin.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/wasmachine.png"));
        Player.Width = playerSkin.ImageSource.Width;
        Player.Height = playerSkin.ImageSource.Height;
        Player.Fill = playerSkin;

        App.PlayerRectangle = Player;

        // CanvasEntities.AddEntity(new DirtyClothes(1200, 60));
    }

    private double _playerScoreTracker = 0;

    private void HighscoreTick(object? sender, EventArgs e)
    {
        _playerScoreTracker += App.GameTimer.DeltaTime;

        // Check if one second has elapsed
        if (_playerScoreTracker > 0.5)
        {
            _playerScoreTracker = 0;
            _playerScore++;
            ScoreTextBlock.Text = _playerScore.ToString();

            CanvasEntities.AddEntity(new DirtyClothes(2000, GetRandomCanvasLane().GetLanePosition()));
        }
    }

    private void CanvasTick(object? sender, EventArgs e)
    {
        if (_playerRising) _playerUpVelocity += _playerAcceleration;

        // Ensure player cannot go faster 
        if (_playerUpVelocity > _velocityCap) _playerUpVelocity = _velocityCap;

        // Apply gravity to user when not on/under the ground
        // if (Canvas.GetBottom(Player) > 0) 
        _playerUpVelocity -= _gravity;

        // Predict if the player is going to hit the ground, acts as ground collision detection
        if (_playerUpVelocity < 0)
        {
            int currentPosition = (int)Canvas.GetBottom(Player);
            double predictedDownPosition = currentPosition - (-_playerUpVelocity * App.GameTimer.DeltaTime);
            if (predictedDownPosition < _bottomLevel)
            {
                _playerUpVelocity = 0;
                Canvas.SetBottom(Player, _bottomLevel);
            }
        }

        // Predict if the player is going to hit the ceiling, acts as ceiling collision
        if (_playerUpVelocity > 0)
        {
            int currentPosition = (int)Canvas.GetBottom(Player) + (int)Player.Height;
            double predictedUpPosition = currentPosition + (_playerUpVelocity * App.GameTimer.DeltaTime);
            if (predictedUpPosition > _ceilingLevel)
            {
                _playerUpVelocity = 0;
                Canvas.SetBottom(Player, _ceilingLevel - Player.Height);
            }
        }

        // Apply velocity to player
        Canvas.SetBottom(Player, Canvas.GetBottom(Player) + (_playerUpVelocity * App.GameTimer.DeltaTime));
    }

    private void EntitiesTick(object? sender, EventArgs e)
    {
        foreach (CanvasEntity entity in CanvasEntities.GetCanvasEntities().ToArray())
        {
            entity.EntityTick();

            if (Helpers.CollidesWithPlayer(entity.GetEntityRectangle()))
            {
                CanvasEntities.RemoveEntity(entity);
                Console.WriteLine("NO!");
            }
        }
    }

    private void CanvasKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Space) _playerRising = true;

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
        if (e.Key == Key.Space) _playerRising = false;
    }

    private CanvasLane GetRandomCanvasLane()
    {
        if (_canvasLanes.Count == 1) return _canvasLanes[0];

        Random random = new Random();

        int num = random.Next(0, _canvasLanes.Count);
        Console.WriteLine(num + ":" + _canvasLanes.Count);
        CanvasLane randomLane = _canvasLanes[random.Next(0, _canvasLanes.Count)];

        // Recurse function until another random lane has been found
        if (lastLane != null && randomLane == lastLane) randomLane = GetRandomCanvasLane();

        // Set last lane to current selected name
        lastLane = randomLane;
        return randomLane;
    }
}