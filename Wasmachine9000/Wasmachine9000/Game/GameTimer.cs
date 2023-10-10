using System;
using System.Collections.Generic;
using System.Windows.Threading;

namespace Wasmachine9000.Game;

public class GameTimer
{
    private Dictionary<String, Action<object?, EventArgs>> _callbacks =
        new Dictionary<String, Action<object?, EventArgs>>();

    private DispatcherTimer _gameTimer = new DispatcherTimer();

    public int DeltaTime;

    public GameTimer()
    {
        _gameTimer.Interval = TimeSpan.FromMilliseconds(10);
        _gameTimer.Tick += GameTick;
        _gameTimer.Start();
    }

    public void AddListener(string name, Action<object?, EventArgs> callback)
    {
        _callbacks.Add(name, callback);
    }

    public void RemoveListener(string name)
    {
        _callbacks.Remove(name);
    }

    public Action<object?, EventArgs>? GetListener(string name)
    {
        if (_callbacks.TryGetValue(name, out Action<object?, EventArgs>? listener))
        {
            return listener;
        }

        return null;
    }

    public void GameTick(object? sender, EventArgs e)
    {
        foreach (KeyValuePair<string, Action<object?, EventArgs>> callback in _callbacks)
        {
            callback.Value(sender, e);
        }
    }
}