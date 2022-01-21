using UnityEngine;

[AddComponentMenu("Quater-view Character Controller/Player Input")]
[RequireComponent(typeof(Character))]
public class PlayerInput : MonoBehaviour
{
    public Character character;
    public float moveSpeed;
    public float runSpeed;

    private bool _runEnabled;

    public void Update()
    {
        OnDirectionalInput(new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")));

        if (Input.GetKeyDown(KeyCode.LeftShift)) OnRunKeyDown();
        if (Input.GetKeyUp(KeyCode.LeftShift)) OnRunKeyUp();
    }

    public void OnDirectionalInput(Vector3 directionalInput)
    {
        float targetSpeed = _runEnabled ? runSpeed : moveSpeed;
        character.SetMovementInput(directionalInput * targetSpeed);
    }

    public void OnRunKeyDown()
    {
        _runEnabled = true;
    }

    public void OnRunKeyUp()
    {
        _runEnabled = false;
    }
}