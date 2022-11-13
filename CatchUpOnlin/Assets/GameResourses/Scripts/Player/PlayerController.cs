using UnityEngine;

namespace CUO.Player
{
    /// <summary>
    /// Контроллер игрока
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private BasePlayerMove _playerMover;
        [SerializeField]
        private BasePlayerDash _playerDash;
        [SerializeField]
        private PlayerJump _playerJump;
        [SerializeField]
        private PlayerAttack _playerAttack;

        private void Awake()
        {
            _playerDash.IsDash += AttackAttempt;
        }

        private void OnDestroy()
        {
            _playerDash.IsDash -= AttackAttempt;
        }

        private void Start()
        {
            AvailablePlayerControl(true);
        }

        private void AttackAttempt(bool isAvalible)
        {
            AvailablePlayerControl(isAvalible);
            _playerAttack.UpdateDashInfo(!isAvalible);
        }

        /// <summary>
        /// Достут управления игроком
        /// </summary>
        /// <param name="isAvalible"></param>
        public void AvailablePlayerControl(bool isAvalible)
        {
            _playerMover.IsAvailable = isAvalible;
            _playerDash.IsAvailable = isAvalible;
            _playerJump.IsAvailable = isAvalible;
        }
    }
}
