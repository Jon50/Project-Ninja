using UnityEngine;

public class Raycasters : MonoBehaviour
{
    [SerializeField] private float _maxGroundDistance = 5f;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _jumpPadLayer;
    [SerializeField] private LayerMask _pickupLayer;

    private Transform _thisTransform;

    private void Awake()
    {
        _thisTransform = GetComponent<Transform>();
    }


    public float MaxGroundDistance { get => _maxGroundDistance; }


    public RaycastHit GroundHit
    {
        get
        {
            if (_thisTransform == null)
                return new RaycastHit();

            IsOnGround = Physics.Raycast(_thisTransform.position, -_thisTransform.up, out var _groundHit, _maxGroundDistance, _groundLayer);
            return _groundHit;
        }
    }

    public bool IsOnGround { get; private set; }

    public RaycastHit JumpPadHit
    {
        get
        {
            IsOnJumpPad = Physics.Raycast(_thisTransform.position, -_thisTransform.up + _thisTransform.forward, out var _jumpPadHit, _maxGroundDistance, _jumpPadLayer);
            return _jumpPadHit;
        }
    }

    public bool IsOnJumpPad { get; private set; }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, GroundHit.point);
    }
#endif
}
