using UnityEngine;

namespace Assets.Code.Gameplay.Map
{
    public enum AreaType { Town, Field, Mountain, Forest, Desert }

    [CreateAssetMenu(fileName = "AreaData", menuName = "Area")]
    public class Area : ScriptableObject
    {
        [SerializeField] private AreaType _type;
        [SerializeField] private int _timeCost;
        [SerializeField] private int _energyCost;
        [SerializeField] private GameObject _prefab;

        public void CreateInstance(Vector3 position)
        {
            Instantiate(_prefab, position, Quaternion.identity);
        }

        public override string ToString()
        {
            return $"{_type}: {_timeCost}||{_energyCost}";
        }
    }
}