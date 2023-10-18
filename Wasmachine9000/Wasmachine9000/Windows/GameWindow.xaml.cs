﻿using System;
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
    private double _backgroundTracker;

    private readonly List<CanvasLane> _canvasLanes = new();
    private CanvasLane? _lastLane;

    // Player
    private readonly PlayerEntity _playerEntity;

    private double _playerScoreTracker;

    // Background one and two images
    private readonly ImageBrush BackgroundImageOne = new();
    private readonly ImageBrush BackgroundImageTwo = new();


    public GameWindow()
    {
        InitializeComponent();
        App.GameInfo.GameCanvas = GameCanvas;
        App.GameInfo.PlayerLives = 3;

        // Set bottom and ceiling level
        App.GameInfo.FloorLevel = 49;
        CanvasContainer.Loaded += (sender, args) =>
        {
            App.GameInfo.CeilingLevel = CanvasContainer.ActualHeight;

            // Create canvas lanes
            int canvasLaneAmount = 5;
            for (int i = 0; i < canvasLaneAmount; i++)
            {
                _canvasLanes.Add(new CanvasLane((int)(GameCanvas.ActualHeight / (canvasLaneAmount + 2)) * (i + 1) + 30));
            }
        };

        GameCanvas.Focus(); // Makes keyboard event work

        // Register the canvas listener to the global game timer.
        App.GameTimer.AddListener("canvasListener", CanvasTick);
        App.GameTimer.AddListener("highscoreListener", HighscoreTick);
        App.GameTimer.AddListener("entitiesListener", EntitiesTick);

        // Set player position and adds it to the entities list
        _playerEntity = new PlayerEntity((int)Math.Round(SystemParameters.FullPrimaryScreenWidth / 10) * 2,
            (int)App.GameInfo.FloorLevel);
        App.GameInfo.Player = _playerEntity;
        App.GameInfo.CanvasEntities.AddEntity(_playerEntity);


        // Register background listener to global game timer
        App.GameTimer.AddListener("backgroundListener", BackgroundTick);


        // Load background one into rectangle
        Canvas.SetLeft(BackgroundOne, 0);
        BackgroundImageOne.ImageSource =
            new BitmapImage(new Uri("pack://application:,,,/Assets\\Background\\background1.png"));
        CanvasContainer.Loaded += (sender, args) => BackgroundOne.Width = BackgroundImageOne.ImageSource.Width;
        CanvasContainer.Loaded += (sender, args) => BackgroundOne.Height = CanvasContainer.ActualHeight;
        BackgroundOne.Fill = BackgroundImageOne;

        // Load background Two into rectangle
        Canvas.SetLeft(BackgroundTwo, 0);
        BackgroundImageTwo.ImageSource =
            new BitmapImage(new Uri("pack://application:,,,/Assets\\Background\\background1.png"));
        CanvasContainer.Loaded += (sender, args) => BackgroundTwo.Width = BackgroundImageTwo.ImageSource.Width;
        CanvasContainer.Loaded += (sender, args) => BackgroundTwo.Height = CanvasContainer.ActualHeight;
        BackgroundTwo.Fill = BackgroundImageTwo;
    }

    private void HighscoreTick(object? sender, EventArgs e)
    {
        _playerScoreTracker += App.GameTimer.DeltaTime;

        // Check if one second has elapsed
        if (_playerScoreTracker > 0.6)
        {
            _playerScoreTracker = 0;
            App.GameInfo.PlayerScore++;
            ScoreTextBlock.Text = App.GameInfo.PlayerScore.ToString();

            var random = new Random();
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
                App.GameInfo.CanvasEntities.AddEntity(new RocketPants(2000, (int)_playerEntity.GetY()));
        }
    }

    private void CanvasTick(object? sender, EventArgs e)
    {
    }

    private void EntitiesTick(object? sender, EventArgs e)
    {
        // 'Coppies' canvas entities array so it can be modified whilst being looped over.
        foreach (var entity in App.GameInfo.CanvasEntities.GetCanvasEntities().ToArray())
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
                App.GameInfo.PlayerLives--;
                DisplayPlayerLives();

                if (App.GameInfo.PlayerLives <= 0) Exit();
            }
        }
    }

    private void BackgroundTick(object? sender, EventArgs e)
    {
        // loop background for infinite runner
        if (BackgroundOne.Width - -Canvas.GetLeft(BackgroundOne) < CanvasContainer.ActualWidth)
            Canvas.SetLeft(BackgroundTwo, Canvas.GetLeft(BackgroundOne) + BackgroundOne.ActualWidth);
        if (BackgroundTwo.Width - -Canvas.GetLeft(BackgroundTwo) < CanvasContainer.ActualWidth)
            Canvas.SetLeft(BackgroundOne, Canvas.GetLeft(BackgroundTwo) + BackgroundTwo.ActualWidth);

        // apply movement to both backgrounds
        Canvas.SetLeft(BackgroundOne,
            Canvas.GetLeft(BackgroundOne) - (App.GameInfo.GameSpeed + 400) * App.GameTimer.DeltaTime);
        Canvas.SetLeft(BackgroundTwo,
            Canvas.GetLeft(BackgroundTwo) - (App.GameInfo.GameSpeed + 400) * App.GameTimer.DeltaTime);

        _backgroundTracker += App.GameTimer.DeltaTime;

        // increase speed if conditions are met
        if (_backgroundTracker > 5 && (App.GameInfo.GameSpeed + 400) + 10 < App.GameInfo.MaxGameSpeed)
        {
            _backgroundTracker = 0;
            App.GameInfo.GameSpeed += 5;
        }
        else if ((App.GameInfo.GameSpeed + 400) + 10 > App.GameInfo.MaxGameSpeed)
        {
            App.GameInfo.GameSpeed = App.GameInfo.MaxGameSpeed;
        }
    }

    private void CanvasKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Space) _playerEntity.SetPlayerRising(true);

        if (e.Key == Key.Escape)
        {
            Exit();
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

        var random = new Random();

        // Choose random canvas lane
        var num = random.Next(0, _canvasLanes.Count);
        var randomLane = _canvasLanes[random.Next(0, _canvasLanes.Count)];

        // Recurse function until another random lane has been found
        if (_lastLane != null && randomLane == _lastLane) randomLane = GetRandomCanvasLane();

        // Set last lane to current selected name
        _lastLane = randomLane;
        return randomLane;
    }

    private void DisplayPlayerLives()
    {
        switch (App.GameInfo.PlayerLives)
        {
            case 3:
                ShowHeart(LiveHeart1, LiveHeartEmpty1);
                ShowHeart(LiveHeart2, LiveHeartEmpty2);
                ShowHeart(LiveHeart3, LiveHeartEmpty3);
                break;
            case 2:
                ShowHeart(LiveHeart1, LiveHeartEmpty1);
                ShowHeart(LiveHeart2, LiveHeartEmpty2);
                HideHeart(LiveHeart3, LiveHeartEmpty3);
                break;
            case 1:
                ShowHeart(LiveHeart1, LiveHeartEmpty1);
                HideHeart(LiveHeart2, LiveHeartEmpty2);
                HideHeart(LiveHeart3, LiveHeartEmpty3);
                break;
            case 0:
                HideHeart(LiveHeart1, LiveHeartEmpty1);
                HideHeart(LiveHeart2, LiveHeartEmpty2);
                HideHeart(LiveHeart3, LiveHeartEmpty3);
                break;
        }
    }

    private void HideHeart(Image heart, Image heartOutline)
    {
        heart.Visibility = Visibility.Hidden;
        heartOutline.Visibility = Visibility.Visible;
    }

    private void ShowHeart(Image heart, Image heartOutline)
    {
        heart.Visibility = Visibility.Visible;
        heartOutline.Visibility = Visibility.Hidden;
    }

    public void Exit()
    {
        App.GameTimer.RemoveListener("canvasListener");
        App.GameTimer.RemoveListener("highscoreListener");
        App.GameTimer.RemoveListener("entitiesListener");
        App.GameTimer.RemoveListener("backgroundListener");

        //Helpers.OpenPreviousWindow();
        Helpers.OpenWindow(new GameOver());
    }
}