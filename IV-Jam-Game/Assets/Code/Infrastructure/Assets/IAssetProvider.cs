using UnityEngine;

namespace Assets.Code.Infrastructure
{
    public interface IAssetProvider : IService
    {
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, Vector3 postition);
    }
}