using UnityEngine;

namespace CUO.Player
{
    /// <summary>
    /// ������� ����� �������� �������� ������
    /// </summary>
    public abstract class BasePlayerMove : MonoBehaviour
    {
        /// <summary>
        /// ����������� ������������
        /// </summary>
        [HideInInspector]
        public bool IsAvailable;

        protected PlayerInput _inputSystem;

        protected virtual void Awake()
        {
            _inputSystem = new PlayerInput();
        }

        protected virtual void OnEnable()
        {
            _inputSystem.Enable();
        }

        protected virtual void OnDisable()
        {
            _inputSystem.Disable();
        }
    }
}
