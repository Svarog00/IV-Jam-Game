using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Gameplay.Map
{
    public class PathBuildingManager : MonoBehaviour
    {
        public List<Area> Path => _pathList;

        [SerializeField] private List<Area> _pathList = new List<Area>();

        public void AddPoint(Area area)
        {
            _pathList.Add(area);
        }

        public void DeletePoint(Area area)
        {
            _pathList.Remove(area);
        }
    }
}