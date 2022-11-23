using Mirror;
using CUO.Player;
using System;
using System.Collections.Generic;

namespace CUO.Online
{
    [Serializable]
    public class Match : NetworkBehaviour
    {
        public string ID;
        public readonly List<PlayerController> Players = new List<PlayerController>();

        public Match(string id, PlayerController player)
        {
            ID = id;
            Players.Add(player);
        }
    }
}
