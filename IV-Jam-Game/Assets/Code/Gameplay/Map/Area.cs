using System;
using UnityEngine;

namespace Assets.Code.Gameplay.Map
{
    public enum AreaType { Town, Field, Mountain, Forest, Desert }

    [CreateAssetMenu(fileName = "AreaData", menuName = "Area")]
    public class Area : ScriptableObject, IEquatable<Area>
    {
        public int TimeCost => _timeCost;
        public int EnergyCost => _energyCost;

        public bool IsMarked = false;
        public bool IsTarget = false;

        public int X;
        public int Y;

        [SerializeField] private AreaType _type;
        [SerializeField] private int _timeCost;
        [SerializeField] private int _energyCost;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private Sprite _sprite;

        public GameObject CreateInstance(Transform parent, Vector3 position, float areaSize)
        {
            var instance = Instantiate(_prefab, position, Quaternion.identity, parent);
            instance.GetComponent<AreaController>().Initialize(this);
            instance.GetComponent<SpriteRenderer>().sprite = _sprite;
            instance.GetComponent<BoxCollider2D>().size = new Vector2(areaSize, areaSize);
            return instance;
        }

        public override string ToString()
        {
            return $"{_type}: {_timeCost}||{_energyCost}";
        }

        public float GetEfficiency()
        {
            return _timeCost / _energyCost;
        }

        public void SetCoord(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool Equals(Area other)
        {
            return other.X == X && other.Y == Y;
        }
    }
}