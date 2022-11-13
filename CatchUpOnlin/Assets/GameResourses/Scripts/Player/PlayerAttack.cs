using UnityEngine;

namespace CUO.Player
{
    /// <summary>
    /// ���������� ����� ������
    /// </summary>
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField]
        private int _countSuccessAttack = 0;
        private bool _isDash = false;

        /// <summary>
        /// ��������� ��������
        /// </summary>
        public void CounterReset()
        {
            _countSuccessAttack = 0;
            _isDash = false;
        }

        /// <summary>
        /// ���������� ���������� � �����
        /// </summary>
        public void UpdateDashInfo(bool dash) => _isDash = dash;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out PlayerTakingDamage otherPlayer) && _isDash)
            {
                if(otherPlayer.GetDamage())
                {
                    _countSuccessAttack++;
                }
            }
        }
    }
}
