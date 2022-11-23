using UnityEngine;
using Mirror;
using System.Collections.Generic;
using CUO.Player;

namespace CUO.Online
{
    public class TurnMangaer : MonoBehaviour
    {
        private List<PlayerController> _players = new List<PlayerController>();

        public void AddPlayer(PlayerController player)
        {
            _players.Add(player);
        }
    }
}
