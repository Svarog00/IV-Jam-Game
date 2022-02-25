using Assets.Code.Infrastructure;
using UnityEngine;

namespace Assets.Code.Gameplay.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    class PlayerControl : MonoBehaviour
    {
        private IInputService _inputService;
        private PlayerMovement _playerMovement;
        private Rigidbody2D _rb2;

        private void Start()
        {
            _rb2 = GetComponent<Rigidbody2D>();

            _inputService = ServiceLocator.Container.Single<IInputService>();
            _playerMovement = new PlayerMovement(_rb2, gameObject.transform, 2f);
        }

        private void Update()
        {
            if(_inputService.IsLeftMouseButtonDown())
            {
                _playerMovement.SetPoint(UtilitiesClass.GetWorldMousePosition());
            }
        }

        private void FixedUpdate()
        {
            _playerMovement.Move();
        }
    }
}
