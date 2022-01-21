using UnityEngine;

[AddComponentMenu("Quater-view Character Controller/Character")]
[RequireComponent(typeof(CapsuleCollider)), RequireComponent(typeof(Rigidbody))]
public class Character : MonoBehaviour
{
    public Rigidbody rigidbody;
    public float acceleration;
    public Vector3 moveVelocity;
    public Vector3 forceVelocity;
    public float minimumForceScale;
    public Vector3 velocitySmooth;
    public MeshCollider currentFloor;

    private Camera _camera;
    private bool _forceApplied;
    private bool _processingForceVelocity;

    public void OnEnable()
    {
        _camera = Camera.main;
    }

    public void FixedUpdate()
    {
        CalculateVelocity();
    }

    public void SetMovementInput(Vector3 velocity)
    {
        moveVelocity = velocity;
    }

    public void AddForce(Vector3 impulseForce)
    {
        forceVelocity = impulseForce;
        _forceApplied = true;
        _processingForceVelocity = true;
    }

    public Vector3 GetMouseWorldPosition()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (currentFloor.Raycast(ray, out RaycastHit hit, 1000)) return hit.point;
        return Vector3.zero;
    }

    public void CalculateVelocity()
    {
        Vector3 currentVelocity = rigidbody.velocity;
        Vector3 targetMoveVelocity = moveVelocity;
        Vector3 dampedVelocity;

        if (_forceApplied)
        {
            _forceApplied = false;
            _processingForceVelocity = true;
            velocitySmooth = Vector3.zero;
            targetMoveVelocity = forceVelocity;
            currentVelocity = forceVelocity;
        }

        if (_processingForceVelocity)
        {
            if (currentVelocity.sqrMagnitude < minimumForceScale)
            {
                _processingForceVelocity = false;
                forceVelocity = Vector3.zero;
            }
            else
            {
                targetMoveVelocity = Vector3.zero;
            }
        }

        dampedVelocity = Vector3.SmoothDamp(currentVelocity, targetMoveVelocity, ref velocitySmooth, acceleration);
        currentVelocity.x = dampedVelocity.x;
        currentVelocity.z = dampedVelocity.z;
        rigidbody.velocity = currentVelocity;
    }
}
