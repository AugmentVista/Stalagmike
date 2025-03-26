using Assets.Scripts.Entity.Util;
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
        public int damageTicks= 200;
        public int damageCooldownTicks = 200;
        List<HealthSystem> healthSystemList = new();
        bool timerActive;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out HealthSystem healthSystem))
            {
                if (!healthSystemList.Contains(healthSystem)) 
                { 
                    healthSystemList.Add(healthSystem);
                }
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out HealthSystem healthSystem))
            {
                if (healthSystemList.Contains(healthSystem)) 
                { 
                    healthSystemList.Remove(healthSystem);
                }
            }
        }
        private void FixedUpdate()
        {
            HurtPlayer();
        }

        private void HurtPlayer()
        {
            if (damageCooldownTicks < 1)
            {
                List<HealthSystem> healthSystemListCopy = new List<HealthSystem>(healthSystemList);
                foreach (HealthSystem healthSystem in healthSystemListCopy)
                {
                    Debug.Log($"HealthSystem Count is {healthSystemListCopy.Count}");
                    healthSystem.TakeDamage(damageInfo);
                }
                damageCooldownTicks = damageTicks;
            }
            else { damageCooldownTicks--; }
        }
    }
}
