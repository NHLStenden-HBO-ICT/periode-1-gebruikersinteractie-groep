using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using Wasmachine9000.Game.CanvasObject;
using Wasmachine9000.Game.Entities;

namespace Wasmachine9000.Windows;

public partial class GameWindow : Window
{
    private bool IsGamePaused = false;

    private double _backgroundTracker;

    private readonly List<CanvasLane> _canvasLanes = new();
    private CanvasLane? _lastLane;

    // Player
    private readonly PlayerEntity _playerEntity;
    private double _playerScoreTracker;

    // Parental control trackers
    private DispatcherTimer _parentalControlTimer = new();
    private TimeSpan _timeLeft;


    public GameWindow()
    {
        InitializeComponent();

        // Parent controll timer
        if (App.GameState.PlaytimeControl)
        {
            _parentalControlTimer.Tick += ParentalControlTimerTick;
            _parentalControlTimer.Interval = TimeSpan.FromSeconds(1);
            _parentalControlTimer.Start();
            TimeBorder.Visibility = Visibility.Visible;
        }


        App.GameInfo.GameCanvas = GameCanvas;
        App.GameInfo.PlayerLives = 3;


        // Set bottom and ceiling level
        App.GameInfo.FloorLevel = 49;
        CanvasContainer.Loaded += (sender, args) =>
        {
            App.GameInfo.CeilingLevel = CanvasContainer.ActualHeight;

            // Create canvas lanes
            int canvasLaneAmount = 6;
            for (int i = 0; i < canvasLaneAmount; i++)
            {
                _canvasLanes.Add(new CanvasLane((int)(GameCanvas.ActualHeight / (canvasLaneAmount + 2)) * (i + 1) +
                                                20));
            }

            App.GameInfo.CanvasEntities.AddEntity(new BackgroundScrollerWrapper(0, 0));
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
        // Add cosmetic entity
        _playerEntity.CheckCosmetic();

        // Register background listener to global game timer
        App.GameTimer.AddListener("backgroundListener", BackgroundTick);

        App.GameInfo.CanvasEntities.AddEntity(new SparksEntity(0, 0));

        // CanvasContainer.Loaded += (sender, args) => App.GameInfo.CanvasEntities.AddEntity(new BackgroundScrollerWrapper(0, 0));
    }

    private void ParentalControlTimerTick(object? sender, EventArgs e)
    {
        if (IsGamePaused) return;

        // Increase time played.
        App.GameState.PlaytimePassed += 1;

        _timeLeft = TimeSpan.FromSeconds(App.GameState.MaxplayTime - App.GameState.PlaytimePassed);
        if (_timeLeft > TimeSpan.Zero)
        {
            // Display left over time.
            TimeLimit.Text = $"{_timeLeft.Minutes:00}:{_timeLeft.Seconds:00}";
        }
        else
        {
            // Set player locked date one hour from now and reset play time passed.
            App.GameState.PlayLockedUntil = DateTime.Now + TimeSpan.FromHours(1);
            App.GameState.PlaytimePassed = 0;
            App.GameState.SaveGameState();

            TimeLimit.Text = "0:00";

            Exit();
        }
    }


    private void HighscoreTick(object? sender, EventArgs e)
    {
        if (IsGamePaused) return;

        _playerScoreTracker += App.GameTimer.DeltaTime;

        // Check if one second has elapsed
        if (_playerScoreTracker > 0.6)
        {
            _playerScoreTracker = 0;
            App.GameInfo.PlayerScore++;
            ScoreTextBlock.Text = App.GameInfo.PlayerScore.ToString();
            CoinsText.Text = App.GameInfo.PlayerCoins.ToString();

            var random = new Random();
            if (random.Next(0, 5) == 4)
            {
                App.GameInfo.CanvasEntities.AddEntity(new DirtyClothes(2000, GetRandomCanvasLane().GetLanePosition()));
                App.GameInfo.CanvasEntities.AddEntity(new DirtyClothes(2000, GetRandomCanvasLane().GetLanePosition()));

                App.GameInfo.CanvasEntities.AddEntity(new CoinEntity(3000, GetRandomCanvasLane().GetLanePosition()));
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
        if (IsGamePaused) return;
    }

    private void EntitiesTick(object? sender, EventArgs e)
    {
        if (IsGamePaused) return;

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
            if (entity is not PlayerEntity && entity is not SparksEntity && entity is not CosmeticEntity &&
                entity is not BackgroundScrollerEntity &&
                Helpers.CollidesWithPlayer(entity.GetEntityRectangle()))
            {
                App.GameInfo.CanvasEntities.RemoveEntity(entity);

                if (entity is CoinEntity)
                {
                    App.GameInfo.PlayerCoins++;
                }
                else
                {
                    App.GameInfo.PlayerLives--;
                    DisplayPlayerLives();
                }

                if (App.GameInfo.PlayerLives <= 0) Exit();
            }
        }
    }

    private void BackgroundTick(object? sender, EventArgs e)
    {
        if (IsGamePaused) return;

        _backgroundTracker += App.GameTimer.DeltaTime;

        // // increase speed if conditions are met
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

    private void HideInstructionText()
    {
        if (!InstructionText.IsVisible) return;
        InstructionText.Visibility = Visibility.Hidden;
    }


    private void CanvasKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Space) _playerEntity.SetPlayerRising(true);
        HideInstructionText();

        if (e.Key == Key.Escape)
        {
            //Exit();
            IsGamePaused = !IsGamePaused;
            if (PlayAgainButton.Visibility == Visibility.Visible)
            {
                PlayAgainButton.Visibility = Visibility.Hidden;
                StopButton.Visibility = Visibility.Hidden;
                ContinueButton.Visibility = Visibility.Hidden;
                PauseScreen.Visibility = Visibility.Hidden;
                Settings.Visibility = Visibility.Hidden;
            }
            else
            {
                PlayAgainButton.Visibility = Visibility.Visible;
                StopButton.Visibility = Visibility.Visible;
                ContinueButton.Visibility = Visibility.Visible;
                PauseScreen.Visibility = Visibility.Visible;
                Settings.Visibility = Visibility.Visible;
            }
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

    private void ContinueButton_Click(object sender, RoutedEventArgs e)
    {
        IsGamePaused = !IsGamePaused;

        PlayAgainButton.Visibility = Visibility.Hidden;
        StopButton.Visibility = Visibility.Hidden;
        ContinueButton.Visibility = Visibility.Hidden;
        PauseScreen.Visibility = Visibility.Hidden;
        Settings.Visibility = Visibility.Hidden;

        GameCanvas.Focus();
    }


    private void PlayAgain_Click(object sender, RoutedEventArgs e)
    {
        IsGamePaused = !IsGamePaused;

        App.GameTimer.RemoveListener("canvasListener");
        App.GameTimer.RemoveListener("highscoreListener");
        App.GameTimer.RemoveListener("entitiesListener");
        App.GameTimer.RemoveListener("backgroundListener");

        App.GameInfo.Reset();

        Helpers.OpenWindow(new GameWindow());
    }

    private void StopButton_Click(object sender, RoutedEventArgs e)
    {
        App.GameTimer.RemoveListener("canvasListener");
        App.GameTimer.RemoveListener("highscoreListener");
        App.GameTimer.RemoveListener("entitiesListener");
        App.GameTimer.RemoveListener("backgroundListener");

        //Helpers.OpenPreviousWindow();
        Helpers.OpenWindow(new GameOver());
    }

    private void Settings_Click(object sender, RoutedEventArgs e)
    {
        Helpers.OpenWindow(new Instellingen());
    }
}