using System;
using System.Runtime.Intrinsics.X86;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Wasmachine9000.Game.CanvasObject;

namespace Wasmachine9000.Game.Entities;

public class PlayerEntity : CanvasEntity
{
    private bool _playerRising = false;
    private int _playerUpVelocity = 0;
    
    public PlayerEntity(int x, int y) : base("wasmachine.png", x, y)
    {
        
    }

    public void SetPlayerRising(bool isRising)
    {
        _playerRising = isRising;
    }

    public override void Create()
    {
        EntityImageBrush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/wasmachine.png"));
        EntityRectangle.Width = EntityImageBrush.ImageSource.Width;
        EntityRectangle.Height = EntityImageBrush.ImageSource.Height;
        EntityRectangle.Fill = EntityImageBrush;
        
        Canvas.SetLeft(EntityRectangle, EntityX);
        Canvas.SetBottom(EntityRectangle, EntityY);
    }

    public override void EntityTick()
    {
        if (_playerRising) _playerUpVelocity += App.GameInfo.PlayerAcceleration;

        // Ensure player cannot go faster 
        if (_playerUpVelocity > App.GameInfo.VelocityCap) _playerUpVelocity = App.GameInfo.VelocityCap;

        // Apply gravity to user when not on/under the ground
        // if (Canvas.GetBottom(Player) > 0) 
        _playerUpVelocity -= App.GameInfo.Gravity;

        // Predict if the player is going to hit the ground, acts as ground collision detection
        if (_playerUpVelocity < 0)
        {
            int currentPosition = (int)Canvas.GetBottom(EntityRectangle);
            double predictedDownPosition = currentPosition - (-_playerUpVelocity * App.GameTimer.DeltaTime);
            if (predictedDownPosition < App.GameInfo.FloorLevel)
            {
                _playerUpVelocity = 0;
                Canvas.SetBottom(EntityRectangle, App.GameInfo.FloorLevel);
            }
        }

        // Predict if the player is going to hit the ceiling, acts as ceiling collision
        if (_playerUpVelocity > 0)
        {
            int currentPosition = (int)Canvas.GetBottom(EntityRectangle) + (int)EntityRectangle.Height;
            double predictedUpPosition = currentPosition + (_playerUpVelocity * App.GameTimer.DeltaTime);
            if (predictedUpPosition > App.GameInfo.CeilingLevel)
            {
                _playerUpVelocity = 0;
                Canvas.SetBottom(EntityRectangle, App.GameInfo.CeilingLevel - EntityRectangle.Height);
            }
        }

        // Apply velocity to player
        Canvas.SetBottom(EntityRectangle, Canvas.GetBottom(EntityRectangle) + (_playerUpVelocity * App.GameTimer.DeltaTime));
    }

}