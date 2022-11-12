using UnityEngine;

namespace CUO.Player
{
    /// <summary>
    /// ��������� �������� ������
    /// </summary>
    public class PlayerRotation : MonoBehaviour
    {
        [SerializeField]
        private float _rotationFactorPerFrame = 1f;

        /// <summary>
        /// ����� �������� �������
        /// </summary>
        /// <param name="direction"></param>
        public void HandleRotation(Vector3 direction)
        {
            Vector3 positionToLookAt;

            positionToLookAt.x = direction.x;
            positionToLookAt.y = 0;
            positionToLookAt.z = direction.z;

            Quaternion currentRotation = transform.rotation;

            if(direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
                transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, _rotationFactorPerFrame * Time.fixedDeltaTime);
            }
        }
    }
}
