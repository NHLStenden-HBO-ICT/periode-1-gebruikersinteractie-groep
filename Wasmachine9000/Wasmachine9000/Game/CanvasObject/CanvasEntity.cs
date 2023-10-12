﻿using System.Windows.Media;
using System.Windows.Shapes;

namespace Wasmachine9000.Game.CanvasObject;

public class CanvasEntity
{
    protected int EntityX;
    protected int EntityY;

    protected string Sprite;
    protected Rectangle EntityRectangle = new Rectangle();
    protected ImageBrush EntityImageBrush = new ImageBrush();

    public CanvasEntity(string sprite, int xPosition, int yPosition)
    {
        Sprite = sprite;

        EntityX = xPosition;
        EntityY = yPosition;
    }

    public int GetX()
    {
        return EntityX;
    }

    public int GetY()
    {
        return EntityY;
    }

    public virtual void Create()
    {
    }

    public virtual void EntityTick()
    {
    }

    public virtual void Destroy()
    {
    }

    public Rectangle GetEntityRectangle()
    {
        return EntityRectangle;
    }
}