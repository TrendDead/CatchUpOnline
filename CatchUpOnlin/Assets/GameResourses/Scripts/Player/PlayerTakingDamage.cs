using UnityEngine;
using System.Collections;
using Mirror;

namespace CUO.Player
{
    /// <summary>
    /// Компонент получения урона игроком
    /// </summary>
    public class PlayerTakingDamage : NetworkBehaviour
    {
        public bool IsCanTakeDamage { get; private set; }

        [SerializeField]
        private float _recoveryTime = 3;
        [SerializeField]
        private Material _defaultMaterial;
        [SerializeField]
        private Material _damageMaterial;

        private Renderer _playerMaterial;

        private void Start()
        {
            _playerMaterial = GetComponent<Renderer>();
            IsCanTakeDamage = true;
        }


        [Client]
        public void GetDamage()
        {
            if(IsCanTakeDamage)
            {
                StartCoroutine(RecoveryTime());
            }
        }

        [Command(requiresAuthority = false)]
        public void Test()
        {
            StartCoroutine(RecoveryTime());
        }


        private IEnumerator RecoveryTime()
        {
            _playerMaterial.material = _damageMaterial;
            IsCanTakeDamage = false;
            yield return new WaitForSeconds(_recoveryTime);
            _playerMaterial.material = _defaultMaterial;
            IsCanTakeDamage = true;
        }

    }
}
