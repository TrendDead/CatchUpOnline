using UnityEngine;

namespace CUO.Online
{
    /// <summary>
    /// ��������� ��������� �����
    /// </summary>
    public class LobbyOn : MonoBehaviour
    {
        [SerializeField]
        private GameObject _canvas;

        private void Start()
        {
            _canvas.SetActive(true);
        }
    }
}
