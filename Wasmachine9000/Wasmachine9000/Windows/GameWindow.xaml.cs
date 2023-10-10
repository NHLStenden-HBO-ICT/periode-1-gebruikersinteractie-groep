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
    private int _gravity = 60;
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

        // Collion detection for the bottom of the screen
        if (Canvas.GetBottom(Player) < 0)
        {
            _playerUpVelocity = 0;
            Canvas.SetBottom(Player, 0);
        }

        // Collision detection for the top of the screen
        if (Canvas.GetBottom(Player) > SystemParameters.FullPrimaryScreenHeight - Player.Height)
        {
            _playerUpVelocity = 0;
            Canvas.SetBottom(Player, SystemParameters.FullPrimaryScreenHeight - Player.Height);
        }

        // Apply velocity to player rectangle
        Canvas.SetBottom(Player, Canvas.GetBottom(Player) + (_playerUpVelocity * App.GameTimer.DeltaTime));
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