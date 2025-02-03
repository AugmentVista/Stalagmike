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

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out PlayerController controller)) {  PlayerDetected(controller); }
        }
    }
}