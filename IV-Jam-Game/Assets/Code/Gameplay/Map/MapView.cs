using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Gameplay.Map
{
    public class MapView : MonoBehaviour
    {
        [SerializeField] private Vector3 _originPosition = Vector3.zero;

        [SerializeField] private AreasContainer _areasContainer;

        [SerializeField] private float _areaSize;
        [SerializeField] private int _width;
        [SerializeField] private int _height;

        private MapModel _model;

        void Start()
        {
            _model = new MapModel(_width, _height, _areasContainer);

            DrawScheme();
            DrawMap();
            CreateBorder();
        }

        private void CreateBorder()
        {
            
        }

        private void DrawMap()
        {
            for (int i = 0; i < _model.Areas.GetLength(0); i++)
            {
                for (int j = 0; j < _model.Areas.GetLength(1); j++)
                {
                    _model.Areas[i, j].CreateInstance(GetWorldPosition(i, j) + new Vector3(_areaSize, _areaSize) * .5f);
                }
            }
        }

        private void DrawScheme()
        {
            for (int i = 0; i < _model.Areas.GetLength(0); i++)
            {
                for (int j = 0; j < _model.Areas.GetLength(1); j++)
                {
                    UtilitiesClass.CreateWorldText(_model.Areas[i, j]?.ToString(), null, 
                        GetWorldPosition(i, j) + new Vector3(_areaSize, _areaSize) * .5f, 
                        5, Color.white, TextAnchor.MiddleCenter, TextAlignment.Center);

                    Debug.DrawLine(GetWorldPosition(i, j), GetWorldPosition(i, j + 1), Color.white, 100f);
                    Debug.DrawLine(GetWorldPosition(i, j), GetWorldPosition(i + 1, j), Color.white, 100f);
                }
                Debug.DrawLine(GetWorldPosition(0, _height), GetWorldPosition(_width, _height), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(_width, 0), GetWorldPosition(_width, _height), Color.white, 100f);
            }
        }

        public Vector3 GetWorldPosition(int x, int y)
        {
            return new Vector3(x, y) * _areaSize + _originPosition;
        }

        public void GetXY(Vector3 worldPosition, out int x, out int y)
        {
            x = Mathf.FloorToInt((worldPosition - _originPosition).x / _areaSize);
            y = Mathf.FloorToInt((worldPosition - _originPosition).y / _areaSize);
        }
    }
}