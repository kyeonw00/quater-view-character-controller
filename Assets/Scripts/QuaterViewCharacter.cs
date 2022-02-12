using UnityEngine;

public class QuaterViewCharacter : MonoBehaviour
{
    public new Rigidbody rigidbody;
    public new CapsuleCollider collider;
    public float gravity;
    public float walkSpeed;
    public float runSpeed;
    public float acceleration;
    public float airControl;
    public LayerMask obstacleLayers;
    public float collideMinimumDistance;

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
        TryGetComponent(out rigidbody);
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
        Vector3 bottomPoint = transform.position + collider.center + Vector3.down * (collider.height / 2 - collider.radius);
        _collisionCheckHitCount = Physics.SphereCastNonAlloc(bottomPoint, collider.radius, Vector3.down, _collisionCheckHit, Mathf.Abs(_velocity.y), obstacleLayers);

        /// <remarks>
        /// 이미 terrain과 콜라이더가 겹쳐진 상태에서 collision checking 진행해도 hit point는 두 콜라이더 사이의 정규화된 지점으로 반환됨
        /// <code>_grounded</code>가 <value>true</value> 일 때, hit point 기준으로 콜라이더가 서로 겹쳐져 있지 않은지 확인하는 로직 필요
        /// </remarks>

        if (_collisionCheckHitCount > 0)
        {
            for (int i = 0; i < _collisionCheckHitCount; i++)
            {
                if (_collisionCheckHit[i].distance <= collideMinimumDistance)
                {
                    _grounded = true;
                    _velocity.y = _collisionCheckHit[i].distance;
                    _dampVelocity.y = _collisionCheckHit[i].distance;
                }

                if ((_collisionCheckHit[i].point - bottomPoint).sqrMagnitude < Mathf.Pow(collider.radius, 2))
                {
                    Vector3 dirToBottomPoint = (bottomPoint - _collisionCheckHit[i].point).normalized;
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
}
