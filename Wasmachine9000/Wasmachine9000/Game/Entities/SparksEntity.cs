using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Wasmachine9000.Game.CanvasObject;

namespace Wasmachine9000.Game.Entities;

public class SparksEntity : CanvasEntity
{

    private List<ImageBrush> _sparkImageBrushes = new List<ImageBrush>();

    private int _currentSpark = 0;
    private double _sparkTick = 0;

    private ScaleTransform _sparkTransformTop = new ScaleTransform(1, -1);
    private ScaleTransform _sparkTransformBottom = new ScaleTransform(1, 1);

    public SparksEntity(int x, int y) : base("Sparks/frame1.png", x, y) //contructor
    {
        for (int i = 1; i < 5; i++)
        {
            ImageBrush brush = new ImageBrush();
            brush.ImageSource = new BitmapImage(new Uri(Helpers.GetSpriteResource("Sparks/frame" + i + ".png")));;
            _sparkImageBrushes.Add(brush);
        }
    }

    public override void Create()
    {

        EntityRectangle.Width = _sparkImageBrushes[0].ImageSource.Width / 3;
        EntityRectangle.Height = _sparkImageBrushes[0].ImageSource.Height / 3;
        EntityRectangle.Fill = _sparkImageBrushes[0];

        SetVisible(false);

        SetZIndex(3);

    }

    public override void EntityTick()
    {
        _sparkTick += App.GameTimer.DeltaTime;

        // Sparks.RenderTransform = _sparkTransformTop;

        if (App.GameInfo.Player.GetY() + App.GameInfo.Player.GetEntityRectangle().Height == App.GameInfo.CeilingLevel)
        {

            EntityRectangle.RenderTransform = _sparkTransformTop;
            SetVisible(true);

            SetPosition(App.GameInfo.Player.GetX() - EntityRectangle.Width, App.GameInfo.CeilingLevel - EntityRectangle.Height * 2);


        } else if (App.GameInfo.Player.GetY() == App.GameInfo.FloorLevel)
        {

            EntityRectangle.RenderTransform = _sparkTransformBottom;
            SetVisible(true);

            SetPosition(App.GameInfo.Player.GetX() - EntityRectangle.Width, App.GameInfo.Player.GetY());


        } else
        {
            SetVisible(false);
        }



        if (_sparkTick > 0.05)
        {
            _sparkTick = 0;

            EntityRectangle.Fill = _sparkImageBrushes[_currentSpark];

            _currentSpark++;

            if (_currentSpark == 4)
            {
                _currentSpark = 0;
            }
        }
    }

}