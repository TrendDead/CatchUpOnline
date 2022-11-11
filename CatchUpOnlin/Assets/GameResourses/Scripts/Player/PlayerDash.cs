using UnityEngine;

namespace CUO.Player
{
    /// <summary>
    /// ��������� ����� ������
    /// </summary>
    public class PlayerDash : MonoBehaviour, IAvailable
    {
        /// <summary>
        /// ����������� ������������
        /// </summary>
        public bool IsAvailable { get; set; }

        [SerializeField]
        private float _distanceDash;

        private PlayerInput _inputSystem;

        private void Awake()
        {
            _inputSystem = new PlayerInput();
        }

        private void OnEnable()
        {
            _inputSystem.Enable();
            _inputSystem.Player.Dash.performed += context => Dash();
        }

        private void OnDisable()
        {
            _inputSystem.Disable();
            _inputSystem.Player.Dash.performed -= context => Dash();
        }

        private void Dash()
        {

        }
    }
}
