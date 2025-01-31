using UnityEngine;

namespace Assets.Scripts.Entity.Foe
{
    internal class Class1:MonoBehaviour
    {
        [SerializeField] foebehavior patrol, chase;
        PlayerDetector playerDetector;
        int stateInt;

        void Ready()
        {
            playerDetector = GetComponent<PlayerDetector>();
            chase.detector = playerDetector;
        }

        void PhysicsProcess()
        {
            switch (stateInt)
            {
                case 0: patrol.Execute(); break;
                case 1: chase.Execute(); break;
            }
        }
    }
}
