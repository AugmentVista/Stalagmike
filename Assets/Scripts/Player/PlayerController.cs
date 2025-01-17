using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
class PlayerController : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] GroundDetector groundDetector;
    InputManager inputManager = new();
    Rigidbody2D rb;

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
    [Header("Melee Stats")]
    [SerializeField] Hitbox meleeHitbox;
    [SerializeField] int meleeCooldownTicks = 15;
    [Header("Ranged Stats")]
    [SerializeField] GameObject ammo;
    [SerializeField] int rangedCooldownTicks = 30;
    // Misc Attack Stuff
    int attackCooldown = 0;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();

        groundDetector.Landed += () => { grounded = true; };
    }

    private void FixedUpdate()
    {
        HandleMovement();
        HandleAttacks();
    }

    private void HandleAttacks()
    {
        Debug.Log($"Is Atk pressed? {inputManager.standardAttackInput}. Is SpA pressed? {inputManager.specialAttackInput}.");

        // If there is no cooldown, check for attack input. Otherwise, decrement the cooldown.
        if (rangedCooldownTicks < 1)
        {
            // If we have an attack input, use the attack and set the cooldown counter to the relevant cooldown length.
            if (inputManager.standardAttackInput) { UseMeleeAtk(); attackCooldown = meleeCooldownTicks; }
            else if (inputManager.specialAttackInput) { UseRangedAtk(); attackCooldown = rangedCooldownTicks; }
        }
        else { attackCooldown--; }
    }

    private void UseMeleeAtk()
    {
        throw new NotImplementedException();
    }
    private void UseRangedAtk()
    {
        throw new NotImplementedException();
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
        IsFlipped = velocity.x < 0;
    }
}