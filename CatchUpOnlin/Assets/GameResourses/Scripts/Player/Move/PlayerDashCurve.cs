using System;
using System.Collections;
using UnityEngine;

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

        private bool isGrounded = true;


        protected override void Dash()
        {
            Vector2 direction = _inputSystem.Player.Move.ReadValue<Vector2>();

            if (direction != Vector2.zero && isGrounded)
            {
                StartCoroutine(LineDash(new Vector3(direction.x, 0f, direction.y)));
                IsDash?.Invoke(false);
            }
        }

        private IEnumerator LineDash(Vector3 direction)
        {
            isGrounded = false;
            var startPosition = transform.position;
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
            }
            IsDash?.Invoke(true);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Ground" && !isGrounded)
            {
                IsDash?.Invoke(true);
                isGrounded = true;
            }
        }
    }
}
