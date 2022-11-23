using UnityEngine;
using Mirror;
using System.Collections.Generic;
using CUO.Player;
using UnityEngine.UI;

namespace CUO.Online
{
    /// <summary>
    /// Компонент отображении имени игрока в лобби
    /// </summary>
    public class PlayerUI : MonoBehaviour
    {
        [SerializeField]
        private Text _playerName;

        private PlayerController _player;

        public void SetPlayer(PlayerController player)
        {
            _player = player;
            _playerName.text = "Name";
        }
    }
}
