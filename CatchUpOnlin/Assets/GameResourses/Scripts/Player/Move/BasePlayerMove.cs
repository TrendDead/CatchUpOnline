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
        [SerializeField]
        private CameraRotateController _cameraRotateController; // не везде он нужен, лучше убрать

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

        protected Vector3 AddCameraAngle(Vector2 direction)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg + _cameraRotateController.transform.eulerAngles.y;
            return (Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward).normalized;
        }
    }
}
