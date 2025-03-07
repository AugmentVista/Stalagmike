using Assets.Scripts;
using UnityEngine;

public class KillBox : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out HealthSystem hs))
        {
            hs.TakeDamage(int.MaxValue);
        }
    }
}