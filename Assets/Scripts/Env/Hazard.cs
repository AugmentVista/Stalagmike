using Assets.Scripts.Struct;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Env
{
    /// <summary>
    /// Represents an object that can interact with entities.
    /// </summary>
    internal class Hazard : MonoBehaviour
    {
        [SerializeField] HitInfo damageInfo;
        List<HealthSystem> blerg = new();

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out HealthSystem healthSystem))
            {
                if (!blerg.Contains(healthSystem)) { blerg.Add(healthSystem); }
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out HealthSystem healthSystem))
            {
                if (blerg.Contains(healthSystem)) { blerg.Remove(healthSystem); }
            }
        }
        private void FixedUpdate()
        {
            foreach (HealthSystem healthSystem in blerg)
            {
                healthSystem.TakeDamage(damageInfo);
            }
        }
    }
}
