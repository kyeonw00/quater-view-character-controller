using UnityEngine;

public class SphereCastTest : MonoBehaviour
{
    public LayerMask obstacleLayerMask;
    public float sphereRadius;
    public float collisionCheckLength;
    public float skinWidth = 0.02f;
    public QueryTriggerInteraction triggerInteraction;

    private int _collisionHitCount = 0;
    private RaycastHit[] _collisionHits = new RaycastHit[8];

    public void OnDrawGizmosSelected()
    {
        _collisionHitCount = Physics.SphereCastNonAlloc(transform.position, sphereRadius, Vector3.down, _collisionHits, collisionCheckLength + sphereRadius, obstacleLayerMask, triggerInteraction);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sphereRadius);

        for (int i = 0; i < _collisionHitCount; i++)
        {
            Vector3 dirToHitPoint = (_collisionHits[i].point - transform.position).normalized;
            // Vector3 dirToHitPoint = _collisionHits[i].normal * -1 * sphereRadius;

            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + dirToHitPoint * sphereRadius);
            
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(_collisionHits[i].point, 0.15f);
            Gizmos.DrawLine(_collisionHits[i].point, _collisionHits[i].point + dirToHitPoint * -_collisionHits[i].distance);
        }
    }
}
