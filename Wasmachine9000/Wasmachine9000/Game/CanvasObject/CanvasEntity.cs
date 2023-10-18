using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Wasmachine9000.Game.CanvasObject;

public class CanvasEntity
{
    protected int EntityX;
    protected int EntityY;
    protected int EntityZ;

    protected string Sprite;
    protected Rectangle EntityRectangle = new Rectangle();
    protected ImageBrush EntityImageBrush = new ImageBrush();

    public CanvasEntity(string sprite, int xPosition, int yPosition)
    {
        Sprite = sprite;

        EntityX = xPosition;
        EntityY = yPosition;
        SetZIndex(EntityZ);
    }

    public void SetPosition(double x, double y)
    {
        SetX(x); SetY(y);
    }

    public double GetX()
    {
        return Canvas.GetLeft(EntityRectangle);
    }

    public void SetX(double x)
    {
        Canvas.SetLeft(EntityRectangle, x);
    }

    public double GetY()
    {
        return Canvas.GetBottom(EntityRectangle);
    }

    public void SetY(double y)
    {
        Canvas.SetBottom(EntityRectangle, y);
    }

    public void SetZIndex(int z)
    {
        Canvas.SetZIndex(EntityRectangle, z);
    }

    public int GetZIndex()
    {
        return Canvas.GetZIndex(EntityRectangle);
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

    public void SetVisible(bool isVisible)
    {
        EntityRectangle.Visibility = isVisible ? Visibility.Visible : Visibility.Hidden;
    }

    public bool GetVisible()
    {
        return EntityRectangle.Visibility == Visibility.Visible;
    }

    public double GetWidth()
    {
        return EntityRectangle.ActualWidth;

    }

    public double GetLeft()
    {
        return Canvas.GetLeft(EntityRectangle);
    }
}