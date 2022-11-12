using System;
using UnityEngine;

namespace CUO.Player
{
    /// <summary>
    /// Рывок передачей ипульса
    /// </summary>
    public class PlayerDashImpulse : BasePlayerDash
    {
        /// <summary>
        /// Событие соверщения рывка
        /// </summary>
        public override event Action<bool> IsDash = delegate { };

        [SerializeField]
        private float _distanceDash;
        [SerializeField]
        private float _upForce;

        private Rigidbody _rigidbody;
        private bool isGrounded = true;

        protected override void Awake()
        {
            base.Awake();
            _rigidbody = GetComponent<Rigidbody>();
        }

        protected override void Dash()
        {
            Vector2 direction = _inputSystem.Player.Move.ReadValue<Vector2>();

            if (direction != Vector2.zero && isGrounded && IsAvailable)
            {
                Vector3 directionWithCamera = AddCameraAngle(direction);
                _rigidbody.AddForce(directionWithCamera.x * _distanceDash, _upForce, directionWithCamera.z * _distanceDash, ForceMode.Acceleration);
                isGrounded = false;
                IsDash?.Invoke(false);
            }

        }

       private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Ground" && !isGrounded)
            {
                IsDash?.Invoke(true);
                _rigidbody.velocity = new Vector3(0, 0, 0);
                isGrounded = true;
            }
        }
    }
}
