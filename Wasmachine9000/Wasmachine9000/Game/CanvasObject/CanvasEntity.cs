using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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
}