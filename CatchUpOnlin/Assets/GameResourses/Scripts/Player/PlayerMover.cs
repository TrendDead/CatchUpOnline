using UnityEngine;

namespace CUO.Player
{
    /// <summary>
    /// Компонент движения игрока
    /// </summary>
    public class PlayerMover : MonoBehaviour, IAvailable
    {
        /// <summary>
        /// Возможность перемещаться
        /// </summary>
        public bool IsAvailable { get; set; }

        [SerializeField]
        private float _moveSpeed;

        private PlayerInput _inputSystem;


        private void Awake()
        {
            _inputSystem = new PlayerInput();
        }

        private void OnEnable()
        {
            _inputSystem.Enable();
        }

        private void OnDisable()
        {
            _inputSystem.Disable();
        }

        private void Update()
        {
            Vector2 direction = _inputSystem.Player.Move.ReadValue<Vector2>();

            if(IsAvailable)
            {
                Move(direction);
            }
        }

        private void Move(Vector2 direction)
        {
            float scaleMoveSpeed = _moveSpeed * Time.deltaTime;

            Vector3 moveDirection = new Vector3(direction.x, 0f, direction.y);
            transform.position += moveDirection * scaleMoveSpeed;
        }
    }
}
