using UnityEngine;

namespace CUO.Player
{
    /// <summary>
    /// Базовый класс контроля движения игрока
    /// </summary>
    public abstract class BasePlayerMove : MonoBehaviour
    {
        /// <summary>
        /// Возможность перемещаться
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
