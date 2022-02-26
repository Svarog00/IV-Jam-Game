using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Gameplay.Map
{
    public class PathBuildingManager : MonoBehaviour
    {
        public IEnumerable Path => _path;

        private List<Area> _path = new List<Area>();

        public void AddPoint(Area area)
        {
            _path.Add(area);
        }

        public void DeletePoint(Area area)
        {
            _path.Remove(area);
        }
    }
}