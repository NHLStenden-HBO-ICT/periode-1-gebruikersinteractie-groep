using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Wasmachine9000.Windows;

public partial class GameWindow : Window
{
    private int _playerUpVelocity = 0;
    private int _playerAcceleration = 80;
    private int _gravity = 20;

    private bool _playerRising;

    public GameWindow()
    {
        InitializeComponent();

        GameCanvas.Focus(); // Makes keyboard event work

        // Register the canvas listener to the global game timer.
        App.GameTimer.AddListener("canvasListener", CanvasTick);

        Canvas.SetLeft(Player, 100);
        Canvas.SetBottom(Player, 1);
    }

    public void CanvasTick(object? sender, EventArgs e)
    {
        if (_playerRising) _playerUpVelocity += _playerAcceleration;

        // Ensure player cannot go faster 
        if (_playerUpVelocity > 1000) _playerUpVelocity = 1000;

        if (Canvas.GetBottom(Player) > 0) _playerUpVelocity -= _gravity;

        // Collion detection for the bottom of the screen
        if (Canvas.GetBottom(Player) < 0)
        {
            _playerUpVelocity = 0;
            Canvas.SetBottom(Player, 0);
        }

        // Collision detection for the top of the screen
        if (Canvas.GetBottom(Player) > SystemParameters.FullPrimaryScreenHeight)
        {
            _playerUpVelocity = 0;
            Canvas.SetBottom(Player, SystemParameters.FullPrimaryScreenHeight);
        }

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