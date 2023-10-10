using System;
using System.Collections.Generic;
using System.Windows.Threading;

namespace Wasmachine9000.Game;

public class GameTimer
{
    private Dictionary<String, Action<object?, EventArgs>> _callbacks = new();

    private DispatcherTimer _gameTimer = new DispatcherTimer();

    public double DeltaTime;
    private long _lastTimestampMs;

    public GameTimer()
    {
        _gameTimer.Interval = TimeSpan.FromMilliseconds(1);
        _gameTimer.Tick += GameTick;
        _gameTimer.Start();
        this._lastTimestampMs = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        this.DeltaTime = 0;
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
        // Calculate the time between the previous and current frame. Used to ensure all objects move/act at the same rate
        // even when the clock is running slower. Mostly used for player and enemy movement.
        this.DeltaTime = ((double)DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - (double)this._lastTimestampMs) / 1000;
        foreach (KeyValuePair<string, Action<object?, EventArgs>> callback in _callbacks)
        {
            callback.Value(sender, e);
        }

        this._lastTimestampMs = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    }
}