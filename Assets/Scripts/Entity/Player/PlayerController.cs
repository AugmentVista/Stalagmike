using Assets.Scripts;
using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
class PlayerController : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] GroundDetector groundDetector;
    [SerializeField] Animator animator;
    Rigidbody2D rb;
    AbilityController abilityController;
    Action PhysicsProcess = delegate { };

    [Header("Spawning and Checkpoints")]
    public Transform RespawnPoint; // This needs some logic for setting the spawn point.

    [Header("Movement Stats")]
    [SerializeField] float moveSpeed = 3;
    [SerializeField] int jumpTicks = 8;
    [SerializeField] float jumpForce = 0.65f;
    /// <summary>
    /// Are we backwards?
    /// </summary>
    /// I don't know if we need this as publicly readable, but we'll have it there just in case.
    public bool IsFlipped { get; private set; } = false;
    //#region movementFields
    bool grounded = false;
    int jumpTicksLeft = 0;
    //#endregion

    private void Start()
    {
        // Event stuff.
        groundDetector.Landed += () => { grounded = true; };

        if (TryGetComponent(out HealthSystem healthSystem))
        {
            healthSystem.OnDeath += delegate { transform.position = RespawnPoint.position; };
        }
    }

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        abilityController = GetComponent<AbilityController>();
    }

    private void FixedUpdate()
    {
        HandleMovement();
        HandleAnimation();
        PhysicsProcess();
    }

    private void HandleMovement()
    {
        // Get current and target velocity
        Vector2 velocity = rb.velocity;
        float targetVel = InputManager.moveValue * moveSpeed;

        velocity.x = Mathf.Lerp(targetVel, velocity.x, Time.fixedDeltaTime);

        // Do jumpy things here.
        if (InputManager.jumpInput && grounded) { jumpTicksLeft = jumpTicks; grounded = false; }
        if (InputManager.jumpInput && jumpTicksLeft > 0) { velocity.y += jumpForce; jumpTicksLeft--; }

        // Reassign when done.
        rb.velocity = velocity;

        // Set flipped (or not) state.
        //IsFlipped = velocity.x < 0;
        if (velocity.x < 0) { IsFlipped = true; }
        else if (velocity.x > 0) { IsFlipped = false; }
    }

    private void HandleAnimation()
    {
        animator.SetFloat(0, rb.velocity.x);
        if (InputManager.jumpInput && jumpTicksLeft > 0) { animator.SetTrigger(1); }
    }
}