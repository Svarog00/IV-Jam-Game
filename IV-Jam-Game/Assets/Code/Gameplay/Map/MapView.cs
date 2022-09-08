using Assets.Code.Gameplay.Camera;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Gameplay.Map
{
    public class MapView : MonoBehaviour
    {
        private const string InitialPointTag = "InitialPoint";
        private const string PlayerTag = "Player";

        public int Width => _width;
        public int Height => _height;

        [SerializeField] private Vector3 _originPosition = Vector3.zero;

        [SerializeField] private AreasContainer _areasContainer;
        [SerializeField] private GameObject _targetPrefab;

        [SerializeField] private float _areaSize;
        [SerializeField] private int _width;
        [SerializeField] private int _height;

        private MapModel _model;
        private GameObject[,] _areas;

        private GameObject _targetTmpInstance;

        void Awake()
        {
            _model = new MapModel(_width, _height, _areasContainer);
            _areas = new GameObject[_width, _height];
            DrawMap();
            CreateBorder();
        }

        private void CreateBorder()
        {
            PolygonCollider2D bc = gameObject.AddComponent<PolygonCollider2D>();
            bc.isTrigger = true;
            bc.points = new Vector2[]{
                new Vector2(0, 0),
                new Vector2(0, _height * _areaSize),
                new Vector2(_width * _areaSize, _height * _areaSize),
                new Vector2(_width * _areaSize, 0),
            };

            UnityEngine.Camera.main.GetComponent<CameraFollow>().SetBorder(bc);
        }

        private void DrawMap()
        {
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    _areas[i, j] = _model.Areas[i, j].CreateInstance(
                        gameObject.transform, 
                        GetWorldPosition(i, j) + new Vector3(_areaSize, _areaSize) * .5f,
                        _areaSize);
                }
            }
            GameObject.FindGameObjectWithTag(InitialPointTag).transform.position = 
                GetWorldPosition(_model.GetTownPosition().x, 
                _model.GetTownPosition().y) + new Vector3(_areaSize * .5f, _areaSize * .5f);
        }

        public bool IsPathViable(List<Area> path)
        {
            return _model.IsPathViable(path);
        }

        public void BuildEfficientPath(out int energyCost, out int timeCost, int x, int y)
        {
            _model.BuildEfficientPath(out energyCost, out timeCost, x, y);
        }

        public void RegenerateMap()
        {
            _width++;
            _height++;

            Destroy(_targetTmpInstance);

            foreach(var area in _areas)
            {
                Destroy(area);
            }

            _model = new MapModel(_width, _height, _areasContainer);
            _areas = new GameObject[_width, _height];
            DrawMap();
            UpdateBorder();
        }

        private void UpdateBorder()
        {
            PolygonCollider2D collider = gameObject.GetComponent<PolygonCollider2D>();
            collider.points = new Vector2[]{
                new Vector2(0, 0),
                new Vector2(0, _height * _areaSize),
                new Vector2(_width * _areaSize, _height * _areaSize),
                new Vector2(_width * _areaSize, 0),
            };
        }

        public Vector3 GetWorldPosition(float x, float y)
        {
            return new Vector3(x, y) * _areaSize + _originPosition;
        }

        public void GetXY(Vector3 worldPosition, out int x, out int y)
        {
            x = Mathf.FloorToInt((worldPosition - _originPosition).x / _areaSize);
            y = Mathf.FloorToInt((worldPosition - _originPosition).y / _areaSize);
        }

        public void GetRandomArea(out int x, out int y)
        {
            _model.GetRandomAreaCoord(out x, out y);
        }

        public void SetTarget(int x, int y)
        {
            print("New target set");
            _targetTmpInstance = Instantiate(
                _targetPrefab, 
                GetWorldPosition(x, y) + new Vector3(_areaSize * .5f, _areaSize * .5f), Quaternion.identity, 
                _areas[x, y].transform);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if(collision.CompareTag(PlayerTag))
            {
                if(collision.transform.position.x > _width * _areaSize)
                {
                    collision.transform.position = new Vector2(0, collision.transform.position.y);
                }
                if (collision.transform.position.x < 0)
                {
                    collision.transform.position = new Vector2(_width * _areaSize, collision.transform.position.y);
                }

                if (collision.transform.position.y > _height * _areaSize)
                {
                    collision.transform.position = new Vector2(collision.transform.position.x, 0);
                }
                if (collision.transform.position.y < 0)
                {
                    collision.transform.position = new Vector2(collision.transform.position.x, _height * _areaSize);
                }
            }
        }
    }
}