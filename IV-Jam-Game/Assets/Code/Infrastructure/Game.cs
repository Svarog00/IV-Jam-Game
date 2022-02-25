namespace Assets.Code.Infrastructure
{
    class Game
    {
        public readonly GameStateMachine GameStateMachine;

        public Game(ICoroutineRunner coroutineRunner)
        {
            GameStateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), ServiceLocator.Container);

            
        }
    }
}