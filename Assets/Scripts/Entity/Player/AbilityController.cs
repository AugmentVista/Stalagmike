using Assets.Scripts;
using Assets.Scripts.Entity.Util;
using System;
using UnityEngine;

internal class AbilityController : MonoBehaviour
{
    [Header("Refs")]
    Rigidbody2D rb;
    PlayerController pc;
    Action PhysicsProcess = delegate { };

    [Header("Melee Stats")]
    [SerializeField] GameObject meleeAttack;
    [SerializeField] Hitbox meleeHitbox;
    [SerializeField] HitInfo meleeHitInfo;
    [SerializeField] int meleeCooldownTicks = 15;
    [SerializeField] int meleeActiveTicks = 5;

    [Header("Ranged Stats")]
    [SerializeField] GameObject ammo;
    [SerializeField] float startingVelocityScale = 5;
    Vector2 tempStartVector { get { return ShouldAttackBeFlipped() ? Vector2.left : Vector2.right; } }
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
            if (InputManager.standardAttackInput) { UseMeleeAtk(); attackCooldown = meleeCooldownTicks; }
            else if (InputManager.specialAttackInput) { UseRangedAtk(); attackCooldown = rangedCooldownTicks; }
        }
        else { attackCooldown--; }
    }

    private void UseMeleeAtk()
    {
        // Create a vector3 to set our rotation.
        Vector3 angles = Vector3.zero;
        if (ShouldAttackBeFlipped()) { angles = new Vector3(0, 180, 0); }
        meleeAttack.transform.localEulerAngles = angles;
        meleeAttack.SetActive(true);

        // Set a timer to count down how many ticks the attack hitbox will be active if it doesn't hit anything.
        int activeTicksLeft = meleeActiveTicks;

        // Use a method so we can deactivate and unsubscribe later.
        void AtkProcess()
        {
            //Debug.Log("ticks left = " + activeTicksLeft);
            activeTicksLeft--;
            if (activeTicksLeft <= 0)
            {
                // meleeAttack.SetActive(false) ;
                PhysicsProcess -= AtkProcess;
            }
        }

        // Add the method.
        PhysicsProcess += AtkProcess;
    }

    private void OnMeleeHit(HealthSystem target)
    {
        if (target.gameObject != gameObject)
        {
            target.TakeDamage(meleeHitInfo);
            meleeAttack.SetActive(false);
        }
    }

    private void UseRangedAtk()
    {
        // Create ammo at our position, and get relevant components.
        GameObject projectile = Instantiate(ammo, transform);
        Hitbox projectileHitbox = projectile.GetComponent<Hitbox>();
        Rigidbody2D projectileRB = projectile.GetComponent<Rigidbody2D>();

        // Set start velocity.
        projectileRB.velocity = tempStartVector * startingVelocityScale;

        // Set hit behavior.
        projectileHitbox.OnHit += OnRangedHit;

        void OnRangedHit(HealthSystem target)
        {
            if (target.gameObject != gameObject)
            {
                target.TakeDamage(rangedHitInfo);
                Destroy(projectile);
            }
        }
    }

    bool ShouldAttackBeFlipped()
    {
        bool value;
        float vel = rb.velocity.x;
        if (vel > 0) { value = false; }
        else if (vel < 0) { value = true; }
        else { value = pc.IsFlipped; }
        return value;
    }
}