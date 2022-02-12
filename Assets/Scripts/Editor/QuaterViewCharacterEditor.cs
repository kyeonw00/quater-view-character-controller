using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(QuaterViewCharacter))]
public class QuaterViewCharacterEditor : Editor
{
    private QuaterViewCharacter _character;

    public void OnEnable()
    {
        _character = target as QuaterViewCharacter;
    }

    public void OnSceneGUI()
    {
        Handles.color = Color.green;
        Handles.ArrowHandleCap(0, _character.transform.position, _character.transform.rotation * Quaternion.LookRotation(_character.Velocity), 2f, EventType.Repaint);
    }

    
    [DrawGizmo(GizmoType.Active | GizmoType.Selected)]
    internal static void DrawColliderBottomPointGizmos(QuaterViewCharacter src, GizmoType _)
    {
        Vector3 bottomPoint = src.transform.position + src.collider.center + Vector3.down * (src.collider.height / 2 - src.collider.radius + 0.015f);
        
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(bottomPoint, src.collider.radius);
    }

    [DrawGizmo(GizmoType.Active | GizmoType.Selected)]
    internal static void DrawCollisionHitsGizmos(QuaterViewCharacter src, GizmoType _)
    {
        RaycastHit[] hits = src.CollisionCheckHit;

        Gizmos.color = Color.magenta;
        for (int i = 0; i < src.CollisionCheckHitCount; i++)
        {
            Gizmos.DrawWireSphere(hits[i].point, 0.15f);
        }
    }
}
