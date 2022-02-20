using System.Collections;
using System.Collections.Generic;
using KinematicCharacterController;
using UnityEngine;

public class RaycastVisualization : MonoBehaviour
{
    public Vector3 direction;
    public float sphereRadius;
    public float maxDistance;
    public Color debugRayColor;
    public LayerMask collisionMask;

    private int _hitResultsCount;
    private RaycastHit[] _hitResults = new RaycastHit[8];

    public void OnDrawGizmos()
    {
        _hitResultsCount = Physics.SphereCastNonAlloc(transform.position, sphereRadius, direction, _hitResults, maxDistance, collisionMask, QueryTriggerInteraction.Collide);

        Gizmos.color = debugRayColor;
        if (_hitResultsCount != 0)
        {
            for (int i = 0; i < _hitResultsCount; i++)
            {
                Gizmos.DrawWireSphere(transform.position + direction * _hitResults[i].distance, sphereRadius);
                Gizmos.color = Color.green;
                Gizmos.DrawLine(transform.position, transform.position + direction * _hitResults[i].distance);
            }
        }
        else
        {
            Gizmos.color = debugRayColor;
            Gizmos.DrawLine(transform.position, transform.position + direction * maxDistance);
        }
    }
}
