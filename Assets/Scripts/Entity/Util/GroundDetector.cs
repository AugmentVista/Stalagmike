using Assets.Scripts.Entity.Foe;
using System;
using UnityEngine;

/// <summary>
/// Lets us know when we've landed.
/// </summary>
internal class GroundDetector : MonoBehaviour
{
    // Ideally this setup should be more of a setstate, but im lazy. i aint bothering with that...
    public Action Landed = delegate { };

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // This may be better as a tag comparison, but for now we'll use components.
        if(!collision.TryGetComponent(out PlayerDetector ignored)&&!collision.TryGetComponent(out Hitbox ignored2))
        {
            Landed();
        }
    }
}