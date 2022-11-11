using UnityEngine;
using System.Collections.Generic;

namespace CUO.Player
{
    /// <summary>
    /// ���������� ������
    /// </summary>
    [RequireComponent(typeof(PlayerMover), typeof(PlayerDash))]
    public class PlayerController : MonoBehaviour
    {
        //TODO: �������� ����������� ���������� ����������� ���������� ���������� ������� 
        //public List<IAvailable> Availables => _availables;

        private PlayerMover _playerMover;
        private PlayerDash _playerDash;
        private List<IAvailable>  _availables;

        private void Awake()
        {
            _playerMover = GetComponent<PlayerMover>();

            //TODO: ���������� ������� �������
            /*Debug.Log(_playerMover.IsAvailable);
            Debug.Log(_playerMover.GetComponent<IAvailable>());
            Debug.Log(_playerMover.GetComponent<IAvailable>().IsAvailable);
            _availables.Add(_playerMover.GetComponent<IAvailable>());*/

            _playerDash = GetComponent<PlayerDash>();
           // _availables.Add(_playerDash.IsAvailable);
        }

        private void Start()
        {
            AvailablePlayerControl(true);
        }

        public void AvailablePlayerControl(bool isAvalible)
        {
            //Debug.Log(isAvalible);
            _playerMover.IsAvailable = isAvalible;
            _playerDash.IsAvailable = isAvalible;
        }
    }
}
