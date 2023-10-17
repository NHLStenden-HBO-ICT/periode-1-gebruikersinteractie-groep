﻿using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Wasmachine9000.Game.CanvasObject;

namespace Wasmachine9000.Game.Entities;

public class DirtyClothes : CanvasEntity
{
    private static readonly string[] Sprites =
    {
        "tshirt-blue.png",
        "tshirt-green.png",
        "tshirt-yellow.png"
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
        SetX(GetX() - (900 + App.GameInfo.GameSpeed) * App.GameTimer.DeltaTime);

        if (Helpers.CollidesWithPlayer(EntityRectangle))
        {
            Destroy();
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
}