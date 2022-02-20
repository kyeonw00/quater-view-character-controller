using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEditor.UIElements;
using UnityEngine;

public class SphereCastTest : MonoBehaviour
{
    public LayerMask obstacleLayerMask;
    public float sphereRadius;
    public float collisionCheckLength;
    public QueryTriggerInteraction triggerInteraction;

    private int _collisionHitCount = 0;
    private RaycastHit[] _collisionHits = new RaycastHit[8];

    public void OnDrawGizmosSelected()
    {
        _collisionHitCount = Physics.SphereCastNonAlloc(transform.position + Vector3.up * sphereRadius, sphereRadius, Vector3.down, _collisionHits, collisionCheckLength + sphereRadius, obstacleLayerMask, triggerInteraction);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sphereRadius);

        // if (hitSomething)
        // {
        //     Gizmos.color = Color.green;
        //     Gizmos.DrawWireSphere(collisionHit.point, 0.25f);
        // }

        /* overlap casting visualizations...
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sphereRadius);
        Gizmos.DrawWireSphere(transform.position + Vector3.down * collisionCheckLength, sphereRadius);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, sphereRadius + collisionCheckLength);
        */

        Gizmos.color = Color.green;
        for (int i = 0; i < _collisionHitCount; i++)
        {
            Gizmos.DrawWireSphere(_collisionHits[i].point, 0.15f);
            Gizmos.DrawLine(_collisionHits[i].point, _collisionHits[i].point + _collisionHits[i].normal);
        }
    }
}
