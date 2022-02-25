using UnityEngine;

namespace Assets.Code.Infrastructure
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;

        public GameFactory(IAssetProvider assetProvider)
        {
            _assets = assetProvider;
        }

        public void CreateHud()
        {
            _assets.Instantiate(AssetPaths.HudPath);
        }

        public GameObject InstantiateHero(GameObject position)
        {
            return _assets.Instantiate(AssetPaths.HeroPath, position.transform.position);
        }

        public void CreateMap()
        {
            _assets.Instantiate(AssetPaths.MapPath);
        }
    }
}