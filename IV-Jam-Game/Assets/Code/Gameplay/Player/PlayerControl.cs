using Assets.Code.Infrastructure;
using UnityEngine;

namespace Assets.Code.Gameplay.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    class PlayerControl : MonoBehaviour
    {
        [SerializeField] private float _speed = 2f;
        private Animator _animator;

        private IInputService _inputService;
        private PlayerMovement _playerMovement;
        private Rigidbody2D _rb2;


        private void Start()
        {
            _rb2 = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();

            _inputService = ServiceLocator.Container.Single<IInputService>();
            _playerMovement = new PlayerMovement(_rb2, gameObject.transform, _speed, _animator);
        }

        private void Update()
        {
            if(_inputService.IsRightMouseButtonDown())
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
