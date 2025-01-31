using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Entity.Foe.Behaviors
{
    internal class ChaseBehavior:FoeBehavior
    {
        internal PlayerDetector detector;
        internal Action Attack;
    }
}
