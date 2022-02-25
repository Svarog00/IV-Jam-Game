using UnityEngine;

namespace Assets.Code.Gameplay.Map
{
    [CreateAssetMenu(fileName = "AreaCollection", menuName = "AreaCollection")]
    public class AreasContainer : ScriptableObject
    {
        [SerializeField] private Area[] _areasCollection;

        public Area GetRandomArea()
        {
            int index = Random.Range(1, 4);
            return _areasCollection[index];
        }

        public Area GetTownArea()
        {
            return _areasCollection[0];
        }
    }
}