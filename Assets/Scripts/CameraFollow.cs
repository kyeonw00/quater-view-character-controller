using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform followTarget;
    public Vector3 offset;
    public Quaternion tilt;

    public void LateUpdate()
    {
        transform.position = followTarget.position + offset;
        transform.rotation = tilt;
    }
}
