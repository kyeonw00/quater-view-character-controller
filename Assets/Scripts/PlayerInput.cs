using UnityEngine;

[AddComponentMenu("Quater-view Character Controller/Player Input")]
[RequireComponent(typeof(Character))]
public class PlayerInput : MonoBehaviour
{
    public Character character;
    public float moveSpeed;
    public float runSpeed;
    public float dashForce;

    private bool _runEnabled;

    public void Update()
    {
        OnDirectionalInput(new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")));

        if (Input.GetKeyDown(KeyCode.LeftShift)) OnRunKeyDown();
        if (Input.GetKeyUp(KeyCode.LeftShift)) OnRunKeyUp();

        if (Input.GetKeyDown(KeyCode.Space)) OnDashInput();
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

    public void OnDashInput()
    {
        character.AddForce(new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")) * dashForce);
    }
}