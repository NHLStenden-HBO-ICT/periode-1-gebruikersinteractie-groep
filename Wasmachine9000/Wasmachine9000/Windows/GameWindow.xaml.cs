using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Wasmachine9000.Windows;

public partial class GameWindow : Window
{
    // Player and movement control/variables
    private int _playerUpVelocity = 0;
    private int _velocityCap = 2000;
    private int _playerAcceleration = 170;
    private int _gravity = 80;
    private bool _playerRising;

    // Player style
    private ImageBrush playerSkin = new ImageBrush();

    public GameWindow()
    {
        InitializeComponent();

        GameCanvas.Focus(); // Makes keyboard event work

        // Register the canvas listener to the global game timer.
        App.GameTimer.AddListener("canvasListener", CanvasTick);

        Canvas.SetLeft(Player, Math.Round(SystemParameters.FullPrimaryScreenWidth / 10) * 2);
        Canvas.SetBottom(Player, 1);

        // Load playerskin into player rectangle
        playerSkin.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/wasmachine.png"));
        Player.Width = playerSkin.ImageSource.Width;
        Player.Height = playerSkin.ImageSource.Height;
        Player.Fill = playerSkin;
    }

    public void CanvasTick(object? sender, EventArgs e)
    {
        if (_playerRising) _playerUpVelocity += _playerAcceleration;

        // Ensure player cannot go faster 
        if (_playerUpVelocity > _velocityCap) _playerUpVelocity = _velocityCap;

        // Apply gravity to user when not on/under the ground
        if (Canvas.GetBottom(Player) > 0) _playerUpVelocity -= _gravity;

        double deltaTime = App.GameTimer.DeltaTime; // Store the DeltaTime for interpolation

        // Predict if the player is going to hit the ground, acts as ground collision detection
        if (_playerUpVelocity < 0)
        {
            int currentPosition = (int)Canvas.GetBottom(Player);
            double predictedDownPosition = currentPosition - (-_playerUpVelocity * deltaTime);
            if (predictedDownPosition < 0)
            {
                _playerUpVelocity = 0;
                Canvas.SetBottom(Player, 0);
            }
        }

        // Predict if the player is going to hit the ceiling, acts as ceiling collision
        if (_playerUpVelocity > 0)
        {
            int currentPosition = (int)Canvas.GetBottom(Player) + (int)Player.Height;
            double predictedUpPosition = currentPosition + (_playerUpVelocity * deltaTime);
            if (predictedUpPosition > CanvasContainer.ActualHeight)
            {
                _playerUpVelocity = 0;
                Canvas.SetBottom(Player, CanvasContainer.ActualHeight - Player.Height);
            }
        }

        // Calculate the interpolated position using cubic interpolation
        double previousPosition = Canvas.GetBottom(Player);
        double newPosition = previousPosition + (_playerUpVelocity * deltaTime);
        double interpolatedPosition = CubicInterpolate(previousPosition, newPosition, 0.0, 0.0, deltaTime); // The third and fourth arguments are 0.0

        // Apply interpolated position to player
        Canvas.SetBottom(Player, interpolatedPosition);
    }

    private double CubicInterpolate(double p0, double p1, double v0, double v1, double t)
    {
        // Cubic interpolation formula
        double t2 = t * t;
        double a = -0.5 * p0 + 1.5 * p1 - 1.5 * v0 + 0.5 * v1;
        double b = p0 - 2.5 * p1 + 2.0 * v0 - 0.5 * v1;
        double c = -0.5 * v0;
        double d = p1;
        return a * t * t2 + b * t2 + c * t + d;
    }


    private void CanvasKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Space) _playerRising = true;

        if (e.Key == Key.Escape)
        {
            App.GameTimer.RemoveListener("canvasListener");
            Helpers.OpenPreviousWindow();
        }
    }

    private void CanvasKeyUp(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Space) _playerRising = false;
    }
}