using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Wasmachine9000.Game.CanvasObject;
using YamlDotNet.Core.Events;

namespace Wasmachine9000.Game.Entities;

public class BackgroundScrollerEntity : CanvasEntity
{

    private double _initialEntityWidth = 0;
    private string _currentSprite = "";

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
        _currentSprite = sprite;
        EntityImageBrush.ImageSource =  new BitmapImage(new Uri(Helpers.GetSpriteResource(sprite)));
        EntityRectangle.Width = App.GameInfo.GameCanvas.ActualHeight * (EntityImageBrush.ImageSource.Width / EntityImageBrush.ImageSource.Height);
        EntityRectangle.Fill = EntityImageBrush;
    }

    public override async void EntityTick()
    {
        SetX(GetX() - (App.GameInfo.GameSpeed + 400) * App.GameTimer.DeltaTime);
        // SetX(GetX() - 450 * App.GameTimer.DeltaTime);
    }

    public double GetInitialEntityWidth()
    {
        return _initialEntityWidth;
    }

    public string GetCurrentEntitySprite()
    {
        return _currentSprite;
    }

}