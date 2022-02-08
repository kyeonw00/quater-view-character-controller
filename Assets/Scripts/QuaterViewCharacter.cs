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

    private Vector3 _directionalInput = new Vector3();
    private Vector3 _velocity = new Vector3();
    private Vector3 _dampVelocity = new Vector3();
    private float _gravityVelocity;
    private bool _grounded;

    public Vector3 Velocity => _velocity;

    public void Reset()
    {
        TryGetComponent(out rigidbody);
        TryGetComponent(out collider);

        gravity = 0;
        walkSpeed = 0;
        runSpeed = 0;
        acceleration = 0;
        obstacleLayers = 0;
    }

    public void OnEnable()
    {
        _gravityVelocity = (-2 * gravity) / Mathf.Pow(airControl, 2);
    }

    public void Update()
    {
        SetDirectionalInput();
    }

    public void FixedUpdate()
    {
        CalculateVelocity();
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
        _grounded = true;
    }

    public void Move(float deltaTime)
    {
        Vector3 velocityInFrame = _velocity * deltaTime;
        transform.Translate(velocityInFrame);
    }
}
