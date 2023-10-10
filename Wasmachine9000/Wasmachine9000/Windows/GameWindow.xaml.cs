using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Wasmachine9000.Windows;

public partial class GameWindow : Window
{
    private int _playerUpVelocity = 0;
    private int _playerSideVelocity = 0;
    private int _playerAcceleration = 40;
    private int _resistance = 1;
    private int _gravity = 20;
    private bool _playerMoveLeft;
    private bool _playerMoveRight;
    private bool _playerMoveUp;
    private bool _playerMoveDown;

    private bool _playerIsFalling = false;

    public GameWindow()
    {
        InitializeComponent();

        GameCanvas.Focus(); // Makes keyboard event work

        // Register the canvas listener to the global game timer.
        App.GameTimer.AddListener("canvasListener", CanvasTick);

        Canvas.SetLeft(Player, 1);
        Canvas.SetBottom(Player, 1);
    }

    public void CanvasTick(object? sender, EventArgs e)
    {
        // Apply acceleration in pressed direction
        if (_playerMoveLeft) _playerSideVelocity -= _playerAcceleration;
        if (_playerMoveRight) _playerSideVelocity += _playerAcceleration;

        // Ensure player cannot go faster that 1000 units
        if (_playerMoveLeft && _playerSideVelocity < -1000) _playerSideVelocity = -1000;
        if (_playerMoveRight && _playerSideVelocity > 1000) _playerSideVelocity = 1000;

        // Ensure player cannot go faster 
        if (_playerUpVelocity > 1000) _playerUpVelocity = 1000;

        if (Canvas.GetBottom(Player) > 0) _playerUpVelocity -= _gravity;

        // Apply resistance to player
        if (_playerSideVelocity > 0) _playerSideVelocity -= _resistance;
        if (_playerSideVelocity < 0) _playerSideVelocity += _resistance;

        // Collion detection for the bottom of the screen
        if (Canvas.GetBottom(Player) < 0)
        {
            _playerIsFalling = false;
            _playerUpVelocity -= (int)Math.Round(_playerUpVelocity * 1.8f);
            Canvas.SetBottom(Player, 0);
        }

        if (Canvas.GetLeft(Player) < 0)
        {
            Canvas.SetLeft(Player, 0);
            _playerSideVelocity -= (int)Math.Round(_playerSideVelocity * 1.8f);
        }

        if (Canvas.GetLeft(Player) > SystemParameters.FullPrimaryScreenWidth - Player.Width)
        {
            Canvas.SetLeft(Player, SystemParameters.FullPrimaryScreenWidth - Player.Width);
            _playerSideVelocity -= (int)Math.Round(_playerSideVelocity * 1.8f);
            ;
        }

        Console.WriteLine(_playerSideVelocity);

        Canvas.SetBottom(Player, Canvas.GetBottom(Player) + (_playerUpVelocity * App.GameTimer.DeltaTime));
        Canvas.SetLeft(Player, Canvas.GetLeft(Player) + (_playerSideVelocity * App.GameTimer.DeltaTime));

        // if (_playerMoveLeft) Canvas.SetLeft(Player, Canvas.GetLeft(Player) - (_playerVelocity * App.GameTimer.DeltaTime));
        // if (_playerMoveRight) Canvas.SetLeft(Player, Canvas.GetLeft(Player) + (_playerVelocity * App.GameTimer.DeltaTime));
        // if (_playerMoveUp) Canvas.SetTop(Player, Canvas.GetTop(Player) - (_playerVelocity * App.GameTimer.DeltaTime));
        // if (_playerMoveDown) Canvas.SetTop(Player, Canvas.GetTop(Player) + (_playerVelocity * App.GameTimer.DeltaTime));
    }

    private void CanvasKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.A) this._playerMoveLeft = true;
        if (e.Key == Key.D) this._playerMoveRight = true;
        // if (e.Key == Key.W) this._playerMoveUp = true;
        // if (e.Key == Key.S) this._playerMoveDown = true;

        if (e.Key == Key.Space)
        {
            // if (_playerIsFalling) return;
            _playerIsFalling = true;
            _playerUpVelocity = 500;
        }

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
        // if (e.Key == Key.Space) this._playerMoveUp = true;
        // if (e.Key == Key.W) this._playerMoveUp = false;
        // if (e.Key == Key.S) this._playerMoveDown = false;
    }
}