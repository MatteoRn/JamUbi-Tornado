using System;
using UnityEngine;
using UnityEngine.Windows;

public class CarController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] public float forwardSpeed = 10f;
    [SerializeField] public float turnSpeed = 150f;
    [SerializeField] public float driftFactor = 0.95f;

    private Rigidbody2D _RigidBody;
    private PlayerInputActions _PlayerInput;
    private float _MoveInput;

    private float initialRotation;
    private void Awake()
    {
        _PlayerInput = new PlayerInputActions();
    }

    void OnEnable()
    {
        _PlayerInput.Enable();
        _PlayerInput.Movement.Move.performed += ctx => _MoveInput = ctx.ReadValue<float>();
        _PlayerInput.Movement.Move.canceled += ctx => _MoveInput = 0;
    }
    void OnDisable()
    {
        _PlayerInput.Disable();
    }

    void Start()
    {
        _RigidBody = GetComponent<Rigidbody2D>();
        initialRotation = _RigidBody.rotation;
    }

    void FixedUpdate()
    {
        _RigidBody.linearVelocity = transform.up * forwardSpeed;

        float steer = _MoveInput;
        float newRot = _RigidBody.rotation - steer * turnSpeed * Time.fixedDeltaTime;
        float clamped = Mathf.Clamp(newRot, initialRotation - 70f, initialRotation + 70f);

        _RigidBody.MoveRotation(clamped);

        ApplyDrift();
    }

    void ApplyDrift()
    {

        Vector2 lForwardVel = transform.up * Vector2.Dot(_RigidBody.linearVelocity, transform.up);
        Vector2 lSideVel = transform.right * Vector2.Dot(_RigidBody.linearVelocity, transform.right);
        _RigidBody.linearVelocity = lForwardVel + lSideVel * driftFactor;
    }
}
