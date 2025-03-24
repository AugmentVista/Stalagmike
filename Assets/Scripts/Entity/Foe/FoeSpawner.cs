using System;
using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts.Entity.Foe
{
    internal class FoeSpawner : MonoBehaviour
    {
        [SerializeField] GameObject[] spawnableFoes;

        FoeBase[] activeFoes;

        // Number of ticks per spawn.
        [SerializeField] int spawnRate = 250;
        [SerializeField] int maxSpawns = 5;

        Random random = new Random(DateTime.Now.Millisecond);

        int currentTicks = 250; // initialize with 250 to make it spawn immediately.

        private void FixedUpdate()
        {
            currentTicks++;
            if (currentTicks>=spawnRate) { Spawn(); currentTicks=0; }
        }

        void Spawn()
        {
            if (activeFoes.Length<maxSpawns)
            {
                // Select a random foe, instantiate it at our position, then get its foebase component.
                FoeBase foe = (FoeBase)Instantiate(spawnableFoes[random.Next(0, spawnableFoes.Length)], transform).GetComponent(typeof(FoeBase));
                HealthSystem hs = (HealthSystem)foe.GetComponent(typeof(HealthSystem));

                // Add to list.
                var newList = activeFoes.ToList();
                newList.Add(foe);
                activeFoes = newList.ToArray();

                hs.OnDeath += OnChildDeath;

                void OnChildDeath()
                {
                    hs.OnDeath-=OnChildDeath;

                    // Remove from list.
                    var otherNewList = activeFoes.ToList();
                    newList.Remove(foe);
                    activeFoes = otherNewList.ToArray();
                }
            }
        }

        private void OnDisable()
        {
            for (int i = 0; i<activeFoes.Length; i++)
            {
                HealthSystem hs = (HealthSystem)activeFoes[i].GetComponent(typeof(HealthSystem));

                // Deal the max possible damage to the foe so it gets destroyed.
                hs.TakeDamage(int.MaxValue);
            }
        }
    }
}
