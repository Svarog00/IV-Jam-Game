using Assets.Code.Gameplay.Map;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Assets.Code.Gameplay
{
    class TaskManager
    {
        public int Energy => _energyNeeded;
        public int Time => _timeNeeded;

        private readonly MapView _map;
        private int _timeNeeded;
        private int _energyNeeded;

        public TaskManager(MapView map)
        {
            _map = map;
        }

        public void GetNewTask()
        {
            int x = 0;
            int y = 0;

            _map.GetRandomArea(out x, out y);
            _map.SetTarget(x, y);
            _map.BuildEfficientPath(out _energyNeeded, out _timeNeeded, x, y);
        }

        public bool CompleteTask(List<Area> path)
        {
            int tmpTime = 0;
            int tmpEnergy = 0;

            foreach(var area in path)
            {
                tmpTime += area.TimeCost;
                tmpEnergy += area.EnergyCost;
            }

            if((tmpTime <= _timeNeeded && tmpEnergy <= _energyNeeded) ||
                (tmpTime / tmpEnergy <= _timeNeeded / _energyNeeded))
            {
                RegenMap();
                return true;
                //GetNewTask();
            }

            return false;
        }

        private void RegenMap()
        {
            _map.RegenerateMap();
        }
    }
}
