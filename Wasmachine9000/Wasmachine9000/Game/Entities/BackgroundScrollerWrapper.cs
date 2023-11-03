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
    private List<GroundScrollerEntity> _groundList = new List<GroundScrollerEntity>();
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

        if (_groundList.Count != 3)
        {
            for (int i = 0; i < 3; i++)
            {
                _groundList.Add(new GroundScrollerEntity(0, 0));
                AddToCanvasEntities(_groundList[i]);
                _groundList[i].ChangeSprite("Ground/groundEntity1.png");
                _groundList[i].SetX(_groundList[i].GetInitialEntityWidth() * i);
            }
        }
    }

    private double _backgroundRightmostX;
    private double _groundRightmostX;
    private int _propaganda = 100;
    private int _counter;
    private int _door = 3;
    private int _exit = 4;

    private int _backgroundId = 0;
    private int _floorId = 0;

    public override async void EntityTick()
    {

        _backgroundRightmostX = 0;
        _groundRightmostX = 0;

        if (_backgroundList[_backgroundId].GetX() +_backgroundList[_backgroundId].GetWidth() < 0 )
        {
            foreach (BackgroundScrollerEntity backgroundCalc in _backgroundList)
            {
                double backgroundRightX = backgroundCalc.GetX() + backgroundCalc.GetWidth();
                if (backgroundRightX > _backgroundRightmostX)
                {
                    _backgroundRightmostX = backgroundRightX;
                }
            }

            _backgroundList[_backgroundId].SetX(_backgroundRightmostX);
            _backgroundRightmostX = 0;

            if (App.GameInfo.PlayerScore > _propaganda)
            {
                _backgroundList[_backgroundId].ChangeSprite("Background/backgroundPlayStore.png");

                _propaganda *= 2;
            }
            // else if (_door == _counter)
            // {
            //     _backgroundList[_backgroundId].ChangeSprite("Background/backgroundDoor.png");
            //     _door += 3;
            // }
            // else if (_exit == _counter)
            // {
            //     _backgroundList[_backgroundId].ChangeSprite("Background/backgroundExit.png");
            //     _exit += 3;
            // }
            else if (_backgroundList[_backgroundId].GetCurrentEntitySprite() != "Background/background1.png")
            {
                _backgroundList[_backgroundId].ChangeSprite("Background/background1.png");
            }
            else
            {
                _counter++;
            }

            _backgroundId++;
            if (_backgroundId == _backgroundList.Count)
            {
                _backgroundId = 0;
            }

        }


        // Position the background entities behind the rightmost one
        if (_groundList[_floorId].GetX() + _groundList[_floorId].GetWidth() < 0 )
        {

            foreach (GroundScrollerEntity groundCalc in _groundList)
            {
                double groundRightX = groundCalc.GetX() + groundCalc.GetWidth();
                if (groundRightX > _groundRightmostX)
                {
                    _groundRightmostX = groundRightX;
                }
            }

            _groundList[_floorId].SetX(_groundRightmostX);

            _floorId++;
            if (_floorId == _groundList.Count)
            {
                _floorId = 0;
            }
        }

    }

    private void AddToCanvasEntities(CanvasEntity entity)
    {
        App.GameInfo.CanvasEntities.AddEntity(entity);
    }
}