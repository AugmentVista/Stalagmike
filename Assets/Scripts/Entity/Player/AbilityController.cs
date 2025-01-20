using Assets.Scripts;
using Assets.Scripts.Struct;
using System;
using UnityEngine;

internal class AbilityController : MonoBehaviour
{
    [Header("Refs")]
    InputManager inputManager = new();
    Rigidbody2D rb;
    PlayerController pc;
    AbilityController abilityController;
    Action PhysicsProcess = delegate { };

    [Header("Melee Stats")]
    [SerializeField] Hitbox meleeHitbox;
    [SerializeField] HitInfo meleeHitInfo;
    [SerializeField] int meleeCooldownTicks = 15;
    [SerializeField] int meleeActiveTicks = 5;

    [Header("Ranged Stats")]
    [SerializeField] GameObject ammo;
    [SerializeField] HitInfo rangedHitInfo;
    [SerializeField] int rangedCooldownTicks = 30;

    // Misc Attack Stuff
    int attackCooldown = 0;

    private void Start()
    {
        // Event stuff.
        meleeHitbox.OnHit += OnMeleeHit;
    }

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        pc = GetComponent<PlayerController>();
        abilityController = GetComponent<AbilityController>();
    }
    private void FixedUpdate()
    {
        HandleAttacks();
        PhysicsProcess();
    }

    private void HandleAttacks()
    {
        //Debug.Log($"Is Atk pressed? {inputManager.standardAttackInput}. Is SpA pressed? {inputManager.specialAttackInput}.");

        // If there is no cooldown, check for attack input. Otherwise, decrement the cooldown.
        if (attackCooldown < 1)
        {
            // If we have an attack input, use the attack and set the cooldown counter to the relevant cooldown length.
            if (inputManager.standardAttackInput) { UseMeleeAtk(); attackCooldown = meleeCooldownTicks; }
            else if (inputManager.specialAttackInput) { UseRangedAtk(); attackCooldown = rangedCooldownTicks; }
        }
        else { attackCooldown--; }
    }

    private void UseMeleeAtk()
    {
        bool shouldRotate;
        // Determine if we need to rotate the hitbox. If our last motion was backwards, we should be flipped, and our hitbox should be too.
        float vel = rb.velocity.x;
        if (vel > 0) { shouldRotate = false; }
        else if (vel < 0) { shouldRotate = true; }
        else { shouldRotate = pc.IsFlipped; }

        // Create a vector3 to set our rotation.
        Vector3 angles = Vector3.zero;
        if (shouldRotate) { angles = new Vector3(0, 180, 0); }
        meleeHitbox.transform.localEulerAngles = angles;
        meleeHitbox.enabled = true;

        // Set a timer to count down how many ticks the attack hitbox will be active if it doesn't hit anything.
        int activeTicksLeft = meleeActiveTicks;

        // Use a method so we can deactivate and unsubscribe later.
        void AtkProcess()
        {
            //Debug.Log("ticks left = " + activeTicksLeft);
            activeTicksLeft--;
            if (activeTicksLeft <= 0)
            {
                meleeHitbox.enabled = false;
                PhysicsProcess -= AtkProcess;
            }
        }

        // Add the method.
        PhysicsProcess += AtkProcess;
    }

    private void OnMeleeHit(HealthSystem target)
    {
        target.TakeDamage(meleeHitInfo);
        meleeHitbox.enabled = false;
    }

    private void UseRangedAtk()
    {
        throw new NotImplementedException();
    }
}