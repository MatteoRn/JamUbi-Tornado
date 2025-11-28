using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Windows;

public class CarController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] public float forwardSpeed = 10f;
    [SerializeField] public float turnSpeed = 150f;
    [SerializeField] public float driftFactor = 0.95f;
    [SerializeField] public float boostPower = 5f;
    [SerializeField] public float boostWaitTime = 5f;

    private Rigidbody2D _RigidBody;
    private PlayerInputActions _PlayerInput;
    private float _MoveInput;

    public float resetVelocityTime = 3f;

    public UnityEvent OnResetVelocity = new UnityEvent();

    public static CarController Instance { get; private set; }

    private float initialRotation;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
        _PlayerInput = new PlayerInputActions();
    }

    void OnEnable()
    {
        _PlayerInput.Enable();
        _PlayerInput.Movement.Move.performed += ctx => _MoveInput = ctx.ReadValue<float>();
        _PlayerInput.Movement.Move.canceled += ctx => _MoveInput = 0;

        _PlayerInput.Movement.Boost.started += ctx =>
        {
            if (!canBoost) return;

            isStopVelocity = true;

            if (isStopVelocity) StartCoroutine(ResetVelocity());
            _RigidBody.AddForce(transform.up * boostWaitTime, ForceMode2D.Impulse);
            canBoost = false;
            StartCoroutine(RefillBoost());
        };
    }

    bool canBoost = true;
    IEnumerator RefillBoost()
    {
        yield return new WaitForSeconds(resetVelocityTime);
        canBoost = true;
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
        if (isStopVelocity) return;
        _RigidBody.linearVelocity = transform.up * forwardSpeed;

        float steer = _MoveInput;
        float newRot = _RigidBody.rotation - steer * turnSpeed * Time.fixedDeltaTime;
        float clamped = Mathf.Clamp(newRot, initialRotation - 70f, initialRotation + 70f);

        _RigidBody.MoveRotation(clamped);

        ApplyDrift();
    }
    bool isStopVelocity = false;
    public void SetEnableVelocity()
    {
        isStopVelocity = !isStopVelocity;

        if (isStopVelocity) StartCoroutine(ResetVelocity());
    }

    IEnumerator ResetVelocity()
    {
        yield return new WaitForSeconds(resetVelocityTime);
        isStopVelocity = false;
        OnResetVelocity.Invoke();
    }

    void ApplyDrift()
    {

        Vector2 lForwardVel = transform.up * Vector2.Dot(_RigidBody.linearVelocity, transform.up);
        Vector2 lSideVel = transform.right * Vector2.Dot(_RigidBody.linearVelocity, transform.right);
        _RigidBody.linearVelocity = lForwardVel + lSideVel * driftFactor;
    }
    
}
