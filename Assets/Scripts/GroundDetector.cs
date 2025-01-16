using System;
using UnityEngine;

/// <summary>
/// Lets us know when we've landed.
/// </summary>
internal class GroundDetector:MonoBehaviour
{
    public Action Landed = delegate { };

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Landed();
    }
}