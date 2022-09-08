using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Gameplay.Map
{
    public class PathBuildingManager : MonoBehaviour
    {
        public event EventHandler<OnChangePathEventArgs> OnChangePathEventHandler;
        public event EventHandler OnPathClearActionHandler;

        public List<Area> Path => _pathList;

        [SerializeField] private List<Area> _pathList = new List<Area>();

        public void AddPoint(Area area)
        {
            _pathList.Add(area);
            OnChangePathEventHandler?.Invoke(this, new OnChangePathEventArgs 
            {
                EnergyCost = area.EnergyCost,
                TimeCost = area.TimeCost
            });
        }

        public void DeletePoint(Area area)
        {
            _pathList.Remove(area);
            OnChangePathEventHandler?.Invoke(this, new OnChangePathEventArgs 
            { 
                EnergyCost = -area.EnergyCost, 
                TimeCost = -area.TimeCost 
            });
        }

        public void ClearPath()
        {
            _pathList.Clear();
            OnPathClearActionHandler?.Invoke(this, EventArgs.Empty);
        }
    }

    public class OnChangePathEventArgs
    {
        public int TimeCost;
        public int EnergyCost;
    }
}