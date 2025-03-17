using Assets.Scripts.Entity.Util;
using System;
using UnityEngine;

/// <summary>
/// Lets us know when we've landed.
/// </summary>
internal class GroundDetector : InteractorBase
{
    // Ideally this setup should be more of a setstate, but im lazy. i aint bothering with that...
    public Action Landed = delegate { };

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // This may be better as a tag comparison, but for now we'll use components.
        if(!collision.TryGetComponent(out InteractorBase ignored))
        {
            Debug.Log($"Landed on {collision.name}");
            Landed();
        }
    }
}