using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Wasmachine9000.Game.CanvasObject;

namespace Wasmachine9000.Game.Entities;

public class CosmeticEntity : CanvasEntity
{

    public static string Sprites()
    {
        Dictionary<string, Dictionary<string, bool>> cosmetics = GameState.LoadGameState().GetAllCosmetics();
        for (int i = 1; i <= 6; i++)
        {
            if (cosmetics["Cosmetic" + i]["Equipped"])
            {
                return "Cosmetics/cosmetic" + i + ".png";
            }
        }

        return "";
    }

    public CosmeticEntity(int x, int y) : base(Sprites(), x, y)
    {
        
    }
    public override void EntityTick()
    {
    }
    public override void Create()
    {
        if (Sprite.Length == 0)
        {
            return;
        }
        EntityImageBrush.ImageSource = new BitmapImage(new Uri(Helpers.GetSpriteResource(Sprites())));
        EntityRectangle.Width = EntityImageBrush.ImageSource.Width + 40;
        EntityRectangle.Height = EntityImageBrush.ImageSource.Height + 40;
        EntityRectangle.Fill = EntityImageBrush;

        SetPosition(EntityX, EntityY);
        SetZIndex(3);
    }
    public void CheckCosmetic()
    {
        Sprites();
        if (Sprite.Length == 0)
        {
            return;
        }
        else
        {
            EntityImageBrush.ImageSource = new BitmapImage(new Uri(Helpers.GetSpriteResource(Sprites())));
            EntityRectangle.Fill = EntityImageBrush;
        }
    }
}