using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackHazard : MonoBehaviour
{
    [SerializeField] float knockbackForce =20;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out HealthSystem hs))
        {
            GameObject entity = collision.gameObject;
            if (entity.TryGetComponent(out Rigidbody2D rb2D))
            {
                // Get the player's current velocity
                Vector2 playerVelocity = rb2D.velocity;

                // New Vector that is opposite the player's velocity direction
                Vector2 knockbackDirection = -playerVelocity.normalized;

                // Apply the knockback force in the opposite direction of the player's velocity
                rb2D.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
            }
        }
    }
}
