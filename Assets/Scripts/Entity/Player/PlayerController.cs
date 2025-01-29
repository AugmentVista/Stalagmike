using Assets.Scripts;
using Assets.Scripts.Struct;
using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
class PlayerController : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] GroundDetector groundDetector;
    InputManager inputManager = new();
    Rigidbody2D rb;
    AbilityController abilityController;
    Action PhysicsProcess = delegate { };

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
    bool grounded = true;
    int jumpTicksLeft = 0;
    //#endregion

    private void Start()
    {
        // Event stuff.
        groundDetector.Landed += () => { grounded = true; };
    }

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        abilityController = GetComponent<AbilityController>();
    }

    private void FixedUpdate()
    {
        HandleMovement();
        PhysicsProcess();
    }

    private void HandleMovement()
    {
        // Get current and target velocity
        Vector2 velocity = rb.velocity;
        float targetVel = inputManager.moveValue * moveSpeed;

        velocity.x = Mathf.Lerp(targetVel, velocity.x, Time.fixedDeltaTime);

        // Do jumpy things here.
        if (inputManager.jumpInput && grounded) { jumpTicksLeft = jumpTicks; grounded = false; }
        if (inputManager.jumpInput && jumpTicksLeft > 0) { velocity.y += jumpForce; jumpTicksLeft--; }

        // Reassign when done.
        rb.velocity = velocity;

        // Set flipped (or not) state.
        //IsFlipped = velocity.x < 0;
        if (velocity.x < 0) { IsFlipped = true; }
        else if (velocity.x > 0) { IsFlipped = false; }
    }
}