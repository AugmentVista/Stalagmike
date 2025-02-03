using System;
using UnityEngine;

namespace Assets.Scripts.Entity.Foe
{
    /// <summary>
    /// Detects if a player is within a certain area, and reports on enter.
    /// </summary>
    internal class PlayerDetector:MonoBehaviour
    {
        public Action<PlayerController> PlayerDetected = delegate { };
        public Action PlayerLost = delegate { };
        PlayerController player;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.TryGetComponent(out PlayerController player)) {  PlayerDetected(player); this.player = player; }
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            if(other.TryGetComponent(out PlayerController player) && this.player == player) { this.player = null; PlayerLost(); }
        }
    }
}