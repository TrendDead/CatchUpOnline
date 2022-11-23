using CUO.Online;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CUO.Player
{
    /// <summary>
    /// Контроллер игрока
    /// </summary>
    public class PlayerController : NetworkBehaviour
    {
        private const string REMOTELAYER = "RemotePlayer";

        public CameraRotateController PlayerCamera;

        [Header("Player Componetns")]
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

        [Header("Online Components")]
        [SyncVar]
        public string MatchID;
        public static PlayerController LocalPlayer; //лениво что ппц, переделать

        private NetworkMatch _networkMatch;

        private void OnDestroy()
        {
            _playerDash.IsDash -= AttackAttempt;
            _playerDash.IsDash -= CmdAttackAttempt;
            Destroy(PlayerCamera);
        }

        public void HostGame()
        {
            string id = LobbyMenu.Instance.GetRandomID();
            CmdHostGame(id);
        }

        [Command]
        private void CmdHostGame(string id)
        {
            MatchID = id;
            if (LobbyMenu.Instance.HostGame(id, this))
            {
                Debug.Log("Lobby was created successfully");
                _networkMatch.matchId = id.ToGuid();
                TargetHostGame(true, id);
            }
            else
            {
                Debug.Log("Error in lobby creation");
                TargetHostGame(false, id);
            }
        }

        [TargetRpc]
        private void TargetHostGame(bool success, string id)
        {
            MatchID = id;
            LobbyMenu.Instance.HostSuccess(success, id);
        }

        public void JoinGame(string inputID)
        {
            CmdJoinGame(inputID);
        }

        [Command]
        private void CmdJoinGame(string id)
        {
            MatchID = id;
            if (LobbyMenu.Instance.JoinGame(id, this))
            {
                Debug.Log("Join was successfully");
                _networkMatch.matchId = id.ToGuid();
                TargetJoinGame(true, id);
            }
            else
            {
                Debug.Log("Error in join");
                TargetJoinGame(false, id);
            }
        }

        [TargetRpc]
        private void TargetJoinGame(bool success, string id)
        {
            MatchID = id;
            LobbyMenu.Instance.JoinSuccess(success, id);
        }

        public void BeginGame()
        {
            CmdBeginGame();
        }

        [Command]
        private void CmdBeginGame()
        {
            LobbyMenu.Instance.BeginGame(MatchID);
            Debug.Log("Start game");
        }

        public void StartGame()
        {
            TargetBeginGame();
        }

        [TargetRpc]
        private void TargetBeginGame()
        {
            Debug.Log($"ID  {MatchID} | start");
            DontDestroyOnLoad(gameObject);
            LobbyMenu.Instance.inGame = true;
            transform.localScale = Vector3.one;

            SceneManager.LoadScene("GameScene", LoadSceneMode.Additive);

            GetComponent<Rigidbody>().isKinematic = false;
            Debug.Log("Спавн камеры");
            PlayerCamera = Instantiate(_cameraRotateControllerPrefab, gameObject.transform.position, Quaternion.identity);
            PlayerCamera.Target = this;

            _playerMover.GetCamera(PlayerCamera);
            _playerJump.GetCamera(PlayerCamera);
            _playerDash.GetCamera(PlayerCamera);

            Camera.main.gameObject.SetActive(false);
        }

        private void Start()
        {
            _networkMatch = GetComponent<NetworkMatch>();

            _playerDash.IsDash += AttackAttempt;
            _playerDash.IsDash += CmdAttackAttempt;
            if (isLocalPlayer)
            {
                LocalPlayer = this;

                //Camera.main.gameObject.SetActive(false);
                //PlayerCamera = Instantiate(_cameraRotateControllerPrefab, gameObject.transform.position, Quaternion.identity);
                //PlayerCamera.Target = this;
                //_playerMover.GetCamera(PlayerCamera);
                //_playerJump.GetCamera(PlayerCamera);
                AvailablePlayerControl(true);
            }
            else
            {
                LobbyMenu.Instance.SpawnPlayerUIPrefab(this);

                gameObject.layer = LayerMask.NameToLayer(REMOTELAYER);
                _playerMover.enabled = false;
                //_playerDash.enabled = false;
                _playerJump.enabled = false;
                AvailablePlayerControl(false);
            }

            //_playerDash.GetCamera(PlayerCamera);

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
