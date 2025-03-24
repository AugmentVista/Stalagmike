using Assets.Scripts;
using Assets.Scripts.Entity.Util;
using System;
using UnityEngine;

/// <summary>
/// Controls the hitbox of an attack.
/// </summary>
[RequireComponent(typeof(Collider2D))]
internal class Hitbox : MonoBehaviour
{
    Collider2D trigger;

    //public Collider2D secretCollider; // Uuuh I will worry about it.

    public Action<HealthSystem> OnHit = delegate { };

    public Action<TileBreakableSystem, Vector3> OnTileHit = delegate { };

    [SerializeField] GameObject blockBreakHitbox;

    private void OnEnable()
    {
        // Double check that we have a collider, make sure its a trigger, and enable it whenever we're activated.
        if (trigger == null) trigger = GetComponent<Collider2D>();
        trigger.isTrigger = true;
        trigger.enabled = true;

        if(blockBreakHitbox!= null)
        {
            GameObject blerg = Instantiate(this.blockBreakHitbox, transform);

            // Set our action because idk how best to route it.
            blerg.TryGetComponent(out BlockBreakHitbox blockBreakHitbox);
            blockBreakHitbox.OnTileHit = OnTileHit;
        }
    }

    private void OnDisable()
    {
        // If we're disabled, we should disable our collider so nothing picks it up when it shouldn't.
        trigger.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Hitboxes should only hit things that have a health system. If no HS, do nothing.
        if (collision.TryGetComponent(out HealthSystem target)) { OnHit(target); }
    }
}