namespace Wasmachine9000.Game.CanvasObject;

public class CanvasLane
{
    private int _laneHeight;

    public CanvasLane(int laneHeight)
    {
        _laneHeight = laneHeight;
    }

    public int GetLanePosition()
    {
        return _laneHeight;
    }
}