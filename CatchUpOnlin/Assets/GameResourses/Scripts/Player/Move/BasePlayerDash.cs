using System;
using UnityEngine;

namespace CUO.Player
{
    /// <summary>
    /// �������� ����� �����
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public abstract class BasePlayerDash : BasePlayerMove
    {
        /// <summary>
        /// ������� ���������� �����
        /// </summary>
        public abstract event Action<bool> IsDash;

        protected override void OnEnable()
        {
            base.OnEnable();
            _inputSystem.Player.Dash.performed += context => Dash();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _inputSystem.Player.Dash.performed -= context => Dash();
        }

        protected abstract void Dash();
    }
}
