using UnityEngine;
using Mirror;

namespace CUO.Player
{
    /// <summary>
    /// Базовый класс контроля движения игрока
    /// </summary>
    public abstract class BasePlayerMove : NetworkBehaviour
    {
        /// <summary>
        /// Возможность перемещаться
        /// </summary>
        [HideInInspector]
        public bool IsAvailable;

        protected PlayerInput _inputSystem;

        private CameraRotateController _cameraRotateController;

        /// <summary>
        /// Передача камеры
        /// </summary>
        public void GetCamera(CameraRotateController newCamera)
        {
            _cameraRotateController = newCamera;
        }

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

        [Client]
        protected Vector3 AddCameraAngle(Vector2 direction)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg + _cameraRotateController.transform.eulerAngles.y;
            return (Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward).normalized;
        }
    }
}
