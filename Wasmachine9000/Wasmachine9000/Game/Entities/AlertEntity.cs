using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Wasmachine9000.Game.CanvasObject;
using Wasmachine9000.Windows;

namespace Wasmachine9000.Game.Entities;

public class AlertEntity : CanvasEntity
{
    private double _timer = 0;
    private int _flashes = 0;

    public AlertEntity(int x, int y) : base("rocket-pants-alert.png", x, y)
    {
        
    }

    public override void EntityTick()
    {
        _timer += App.GameTimer.DeltaTime;

        if (_timer > 0.5)
        {
            _timer = 0;
            _flashes++;
            // Invert entity visibility
            SetVisible(!GetVisible());
        }
    }

    public override void Create()
    {
        EntityImageBrush.ImageSource = new BitmapImage(new Uri(Helpers.GetSpriteResource(Sprite)));
        EntityRectangle.Width = EntityImageBrush.ImageSource.Width;
        EntityRectangle.Height = EntityImageBrush.ImageSource.Height;
        EntityRectangle.Fill = EntityImageBrush;
        
        SetPosition(EntityX, EntityY);
    }

    public int GetFlashes()
    {
        return _flashes;
    }
}