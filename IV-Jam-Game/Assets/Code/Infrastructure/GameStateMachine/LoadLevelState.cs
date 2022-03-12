using Assets.Code.Gameplay.Camera;
using UnityEngine;

namespace Assets.Code.Infrastructure
{
    class LoadLevelState : IPayloadState<string>
    {
        private const string InitialPointTag = "InitialPoint";

        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, IGameFactory gameFactory)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
        }

        public void Enter(string payload)
        {
            _sceneLoader.Load(payload, OnLoaded);
        }

        private void OnLoaded()
        {
            _gameFactory.CreateHud();
            CreateMap();
            SpawnPlayer();

            _gameStateMachine.Enter<GameLoopState>();
        }

        public void Exit()
        {
            
        }

        private void CreateMap()
        {
            _gameFactory.CreateMap();
        }

        private void SpawnPlayer()
        {
            var player = _gameFactory.InstantiateHero(GameObject.FindGameObjectWithTag(InitialPointTag));
            CameraFollow(player);
        }

        private void CameraFollow(GameObject player)
        {
            Camera.main.
                GetComponent<CameraFollow>().
                Follow(player);
        }
    }
}