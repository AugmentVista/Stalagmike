using Assets.Scripts;
using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
internal class Hitbox:MonoBehaviour
{
    Collider2D trigger;

    public Action<HealthSystem> OnHit = delegate { };

    private void OnEnable()
    {
        if (trigger == null) trigger = GetComponent<Collider2D>();
        trigger.isTrigger = true;
        trigger.enabled = true;
    }

    private void OnDisable()
    {
        trigger.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out HealthSystem target)) { OnHit(target); }
    }
}