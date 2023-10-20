using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Wasmachine9000.Game.CanvasObject;
using YamlDotNet.Core.Events;

namespace Wasmachine9000.Game.Entities;

public class BackgroundScrollerWrapper : CanvasEntity
{
// private new int EntityZ = 6;

    // private List<CanvasEntity> _createdEntities = new List<CanvasEntity>();

    private List<BackgroundScrollerEntity> _backgroundList = new List<BackgroundScrollerEntity>();
    private int _entityIndex = 0;

    public BackgroundScrollerWrapper (int x, int y) : base(Helpers.GetSpriteResource("Background/background1.png"), x, y) //contructor
    {
        if (_backgroundList.Count != 3)
        {
            for (int i = 0; i < 3; i++)
            {
                _backgroundList.Add(new BackgroundScrollerEntity(0, 0));
                AddToCanvasEntities(_backgroundList[i]);
                _backgroundList[i].ChangeSprite("Background/background1.png");
                _backgroundList[i].SetX(_backgroundList[i].GetInitialEntityWidth() * i);
            }
        }
    }

    private double _rightmostX;
    private int _propaganda = 100;

    public override void EntityTick()
    {

        _rightmostX = 0;

        // double rightmostX = float.MinValue; // Initialize with a very small value

        // Find the rightmost X-coordinate in the list
        foreach (BackgroundScrollerEntity background in _backgroundList)
        {
            double backgroundRightX = background.GetX() + background.GetWidth();
            if (backgroundRightX > _rightmostX)
            {
                _rightmostX = backgroundRightX;
            }
        }

        // Position the background entities behind the rightmost one
        foreach (BackgroundScrollerEntity background in _backgroundList)
        {
            
            if (background.GetX() + background.GetWidth() < 0 )
            {
                background.SetX(_rightmostX);

                if (App.GameInfo.PlayerScore > _propaganda)
                {
                    background.ChangeSprite("Background/backgroundPlayStore.png");

                    _propaganda *= 2;
                }
                else if (background.GetCurrentEntitySprite() == "Background/backgroundPlayStore.png")
                {
                    background.ChangeSprite("Background/background1.png");
                }
            }

        }

    }

    private void AddToCanvasEntities(CanvasEntity entity)
    {
        App.GameInfo.CanvasEntities.AddEntity(entity);
    }
}