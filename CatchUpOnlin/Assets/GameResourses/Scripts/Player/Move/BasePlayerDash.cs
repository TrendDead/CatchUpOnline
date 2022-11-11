using System;
using UnityEngine;

namespace CUO.Player
{
    /// <summary>
    /// Баззовый класс рывка
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public abstract class BasePlayerDash : BasePlayerMove
    {
        /// <summary>
        /// Событие совершения рывка
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
