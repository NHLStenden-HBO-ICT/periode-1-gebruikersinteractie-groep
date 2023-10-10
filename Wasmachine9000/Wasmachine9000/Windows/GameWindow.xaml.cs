using System;
using System.Windows;
using System.Windows.Input;

namespace Wasmachine9000.Windows;

public partial class GameWindow : Window
{
    public GameWindow()
    {
        InitializeComponent();

        GameCanvas.Focus(); // Makes keyboard event work

        // Register the canvas listener to the global game timer.
        App.GameTimer.AddListener("canvasListener", CanvasTick);
    }

    public void CanvasTick(object? sender, EventArgs e)
    {
        Console.WriteLine("Tick");
    }

    private void CanvasKeyDown(object sender, KeyEventArgs e)
    {
        Console.WriteLine("Key pressed: " + e.Key);
        if (e.Key == Key.Escape)
        {
            App.GameTimer.RemoveListener("canvasListener");
            Helpers.OpenPreviousWindow();
        }
    }
}