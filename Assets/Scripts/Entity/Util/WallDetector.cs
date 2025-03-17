using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Entity.Util
{
    /// <summary>
    /// Used for walljumps.
    /// </summary>
    internal class WallDetector:InteractorBase
    {
        public bool Colliding = false;
        private void OnTriggerStay2D(Collider2D collision)
        {
            if(!(Colliding||collision.TryGetComponent(out InteractorBase ignored)))
            {
                Colliding = true;
                Debug.Log($"Wall detection on {collision.name}");
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if(!collision.TryGetComponent(out InteractorBase ignored)) { Colliding = false; }
        }
    }
}
