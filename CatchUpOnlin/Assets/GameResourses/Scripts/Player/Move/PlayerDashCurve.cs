using System;
using System.Collections;
using UnityEngine;
using Mirror;

namespace CUO.Player
{
    /// <summary>
    /// Рывок по кривой
    /// </summary>
    public class PlayerDashCurve : BasePlayerDash
    {
        public override event Action<bool> IsDash = delegate { };

        [SerializeField]
        private float _distanceDash;
        [SerializeField]
        private float _timeDash;
        [SerializeField]
        private float _upForce;
        [SerializeField]
        private AnimationCurve _dashCurve;
        [SerializeField]
        private float _rollbackTime = 1;

        private Rigidbody _rigidbody;
        private bool isGrounded = true;
        private bool isCanDash = true;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        [Client]
        protected override void Dash()
        {
            Vector2 direction = _inputSystem.Player.Move.ReadValue<Vector2>();

            if (direction != Vector2.zero && isGrounded && isCanDash)
            {
                Vector3 directionWithCamera = AddCameraAngle(direction);

                StartCoroutine(LineDash(new Vector3(directionWithCamera.x, 0f, directionWithCamera.z)));
                StartCoroutine(RollbackDash(_rollbackTime));
            }
        }

        private IEnumerator RollbackDash(float WhaitTime)
        {
            isCanDash = false;
            yield return new WaitForSeconds(WhaitTime);
            isCanDash = true;
        }

        private IEnumerator LineDash(Vector3 direction)
        {
            IsDash?.Invoke(false);
            isGrounded = false;

            _rigidbody.velocity = Vector3.zero;

            var startPosition = transform.position;

  

            for (float i = 0; i < _timeDash; i += Time.deltaTime)
            {
                if (isGrounded)
                {
                    break;
                }

                float lerpRatio = i / _timeDash;
                Vector3 positionOffset = (_dashCurve.Evaluate(lerpRatio) * direction) + ((Vector3.up * _dashCurve.Evaluate(lerpRatio)) * _upForce);
                transform.position = Vector3.Lerp(startPosition, startPosition + (direction * _distanceDash), lerpRatio) + positionOffset;

                yield return null;

            }

            _rigidbody.velocity = (direction + Vector3.down) * 20;

           IsDash?.Invoke(true);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if ((collision.gameObject.tag == "Ground") && !isGrounded)
            {
                _rigidbody.velocity = Vector3.zero;
                isGrounded = true;
            }
        }

    }
}
