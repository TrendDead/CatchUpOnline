using Mirror;
using UnityEngine;

namespace CUO.Player
{
    /// <summary>
    /// Контроллер игрока
    /// </summary>
    public class PlayerController : NetworkBehaviour
    {
        private const string REMOTELAYER = "RemotePlayer";

        [SerializeField]
        private BasePlayerMove _playerMover;
        [SerializeField]
        private BasePlayerDash _playerDash;
        [SerializeField]
        private PlayerJump _playerJump;
        [SerializeField]
        private PlayerAttack _playerAttack;
        [SerializeField]
        private CameraRotateController _cameraRotateControllerPrefab;

        private CameraRotateController _cameraRotateController;

        private void OnDestroy()
        {
            _playerDash.IsDash -= AttackAttempt;
            _playerDash.IsDash -= CmdAttackAttempt;
            Camera.main.gameObject.SetActive(true);
            Destroy(_cameraRotateController);
        }

        private void Start()
        {
            _playerDash.IsDash += AttackAttempt;
            _playerDash.IsDash += CmdAttackAttempt;
            if (isLocalPlayer)
            {
                Camera.main.gameObject.SetActive(false);
                _cameraRotateController = Instantiate(_cameraRotateControllerPrefab, gameObject.transform.position, Quaternion.identity);
                _cameraRotateController.Target = this;
                _playerMover.GetCamera(_cameraRotateController);
                _playerJump.GetCamera(_cameraRotateController);
                AvailablePlayerControl(true);
            }
            else
            {
                gameObject.layer = LayerMask.NameToLayer(REMOTELAYER);
                _playerMover.enabled = false;
                //_playerDash.enabled = false;
                _playerJump.enabled = false;
                AvailablePlayerControl(false);
            }

            _playerDash.GetCamera(_cameraRotateController);

            transform.name = $"Player {GetComponent<NetworkIdentity>().netId}";
        }

        [Command(requiresAuthority = false)] //Костыль так-то
        private void CmdAttackAttempt(bool isAvalible)
        {
            AvailablePlayerControl(isAvalible);
            _playerAttack.UpdateDashInfo(!isAvalible);
        }

        [Client]
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
