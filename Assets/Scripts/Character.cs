using UnityEngine;

[AddComponentMenu("Quater-view Character Controller/Character")]
[RequireComponent(typeof(CapsuleCollider)), RequireComponent(typeof(Rigidbody))]
public class Character : MonoBehaviour
{
    public Rigidbody rigidbody;
    public float acceleration;
    public Vector3 moveVelocity;
    public Vector3 velocitySmooth;
    public MeshCollider currentFloor;

    private Camera _camera;

    public void OnEnable()
    {
        _camera = Camera.main;
    }

    public void FixedUpdate()
    {
        Vector3 currentVelocity = rigidbody.velocity;
        Vector3 dampedVelocity = Vector3.SmoothDamp(currentVelocity, moveVelocity, ref velocitySmooth, acceleration);
        currentVelocity.x = dampedVelocity.x;
        currentVelocity.z = dampedVelocity.z;
        rigidbody.velocity = currentVelocity;
    }

    public void SetMovementInput(Vector3 velocity)
    {
        moveVelocity = velocity;
    }

    public void AddForce(Vector3 impulseForce)
    {
    }

    public Vector3 GetMouseWorldPosition()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (currentFloor.Raycast(ray, out RaycastHit hit, 1000)) return hit.point;
        return Vector3.zero;
    }
}
