using System.Collections;
using UnityEngine;

namespace CUO.Player
{
    /// <summary>
    /// Компонент прыжка персанажа
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerJump : BasePlayerMove
    {
        [SerializeField]
        private float _jumpHeight;
        [SerializeField]
        private float _timeJump;
        [SerializeField]
        private AnimationCurve _speedJumpCurve;

        private Rigidbody _rigidbody;
        private bool isGrounded = true;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            _inputSystem.Player.Jump.performed += context => Jump();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _inputSystem.Player.Jump.performed -= context => Jump();
        }

        private void Jump()
        {
            if(IsAvailable && isGrounded)
            {
                StartCoroutine(CurveJump());
            }
        }

        private IEnumerator CurveJump()
        {
            isGrounded = false;
           
            var startPosition = transform.position;

            for (float i = 0; i < _timeJump; i += Time.deltaTime)
            {
                float lerp = i / _timeJump;
                _rigidbody.velocity = (Vector3.up * _speedJumpCurve.Evaluate(lerp)) * _jumpHeight;

                yield return null;

                if (isGrounded)
                {
                    break;
                }
            }
                yield return null;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Ground" && !isGrounded)
            {
                isGrounded = true;
            }
        }
    }
}
