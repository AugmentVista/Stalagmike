using Assets.Scripts;
using System;
using System.Drawing;
using UnityEngine;

/// <summary>
/// Controls the hitbox of an attack.
/// </summary>
[RequireComponent(typeof(Collider2D))]
internal class Hitbox : MonoBehaviour
{
    Collider2D trigger;

    public Collider2D secretCollider; //Don't worry about it matthew

    public Action<HealthSystem> OnHit = delegate { };

    public Action<TileBreakableSystem, Vector3> OnTileHit = delegate { };

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


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out TileBreakableSystem tileSystem)) 
        {
            ContactPoint2D[] contactPoints = collision.contacts;
            foreach (ContactPoint2D point in contactPoints)
            {
                OnTileHit(tileSystem, point.point);
            }
        }
        secretCollider.enabled = false;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Hitboxes should only hit things that have a health system. If no HS, do nothing.
        if (collision.TryGetComponent(out HealthSystem target)) { OnHit(target); }

        if (collision.TryGetComponent(out TileBreakableSystem tileSystem)) 
        { 
            if(!secretCollider.isActiveAndEnabled)
            {
                secretCollider.enabled = true;
            }
        }
    }
}