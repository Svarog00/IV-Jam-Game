using UnityEngine;

namespace Assets.Code.Gameplay.Map
{
    public enum AreaType { Town, Field, Mountain, Forest, Desert }

    [CreateAssetMenu(fileName = "AreaData", menuName = "Area")]
    public class Area : ScriptableObject
    {
        public int TimeCost => _timeCost;
        public int EnergyCost => _energyCost;

        [SerializeField] private AreaType _type;
        [SerializeField] private int _timeCost;
        [SerializeField] private int _energyCost;
        [SerializeField] private GameObject _prefab;

        public void CreateInstance(Vector3 position)
        {
            var instance = Instantiate(_prefab, position, Quaternion.identity);
            instance.GetComponent<AreaController>().Initialize(this);
        }

        public override string ToString()
        {
            return $"{_type}: {_timeCost}||{_energyCost}";
        }

        public float GetEfficiency()
        {
            return _timeCost/_energyCost;
        }
    }
}