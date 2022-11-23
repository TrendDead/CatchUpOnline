using UnityEngine;
using Mirror;
using CUO.Player;
using UnityEngine.UI;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;
using System.Collections;

namespace CUO.Online
{
    public class LobbyMenu : NetworkBehaviour
    {
        /// <summary>
        /// Синглтон
        /// </summary>
        public static LobbyMenu Instance; //так-то тоже ленивая хрень этот ваш синглтон

        public readonly SyncList<Match> _matches = new SyncList<Match>();
        public readonly SyncList<string> _matchIDs = new SyncList<string>();

        [HideInInspector]
        public bool inGame;

        [SerializeField]
        private InputField _inputName;
        [SerializeField]
        private Button _hostButton;
        [SerializeField]
        private Button _joinButton;
        [SerializeField]
        private Canvas _lobbyCanvas;

        [Space(10)]
        [SerializeField]
        private Transform _playerUIParent;
        [SerializeField]
        private GameObject _playerUIPrefab; 
        [SerializeField]
        private Text _textID;
        [SerializeField]
        private Button _beginGameButton;
        [SerializeField]
        private TurnMangaer _turnMangaer;

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        private void Start()
        {
            StartCoroutine(LateStart()); //FIXME <=== Костылим потехонечку
        }

        private IEnumerator LateStart()
        {
            yield return new WaitForSeconds(0.1f);

            if (!inGame)
            {
                PlayerController[] players = FindObjectsOfType<PlayerController>();

                for (int i = 0; i < players.Length; i++)
                {
                    players[i].gameObject.transform.localScale = Vector3.zero; //FEXME: шо за нах? Дич! <==========
                    players[i].GetComponent<Rigidbody>().isKinematic = true;
                }
            }
        }

        public void Host()
        {
            _inputName.interactable = false;
            _hostButton.interactable = false;
            _joinButton.interactable = false;

            PlayerController.LocalPlayer.HostGame(); //Добавить
        }

        public void HostSuccess(bool success, string matchID)
        {
            if(success)
            {
                _lobbyCanvas.enabled = true;

                SpawnPlayerUIPrefab(PlayerController.LocalPlayer);
                _textID.text = matchID;
                _beginGameButton.interactable = true;
            }
            else
            {
                _joinButton.interactable = true;
                _hostButton.interactable = true;
                _inputName.interactable = true;
            }
        }

        public void Join()
        {
            _inputName.interactable = false;
            _hostButton.interactable = false;
            _joinButton.interactable = false;

            PlayerController.LocalPlayer.JoinGame(_inputName.text.ToUpper());
        }

        public void JoinSuccess(bool success, string matchID)
        {
            if (success)
            {
                _lobbyCanvas.enabled = true;

                SpawnPlayerUIPrefab(PlayerController.LocalPlayer);
                _textID.text = matchID;
                _beginGameButton.interactable = false;
            }
            else
            {
                _joinButton.interactable = true;
                _hostButton.interactable = true;
                _inputName.interactable = true;
            }
        }

        public bool HostGame(string matchID, PlayerController playerController)
        {
            if(!_matchIDs.Contains(matchID))
            {
                _matchIDs.Add(matchID);
                _matches.Add(new Match(matchID, playerController));

                return true;
            }
            return false;
        }

        public bool JoinGame(string matchID, PlayerController playerController)
        {
            if(_matchIDs.Contains(matchID))
            {
                for (int i = _matches.Count - 1; i >= 0; i--)
                {
                    if(_matches[i].ID == matchID)
                    {
                        _matches[i].Players.Add(playerController);
                        break;
                    }
                }

                return true;
            }
            return false;
        }

        public string GetRandomID()
        {
            string id = string.Empty;
            for (int i = 0; i < 5; i++) //кол-во игроков либо сделать полем, либо брать у мэнеджера
            {
                int rand = UnityEngine.Random.Range(0, 36);
                if(rand < 26)
                {
                    id += (char)(rand + 65);
                }
                else
                {
                    id += (rand - 26).ToString();
                }
            }
            return id;
        }

        public void SpawnPlayerUIPrefab(PlayerController player) //должен спавнить игрока но брать ui, мб придется по другому делать
        {
            GameObject newUIPlayer = Instantiate(_playerUIPrefab, _playerUIParent);
            newUIPlayer.GetComponent<PlayerUI>().SetPlayer(player);
        }

        public void StartGame()
        {
            PlayerController.LocalPlayer.BeginGame();
        }

        public void BeginGame(string matchID)
        {
            TurnMangaer newTurnManager = Instantiate(_turnMangaer);
            NetworkServer.Spawn(newTurnManager.gameObject);
            newTurnManager.GetComponent<NetworkMatch>().matchId = matchID.ToGuid();

            for (int i = 0; i < _matches.Count; i++)
            {
                if(_matches[i].ID == matchID)
                {
                    foreach (var player in _matches[i].Players)
                    {
                        PlayerController player1 = player.GetComponent<PlayerController>();
                        newTurnManager.AddPlayer(player1);
                        player1.StartGame();
                    }
                    break;
                }
            }

        }
    }

    public static class MatchExtension //Можно ли вынести?! И для чего статик?
    {
        public static Guid ToGuid(this string id)
        {
            MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
            byte[] inputBytes = Encoding.Default.GetBytes(id);
            byte[] hasBytes = provider.ComputeHash(inputBytes);

            return new Guid(hasBytes);
        }
    }

}
