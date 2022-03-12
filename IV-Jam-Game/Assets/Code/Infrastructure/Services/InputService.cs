using UnityEngine;

namespace Assets.Code.Infrastructure
{
    public class InputService : IInputService
    {
        public bool IsLeftMouseButtonDown() => Input.GetMouseButtonDown(0);

        public bool IsPassPathButtonDown() => Input.GetKeyDown(KeyCode.Space);

        public bool IsRightMouseButtonDown() => Input.GetMouseButtonDown(1);
    }
}