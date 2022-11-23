using UnityEngine;
using Mirror;

namespace CUO.Online
{
    /// <summary>
    /// Компонент подключения
    /// </summary>
    public class Connection : MonoBehaviour
    {
        [SerializeField]
        private NetworkManager _networkManager;

        private void Start()
        {
            if (!Application.isBatchMode)
            {
                _networkManager.StartClient();
            }
        }

        public void JoinClient()
        {
            _networkManager.networkAddress = "localhost";
            _networkManager.StartClient();
        }
    }
}
