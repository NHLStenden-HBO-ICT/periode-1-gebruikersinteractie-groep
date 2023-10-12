using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Wasmachine9000.Game.CanvasObject;

namespace Wasmachine9000.Game.Entities;

public class DirtyClothes : CanvasEntity
{
    private static readonly string[] Sprites =
    {
        "wasmachine.png",
    };


    public DirtyClothes(int x, int y) : base(GetRandomSprite(), x, y)
    {
    }

    private static string GetRandomSprite()
    {
        Random random = new Random();
        return Sprites[random.Next(0, Sprites.Length)];
    }

    public override void EntityTick()
    {
        Canvas.SetLeft(EntityRectangle, Canvas.GetLeft(EntityRectangle) - 100 * App.GameTimer.DeltaTime);
    }

    public override void Create()
    {
        EntityImageBrush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/" + Sprite));
        EntityRectangle.Width = EntityImageBrush.ImageSource.Width;
        EntityRectangle.Height = EntityImageBrush.ImageSource.Height;
        EntityRectangle.Fill = EntityImageBrush;

        Canvas.SetLeft(EntityRectangle, EntityX);
        Canvas.SetBottom(EntityRectangle, EntityY);
    }

    public override void Destroy()
    {
        // Destroy elements here and stuff.
    }
}