using UnityEngine;

namespace Assets.Code.Infrastructure
{
    public interface IGameFactory : IService
    {
        void CreateMap();
        GameObject InstantiateHero(GameObject position);
        void CreateHud();
    }
}