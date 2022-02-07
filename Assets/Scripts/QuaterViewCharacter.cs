using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuaterViewCharacter : MonoBehaviour
{
    public float walkSpeed;
    public float runSpeed;
    public float acceleration;

    private Vector3 _directionalInput = new Vector3();
    private Vector3 _velocity;
    private Vector3 _dampVelocity;

    public Vector3 Velocity => _velocity;

    public void Update()
    {
        SetDirectionalInput();
    }

    public void FixedUpdate()
    {
        CalculateVelocity();
        Move(Time.fixedDeltaTime);
    }

    public void SetDirectionalInput()
    {
        _directionalInput.x = Input.GetAxisRaw("Horizontal");
        _directionalInput.z = Input.GetAxisRaw("Vertical");
    }

    public void CalculateVelocity()
    {
        Vector3 targetVelocity = _directionalInput * walkSpeed;
        _velocity = Vector3.SmoothDamp(_velocity, targetVelocity, ref _dampVelocity, acceleration);
    }

    public void Move(float deltaTime)
    {
        Vector3 velocityInFrame = _velocity * deltaTime;
        transform.Translate(velocityInFrame);
    }
}
