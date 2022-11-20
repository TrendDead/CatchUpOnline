using UnityEngine;
using Mirror;

namespace CUO.Player
{
    /// <summary>
    /// Комапонент атаки игрока
    /// </summary>
    public class PlayerAttack : NetworkBehaviour
    {
        [SyncVar]
        private bool _isDash = false;
        private int _countSuccessAttack = 0;

        /// <summary>
        /// Обнуление счетчика
        /// </summary>
        public void CounterReset()
        {
            _countSuccessAttack = 0;
            _isDash = false;
        }

        /// <summary>
        /// Обновление информации о рывке
        /// </summary>
        public void UpdateDashInfo(bool dash) => _isDash = dash;

        //private void OnTriggerEnter(Collider other)
        //{
        //    if (other.gameObject.TryGetComponent(out PlayerTakingDamage otherPlayer))
        //    {
        //        if(otherPlayer.IsCanTakeDamage && _isDash)
        //        {
        //            otherPlayer.GetDamage();
        //            otherPlayer.Test();
        //            _countSuccessAttack++;
        //        }
        //    }
        //}

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out PlayerTakingDamage otherPlayer))
            {
                if(otherPlayer.IsCanTakeDamage && _isDash)
                {
                    otherPlayer.GetDamage();
                    otherPlayer.Test();
                    _countSuccessAttack++;
                }
}
        }
    }
}
