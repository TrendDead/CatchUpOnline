using System.Collections;
using UnityEngine;

namespace CUO.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerJump : BasePlayerMove //FIXME: Попытаться сделать через кривую
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
            _rigidbody.AddForce(Vector3.up * _jumpHeight);
           // _rigidbody.velocity = Vector3.up * _jumpHeight; //TODO: сделать контролируетмый прыжок по кривой
        }

        private IEnumerator LineDash()
        {
            isGrounded = false;
            Vector2 direction = _inputSystem.Player.Move.ReadValue<Vector2>();
           
            var startPosition = transform.position;

            /*for (float i = 0; i < _timeJump; i += Time.deltaTime)
            {
                float lerpRatio = i / _timeJump;
                Vector3 positionOffset = (_dashCurve.Evaluate(lerpRatio) * direction) + ((Vector3.up * _dashCurve.Evaluate(lerpRatio)) * _upForce);
                transform.position = Vector3.Lerp(startPosition, startPosition + (direction * _distanceDash), lerpRatio) + positionOffset;

                yield return null;

                if (isGrounded)
                {
                    break;
                }
            }

            for (float i = 0; i < _timeDash; i += Time.deltaTime)
            {
                float lerpRatio = i / _timeDash;
                Vector3 positionOffset = (_dashCurve.Evaluate(lerpRatio) * direction) + ((Vector3.up * _dashCurve.Evaluate(lerpRatio)) * _upForce);
                transform.position = Vector3.Lerp(startPosition, startPosition + (direction * _distanceDash), lerpRatio) + positionOffset;

                yield return null;

                if (isGrounded)
                {
                    break;
                }
            }*/
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
