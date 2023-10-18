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
        for (int i = 0; i < 3; i++)
        {
            _backgroundList.Add(new BackgroundScrollerEntity(0, 0));
            AddToCanvasEntities(_backgroundList[i]);
            _backgroundList[i].ChangeSprite("Background/background1.png");
            _backgroundList[i].SetX(_backgroundList[i].GetInitialEntityWidth() * i);
        }

    }

    public override void EntityTick()
    {

        if (_backgroundList[_entityIndex].GetX() + _backgroundList[_entityIndex].GetWidth() <= 0)
        {

            // Update the index to switch to the next background
            _entityIndex = (_entityIndex + 1) % _backgroundList.Count;

            // Calculate the new X position for the active background
            // double newX = _backgroundList[(_entityIndex == _backgroundList.Count) ? _entityIndex++ : _entityIndex = 0].GetX() + _backgroundList[_entityIndex].GetWidth();
            Console.WriteLine((_entityIndex == _backgroundList.Count) ? _entityIndex = 0 : _entityIndex++ );

            // Set the X position for the active background to create a seamless scrolling effect
            _backgroundList[_entityIndex].SetX(0);

            // Add any logic related to changing the appearance of the active background here
            // For example, you can change the Fill property of the background based on certain conditions.
            // if (App.GameInfo.PlayerScore > _propaganda)
            // {
            //     _backgroundList[_activeBackgroundIndex].ChangeSprite("NewBackgroundImage.png");
            //     _propaganda *= 2;
            // }
            // else
            // {
            //     _backgroundList[_activeBackgroundIndex].ChangeSprite("DefaultBackgroundImage.png");
            // }

            // _entityIndex++;
            // Console.WriteLine((_entityIndex == _backgroundList.Count) ? _entityIndex++ : _entityIndex = 0);
        }

        if (_entityIndex == _backgroundList.Count)
        {
            _entityIndex = 0;
        }


        //
        // Console.WriteLine(_backgroundList[_entityIndex].GetLeft());
        // Console.WriteLine(_backgroundList[_entityIndex].GetX());
        //
        // if (_backgroundList[_entityIndex].GetLeft() <= 0 )
        // {
        //
        //     Console.WriteLine(_backgroundList[_entityIndex].GetLeft());
        //
        //     int temp = _entityIndex;
        //     temp++;
        //     if (temp == _backgroundList.Count)
        //     {
        //         temp = 0;
        //     }
        //
        //     _backgroundList[_entityIndex].SetX(_backgroundList[temp].GetLeft());
        //
        //     _entityIndex++;
        // }

        // for (int i = 0; i < _backgroundList.Count; i++)
        // {
        //     if (_backgroundList[i].GetX() + _backgroundList[i].GetWidth() <= 0 )
        //     {
        //         int next = i;
        //         next++;
        //         if (next == _backgroundList.Count)
        //         {
        //             next = 0;
        //         }
        //
        //
        //         // Console.WriteLine("trigger");
        //         _backgroundList[i].SetX(_backgroundList[next].GetX() + _backgroundList[next].GetWidth());
        //         // Console.WriteLine(_backgroundList[i].GetX());
        //     }
        //
        //     // _backgroundList[i].SetX(_backgroundList[i].GetX() - (App.GameInfo.GameSpeed + 400) * App.GameTimer.DeltaTime);
        //     // Console.WriteLine(_backgroundList[i].GetX());
        // }

        // foreach (var entity in _backgroundList)
        // {
        //     entity.SetX(entity.GetX() - (App.GameInfo.GameSpeed + 400) * App.GameTimer.DeltaTime);
        // }

    }

    private void AddToCanvasEntities(CanvasEntity entity)
    {
        App.GameInfo.CanvasEntities.AddEntity(entity);
    }
}