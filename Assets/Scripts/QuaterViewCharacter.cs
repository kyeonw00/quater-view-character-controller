using UnityEngine;

public class QuaterViewCharacter : MonoBehaviour
{
    // public new Rigidbody rigidbody;
    public new CapsuleCollider collider;
    public float gravity;
    public float walkSpeed;
    public float runSpeed;
    public float acceleration;
    public float airControl;
    public LayerMask obstacleLayers;
    public float collideMinimumDistance;
    public float maxClimbableAngle;

    [Header("Debug Settings")]
    public bool enableVelocityVisualization;
    public bool enableBottomCollidierVisualization;
    public bool enableHitNormalVisualization;

    private Vector3 _directionalInput = new Vector3();
    private Vector3 _velocity = new Vector3();
    private Vector3 _dampVelocity = new Vector3();
    private float _gravityVelocity;
    private bool _grounded;
    private int _collisionCheckHitCount;
    private RaycastHit[] _collisionCheckHit = new RaycastHit[8];

    public Vector3 Velocity => _velocity;
    public int CollisionCheckHitCount => _collisionCheckHitCount;
    public RaycastHit[] CollisionCheckHit => _collisionCheckHit;

    internal void Reset()
    {
        // TryGetComponent(out rigidbody);
        TryGetComponent(out collider);

        gravity = 0;
        walkSpeed = 0;
        runSpeed = 0;
        acceleration = 0;
        obstacleLayers = 0;
    }

    internal void OnEnable()
    {
        _gravityVelocity = -2 * gravity / Mathf.Pow(airControl, 2);
    }

    internal void Update()
    {
        SetDirectionalInput();
    }

    internal void FixedUpdate()
    {
        CalculateVelocity();
        CheckIfGrounded();
        Move(Time.fixedDeltaTime);
    }

    public void SetDirectionalInput()
    {
        _directionalInput.x = Input.GetAxisRaw("Horizontal");
        _directionalInput.z = Input.GetAxisRaw("Vertical");
    }

    public void CalculateVelocity()
    {
        Vector3 velocity = _velocity;
        Vector3 targetVelocity = _directionalInput * walkSpeed;
        float acc = _grounded ? acceleration : airControl;

        _velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocity.x, ref _dampVelocity.x, acc);
        _velocity.y = velocity.y + _gravityVelocity;
        _velocity.z = Mathf.SmoothDamp(velocity.z, targetVelocity.z, ref _dampVelocity.z, acc);
    }

    public void CheckCollisions()
    {
    }

    public void CheckIfGrounded()
    {
        float gravityVelocity = _velocity.y * Time.fixedDeltaTime;
        // Vector3 bottomVertPoint = transform.position + Vector3.down * (collider.height / 2) + gravityVelocity;
        Vector3 bottomPoint = transform.position + Vector3.down * (collider.height / 2 - collider.radius) * gravityVelocity;
        _collisionCheckHitCount = Physics.SphereCastNonAlloc(bottomPoint, collider.radius, Vector3.down, _collisionCheckHit, Mathf.Abs(_velocity.y), obstacleLayers);

        /// <remarks>
        /// Based on collision hit visualization, Collision test returns weired result.
        /// (0, 0, 0) is returning.
        /// </remarks>

        Debug.DrawLine(bottomPoint, bottomPoint + Vector3.up * gravityVelocity, Color.blue);

        if (_collisionCheckHitCount > 0)
        {
            for (int i = 0; i < _collisionCheckHitCount; i++)
            {
                if (enableHitNormalVisualization)
                {
                    Debug.DrawLine(_collisionCheckHit[i].point, _collisionCheckHit[i].point + _collisionCheckHit[i].normal, Color.yellow);
                }

                if (_collisionCheckHit[i].distance <= collideMinimumDistance)
                {
                    _grounded = true;
                    _velocity.y = _collisionCheckHit[i].distance;
                }

                if (GetSlopeAngle(_collisionCheckHit[i].normal, Vector3.up) > maxClimbableAngle)
                {
                    /* Make the character to slide down the hit slope */
                    _velocity.x += _collisionCheckHit[i].normal.x * _velocity.x;
                    _velocity.z += _collisionCheckHit[i].normal.z * _velocity.z;
                }
            }
        }
        else
        {
            _grounded = false;
        }
    }

    public void Move(float deltaTime)
    {
        Vector3 velocityInFrame = _velocity * deltaTime;
        transform.Translate(velocityInFrame);
    }

    public float GetSlopeAngle(Vector3 normalVector, Vector3 fromVector)
    {
        return Vector3.Angle(fromVector, normalVector);
    }
}
