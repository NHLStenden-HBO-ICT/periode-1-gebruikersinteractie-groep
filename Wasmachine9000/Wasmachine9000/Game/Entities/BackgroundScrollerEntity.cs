using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Wasmachine9000.Game.CanvasObject;
using YamlDotNet.Core.Events;

namespace Wasmachine9000.Game.Entities;

public class BackgroundScrollerEntity : CanvasEntity
{
    // private new int EntityZ = 6;

    // private List<CanvasEntity> _createdEntities = new List<CanvasEntity>();

    private double _initialEntityWidth = 0;

    public BackgroundScrollerEntity (int x, int y) : base(Helpers.GetSpriteResource("Background/background1.png"), x, y) //contructor
    {

    }

    public override void Create()
    {

        EntityRectangle.Width = App.GameInfo.GameCanvas.ActualHeight * 2;
        EntityRectangle.Height = App.GameInfo.GameCanvas.ActualHeight;

        _initialEntityWidth = EntityRectangle.Width;

        SetX(0);
        SetZIndex(0);

    }

    public void ChangeSprite(string sprite)
    {
        EntityImageBrush.ImageSource =  new BitmapImage(new Uri(Helpers.GetSpriteResource(sprite)));
        EntityRectangle.Fill = EntityImageBrush;
    }

    public override void EntityTick()
    {
        SetX(GetX() - (App.GameInfo.GameSpeed + 400) * App.GameTimer.DeltaTime);
    }

    public double GetInitialEntityWidth()
    {
        return _initialEntityWidth;
    }

}