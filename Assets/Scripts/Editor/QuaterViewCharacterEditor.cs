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
        if (!_character.enableVelocityVisualization)
        {
            return;
        }

        Handles.color = Color.green;
        Handles.ArrowHandleCap(0, _character.transform.position, _character.transform.rotation * Quaternion.LookRotation(_character.Velocity), 2f, EventType.Repaint);
    }

    
    [DrawGizmo(GizmoType.Active | GizmoType.Selected)]
    internal static void DrawColliderBottomPointGizmos(QuaterViewCharacter src, GizmoType _)
    {
        if (!src.enableBottomCollidierVisualization)
        {
            return;
        }

        Vector3 bottomPoint = src.transform.position + Vector3.down * (src.collider.height / 2 - src.collider.radius) + (Vector3.up * src.Velocity.y * Time.fixedDeltaTime);

        Gizmos.color = Color.red;
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
            Handles.Label(hits[i].point, $"{hits[i].collider.name} - {hits[i].point}");
        }
    }
}
