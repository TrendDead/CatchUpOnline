using UnityEngine;
using System.Collections;

namespace CUO.Player
{
    /// <summary>
    /// Компонент получения урона игроком
    /// </summary>
    public class PlayerTakingDamage : MonoBehaviour
    {
        [SerializeField]
        private float _recoveryTime = 3;
        [SerializeField]
        private Material _defaultMaterial;
        [SerializeField]
        private Material _damageMaterial;

        private Renderer _playerMaterial;
        private bool IsCanTakeDamage = true;

        private void Start()
        {
            _playerMaterial = GetComponent<Renderer>();
        }

        public bool GetDamage()
        {
            if(IsCanTakeDamage)
            {
                StartCoroutine(RecoveryTime());
                return true;
            }
            return false;
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
