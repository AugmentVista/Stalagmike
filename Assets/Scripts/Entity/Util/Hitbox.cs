using Assets.Scripts;
using Assets.Scripts.Entity.Util;
using System;
using UnityEngine;

/// <summary>
/// Controls the hitbox of an attack.
/// </summary>
[RequireComponent(typeof(Collider2D))]
internal class Hitbox : InteractorBase
{
    Collider2D trigger;

    public Action<HealthSystem> OnHit = delegate { };

    private void OnEnable()
    {
        // Double check that we have a collider, make sure its a trigger, and enable it whenever we're activated.
        if (trigger == null) trigger = GetComponent<Collider2D>();
        trigger.isTrigger = true;
        trigger.enabled = true;
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