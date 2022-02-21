using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GravityChangeTest : MonoBehaviour
{
    public LayerMask collisionMask;
    public Vector3 moveDirection;
    public float speed;
    public float sphereRadius;
    public bool applyRaycastLength;

    private Vector3 _velocity;
    private int _collisionHitCount;
    private RaycastHit[] _collisionHits = new RaycastHit[8];

    public void FixedUpdate()
    {
        _velocity = moveDirection * speed * Time.deltaTime;

        float raycastLength = applyRaycastLength ? _velocity.sqrMagnitude : 0;
        _collisionHitCount = Physics.SphereCastNonAlloc(transform.position, sphereRadius, moveDirection, _collisionHits, raycastLength, collisionMask);

        if (_collisionHitCount > 0)
        {
            Debug.Log("Hit something");
            if (_collisionHits[0].distance < 0)
            {
                Vector3 adjustVelocity = _collisionHits[0].normal * _velocity.sqrMagnitude;
                _velocity.x += adjustVelocity.x;
                _velocity.z += adjustVelocity.z;
            }
        }

        transform.Translate(_velocity);
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, sphereRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + _velocity);

        for (int i = 0; i < _collisionHitCount; i++)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(_collisionHits[i].point, _collisionHits[i].point + _collisionHits[i].normal);
            Gizmos.DrawWireSphere(_collisionHits[i].point, 0.15f);
        }
    }
}
