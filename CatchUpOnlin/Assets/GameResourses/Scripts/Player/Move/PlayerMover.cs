using UnityEngine;

namespace CUO.Player
{
    /// <summary>
    /// Компонент движения игрока
    /// </summary>
    [RequireComponent(typeof(PlayerRotation))]
    public class PlayerMover : BasePlayerMove
    {
        [SerializeField]
        private float _moveSpeed;

        private PlayerRotation _playerRotation;

        protected override void Awake()
        {
            base.Awake();
            _playerRotation = GetComponent<PlayerRotation>();
        }

        private void Update()
        {
            Vector2 direction = _inputSystem.Player.Move.ReadValue<Vector2>();

            if (direction != Vector2.zero)
            {
                Vector3 directionWithCamera = AddCameraAngle(direction);

                if (IsAvailable)
                {
                    _playerRotation.HandleRotation(directionWithCamera);
                    Move(directionWithCamera);
                }
            }
        }

        private void Move(Vector3 direction)
        {
            float scaleMoveSpeed = _moveSpeed * Time.deltaTime;
            Vector3 moveDirection = new Vector3(direction.x, 0f, direction.z);
            transform.position += moveDirection * scaleMoveSpeed;
        }
    }
}
