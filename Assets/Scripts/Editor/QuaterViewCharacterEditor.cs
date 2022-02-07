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
}
