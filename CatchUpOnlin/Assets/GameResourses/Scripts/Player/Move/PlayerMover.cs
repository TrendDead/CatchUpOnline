using UnityEngine;

namespace CUO.Player
{
    /// <summary>
    /// Компонент движения игрока
    /// </summary>
    public class PlayerMover : BasePlayerMove
    {
        [SerializeField]
        private float _moveSpeed;

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
