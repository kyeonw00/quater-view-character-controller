using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    public Character character;
    public float moveSpeed;

    public void Update()
    {
        OnDirectionalInput(new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")));
    }

    public void OnDirectionalInput(Vector3 directionalInput)
    {
        character.SetMovementInput(directionalInput * moveSpeed);
    }
}