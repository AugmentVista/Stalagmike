using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entity.Util
{
    class BlockBreakHitbox:MonoBehaviour
    {
        [SerializeField] int durationTicks = 5;
        int activeTicks = 0;

        public Action<TileBreakableSystem, Vector3> OnTileHit;

        void FixedUpdate()
        {
            if (activeTicks >= durationTicks) { Destroy(gameObject); }
            activeTicks++;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out TileBreakableSystem tileSystem))
            {
                ContactPoint2D[] contactPoints = collision.contacts;
                foreach (ContactPoint2D point in contactPoints)
                {
                    OnTileHit(tileSystem, point.point);
                }
            }
            Destroy(gameObject); // Ideally we should use a pool instead, but this is small scope, idc.
        }
    }
}
