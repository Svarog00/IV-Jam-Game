using UnityEngine;

namespace Assets.Code.Infrastructure
{
    public class AssetProvider : IAssetProvider
    {
        public GameObject Instantiate(string path)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }

        public GameObject Instantiate(string path, Vector3 postition)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, postition, Quaternion.identity);
        }
    }
}