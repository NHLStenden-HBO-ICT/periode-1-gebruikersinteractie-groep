using System.Collections.Generic;
using System.Windows.Controls;

namespace Wasmachine9000.Game.CanvasObject;

public class CanvasEntities
{
    private List<CanvasEntity> _canvasEntities = new();

    public CanvasEntities()
    {
    }

    public List<CanvasEntity> GetCanvasEntities()
    {
        return _canvasEntities;
    }

    public void AddEntity(CanvasEntity entity)
    {
        entity.Create();
        _canvasEntities.Add(entity);
        App.GameInfo.GameCanvas.Children.Add(entity.GetEntityRectangle());
    }

    public void RemoveEntity(CanvasEntity entity)
    {
        CanvasEntity removedEntity = _canvasEntities[_canvasEntities.IndexOf(entity)];
        _canvasEntities.Remove(entity);

        removedEntity.Destroy();

        App.GameInfo.GameCanvas.Children.Remove(removedEntity.GetEntityRectangle());
    }
}