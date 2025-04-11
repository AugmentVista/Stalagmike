using UnityEngine;

internal class Projectile:MonoBehaviour
{
    [SerializeField] int lifetime = 250;

    private void FixedUpdate()
    {
        lifetime--;
        if (lifetime <= 0) { Destroy(gameObject); }
    }
}