using CUO.Player;
using UnityEngine;

/// <summary>
///  онтроллер вращени€ камеры вокруг цели
/// </summary>
public class CameraRotateController : MonoBehaviour
{
    
    public PlayerController Target;

    [SerializeField]
    private float _radius;
    [SerializeField]
    private Vector2 _displacement;
    [SerializeField]
    private float _sensitivity = 3;
    [SerializeField]
    private float _limitYMax = 80;
    [SerializeField]
    private float _limitYMin = 4;

    private Vector3 offset;
    private PlayerInput _inputSystem;
    private float x, y;

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

    private void Start()
    {
        _limitYMax = Mathf.Abs(_limitYMax);
        if (_limitYMax > 90) _limitYMax = 90;
        offset = new Vector3(_displacement.x, _displacement.y, -Mathf.Abs(_radius));
        transform.position = Target.transform.position + offset;
    }

    void Update()
    {
        Vector2 scroll = _inputSystem.Player.Look.ReadValue<Vector2>();

        x = transform.localEulerAngles.y + scroll.x * _sensitivity;
        y += scroll.y * _sensitivity;
        y = Mathf.Clamp(y, -_limitYMax, _limitYMin);
        transform.localEulerAngles = new Vector3(-y, x, 0);
        transform.position = transform.localRotation * offset + Target.transform.position;
    }

    private void OnDrawGizmos()
    {
        if(Target != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(Target.transform.position, _radius);
        }
    }
}
