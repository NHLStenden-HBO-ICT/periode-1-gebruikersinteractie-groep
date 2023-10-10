using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Wasmachine9000.Windows;

public partial class GameWindow : Window
{
    private int _playerVelocity = 2000;
    private bool _playerMoveLeft;
    private bool _playerMoveRight;
    private bool _playerMoveUp;
    private bool _playerMoveDown;

    public GameWindow()
    {
        InitializeComponent();

        GameCanvas.Focus(); // Makes keyboard event work

        // Register the canvas listener to the global game timer.
        App.GameTimer.AddListener("canvasListener", CanvasTick);

        Canvas.SetLeft(Player, 0);
        Canvas.SetTop(Player, 0);
    }

    public void CanvasTick(object? sender, EventArgs e)
    {
        Console.WriteLine(App.GameTimer.DeltaTime);

        if (_playerMoveLeft)
            Canvas.SetLeft(Player, Canvas.GetLeft(Player) - (_playerVelocity * App.GameTimer.DeltaTime));
        if (_playerMoveRight)
            Canvas.SetLeft(Player, Canvas.GetLeft(Player) + (_playerVelocity * App.GameTimer.DeltaTime));
        if (_playerMoveUp) Canvas.SetTop(Player, Canvas.GetTop(Player) - (_playerVelocity * App.GameTimer.DeltaTime));
        if (_playerMoveDown) Canvas.SetTop(Player, Canvas.GetTop(Player) + (_playerVelocity * App.GameTimer.DeltaTime));
    }

    private void CanvasKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.A) this._playerMoveLeft = true;
        if (e.Key == Key.D) this._playerMoveRight = true;
        if (e.Key == Key.W) this._playerMoveUp = true;
        if (e.Key == Key.S) this._playerMoveDown = true;


        if (e.Key == Key.Escape)
        {
            App.GameTimer.RemoveListener("canvasListener");
            Helpers.OpenPreviousWindow();
        }
    }

    private void CanvasKeyUp(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.A) this._playerMoveLeft = false;
        if (e.Key == Key.D) this._playerMoveRight = false;
        if (e.Key == Key.W) this._playerMoveUp = false;
        if (e.Key == Key.S) this._playerMoveDown = false;
    }
}