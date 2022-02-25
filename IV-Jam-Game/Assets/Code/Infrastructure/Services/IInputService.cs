namespace Assets.Code.Infrastructure
{
    public interface IInputService : IService
    {
        bool IsLeftMouseButtonDown();
        bool IsRightMouseButtonDown();
    }
}