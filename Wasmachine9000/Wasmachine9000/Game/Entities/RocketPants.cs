using System;
using System.Windows.Media.Imaging;
using Wasmachine9000.Game.CanvasObject;

namespace Wasmachine9000.Game.Entities;

public class RocketPants : CanvasEntity
{
    public double _timer = 0;
    public bool _shouldMove = false;

    private AlertEntity _alertEntity;


    public RocketPants(int x, int y) : base("rocket-pants.png", x, y)
    {
        _alertEntity = new AlertEntity((int)App.GameInfo.GameCanvas.ActualWidth - 100, y);
        App.GameInfo.CanvasEntities.AddEntity(_alertEntity);
    }

    public override void EntityTick()
    {
        if (!_shouldMove && _alertEntity.GetFlashes() > 6)
        {
            _shouldMove = true;
            App.GameInfo.CanvasEntities.RemoveEntity(_alertEntity);
        }

        if (_shouldMove)
        {
            SetX(GetX() - 1500 * App.GameTimer.DeltaTime);
        }
    }

    public override void Create()
    {
        EntityImageBrush.ImageSource = new BitmapImage(new Uri(Helpers.GetSpriteResource(Sprite)));
        EntityRectangle.Width = EntityImageBrush.ImageSource.Width;
        EntityRectangle.Height = EntityImageBrush.ImageSource.Height;
        EntityRectangle.Fill = EntityImageBrush;

        SetPosition(EntityX, EntityY);
        SetZIndex(3);
    }
}