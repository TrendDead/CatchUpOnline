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

        private void Awake()
        {
            _playerDash.IsDash += AvailablePlayerControl;
        }

        private void OnDestroy()
        {
            _playerDash.IsDash -= AvailablePlayerControl;
        }

        private void Start()
        {

            AvailablePlayerControl(true);
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
