using System.Collections.Generic;
using System.Windows.Controls;

namespace Wasmachine9000.Game.CanvasObject;

public class CanvasEntities
{
    private List<CanvasEntity> _canvasEntities = new();
    private Canvas _gameCanvas;

    public CanvasEntities(Canvas gameCanvas)
    {
        _gameCanvas = gameCanvas;
    }

    public List<CanvasEntity> GetCanvasEntities()
    {
        return _canvasEntities;
    }

    public void AddEntity(CanvasEntity entity)
    {
        entity.Create();
        _canvasEntities.Add(entity);
        _gameCanvas.Children.Add(entity.GetEntityRectangle());
    }

    public void RemoveEntity(CanvasEntity entity)
    {
        CanvasEntity removedEntity = _canvasEntities[_canvasEntities.IndexOf(entity)];
        _canvasEntities.Remove(entity);

        removedEntity.Destroy();

        _gameCanvas.Children.Remove(removedEntity.GetEntityRectangle());
    }
}